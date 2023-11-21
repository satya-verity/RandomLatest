using Newtonsoft.Json.Linq;
using ClientApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Connex.DynamicForms;
using System.Linq;

namespace Connex.DynamicForms
{
    public class FormHandler : IFormHandler
    {
        public Form ConstructForm(string formDefinition)
        {
            Form form = new Form();
            JObject formTemplate = JObject.Parse(formDefinition);

            InitializeFormProperties(form, formTemplate);

            foreach (var element in formTemplate["elements"]?.Children() ?? Enumerable.Empty<JToken>())
            {
                CreateAndAddControl(form, element);
            }

            return form;
        }

        public Form InitializeValues(Form form, IDictionary<string, object> values, Action<string, object, DialogAction> callback)
        {
            if (form == null) return null;

            foreach (Control control in form.Controls)
            {
                InitializeControlValues(control, values, callback);
            }

            callback?.Invoke(form.Name, values, DialogAction.Initialize);

            return form;
        }

        public DialogResult ShowFormAsDialog(Form form)
        {
            return form.ShowDialog();
        }

        private void InitializeFormProperties(Form form, JObject formTemplate)
        {
            form.Left = formTemplate.Value<int>("x");
            form.Top = formTemplate.Value<int>("y");
            form.Width = formTemplate.Value<int>("width");
            form.Height = formTemplate.Value<int>("height");
            form.Text = formTemplate.Value<string>("title");
        }

        private void CreateAndAddControl(Form form, JToken element)
        {
            string elementType = element.Value<string>("type");
            IControlHandlerManager controlHandlerManager = ControlHandlerManagerFactory.CreateControlHandlerManager(elementType);

            Control formElement = controlHandlerManager?.CreateControl();

            if (formElement != null)
            {
                InitializeControlProperties(formElement, element);
                controlHandlerManager.ApplyElementProperties(formElement, element);
                form.Controls.Add(formElement);
            }
        }

        private void InitializeControlProperties(Control formElement, JToken element)
        {
            formElement.Left = element.Value<int>("x");
            formElement.Top = element.Value<int>("y");
            formElement.Width = element.Value<int>("width");
            formElement.Height = element.Value<int>("height");
            formElement.Name = element.Value<string>("name");
        }

        private void InitializeControlValues(Control control, IDictionary<string, object> values, Action<string, object, DialogAction> callback)
        {
            IControlHandlerManager controlHandlerManager = ControlHandlerManagerFactory.CreateControlHandlerManager(control.GetType().Name);

            if (values.TryGetValue(control.Name, out var controlValue))
            {
                controlHandlerManager?.SetControlValue(control, controlValue);
            }

            // Attach event handlers for relevant controls
            controlHandlerManager?.AttachEventHandlers(control, values, callback);
        }

    }

}
