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
                AddControl(form, element);
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

        private void AddControl(Form form, JToken element)
        {
            form.Controls.Add((element["elements"] == null) ? CreateSingleControl(element): CreateGroupControl(element));
        }

        private Control CreateGroupControl(JToken element)
        {
            string elementType = element.Value<string>("type");

            Control groupElement = new GroupControl();

            foreach (var controlElement in element["elements"]?.Children() ?? Enumerable.Empty<JToken>())
            {
                groupElement?.Controls.Add(CreateSingleControl(controlElement));
            }
            return groupElement;
        }

        private Control CreateSingleControl(JToken element)
        {
            string elementType = element.Value<string>("type");
            IControlBinder<Control> controlBinder = ControlBinderFactory.CreateControlBinder(elementType);

            Control formElement = controlBinder?.CreateControl();

            if (formElement != null)
            {
                InitializeControlProperties(formElement, element);
                controlBinder.ApplyElementProperties(formElement, element);
            }
            return formElement;
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
            IControlBinder<Control> controlBinderManager = ControlBinderFactory.CreateControlBinder(control.GetType().Name);

            if (values.TryGetValue(control.Name, out var controlValue))
            {
                controlBinderManager?.SetControlValue(control, controlValue);
            }

            // Attach event handlers for relevant controls
            controlBinderManager?.AttachEventHandlers(control, values, callback);
        }

    }

}
