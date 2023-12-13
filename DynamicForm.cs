using ClientApi;
using Connex.DynamicForms.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public class DynamicForm : IDynamicForm
    {
        private Form form;

        private readonly IFormHandler formHandler;

        public DynamicForm()
        {
            this.formHandler = new FormHandler();
        }

        public DynamicForm(IFormHandler formHandler)
        {
            this.formHandler = formHandler;
        }

        public DialogResult ShowDialog(string formDefinition, IDictionary<string, object> formData, Action<string, object, DialogAction> callBacK)
        {
            try
            {
                this.form = this.formHandler.ConstructForm(formDefinition);
                this.form = this.formHandler.InitializeValues(form, formData, callBacK);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw ex;
            }
            return this.formHandler.ShowFormAsDialog(form);
        }

        public void DlgEnable(string controlName, bool enable)
        {
            this.FindControl(controlName).Enabled = enable;
        }

        public bool DlgEnable(string controlName)
        {
            return this.FindControl(controlName).Enabled;
        }

        public void DlgEnd(int exitCode)
        {
            throw new NotImplementedException();
        }

        public bool DlgFocus(string controlName)
        {
            return this.FindControl(controlName).ContainsFocus;
        }

        public void DlgListBoxArray(string controlName, List<string> entries)
        {
            var control = this.FindControl(controlName);
            IControlBinder<Control> controlBinder = ControlBinderFactory.CreateControlBinder(control.GetType().Name);
            if (controlBinder is IListControl listControlBinder)
            {
                listControlBinder.SetListBoxArray(control, entries);
            }
        }

        public List<string> DlgListBoxArray(string controlName)
        {
            var control = this.FindControl(controlName);
            IControlBinder<Control> controlBinder = ControlBinderFactory.CreateControlBinder(control.GetType().Name);
            if (controlBinder is IListControl listControlBinder)
            {
                return listControlBinder.GetListBoxArray(control);
            }
            return null;
        }

        public void DlgText(string controlName, string text)
        {
            var control = this.FindControl(controlName);
            IControlBinder<Control> controlBinder = ControlBinderFactory.CreateControlBinder(control.GetType().Name);
            controlBinder.SetControlText(control, text);
        }

        public string DlgText(string controlName)
        {
            var control = this.FindControl(controlName);
            IControlBinder<Control> controlBinder = ControlBinderFactory.CreateControlBinder(control.GetType().Name);
            return controlBinder.GetControlText(control);
        }

        public void DlgValue(string controlName, int value)
        {
            var control = this.FindControl(controlName);
            IControlBinder<Control> controlBinder = ControlBinderFactory.CreateControlBinder(control.GetType().Name);
            controlBinder.SetControlValueFromInt(control, value);
        }

        public int DlgValue(string controlName)
        {
            var control = this.FindControl(controlName);
            IControlBinder<Control> controlBinder = ControlBinderFactory.CreateControlBinder(control.GetType().Name);
            return controlBinder.GetControlValueAsInt(control);
        }

        public void DlgVisible(string controlName, bool visible)
        {
            this.FindControl(controlName).Visible = visible;
        }

        public bool DlgVisible(string controlName)
        {
            return this.FindControl(controlName).Visible;
        }

        public void SetDlgFocus(string controlName)
        {
            this.FindControl(controlName).Focus();
        }

        public void SetDlgVisible(string controlName, bool visible)
        {
            this.FindControl(controlName).Visible = visible;
        }

        private Control FindControl(string controlName)
        {
            var control = this.form.Controls[controlName];
            if (control == null)
            {
                var error = $"There is no control with the name {controlName}.";
                Debug.WriteLine(error);
            }
            return control;
        }
    }
}
