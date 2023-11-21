using ClientApi;
using System;
using System.Collections.Generic;
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
            this.form = this.formHandler.ConstructForm(formDefinition);
            this.form = this.formHandler.InitializeValues(form, formData, callBacK);

            return this.formHandler.ShowFormAsDialog(form);          
        }

        public void DlgEnable(string controlName, bool enable)
        {
            throw new NotImplementedException();
        }

        public bool DlgEnable(string controlName)
        {
            throw new NotImplementedException();
        }

        public void DlgEnd(int exitCode)
        {
            throw new NotImplementedException();
        }

        public bool DlgFocus(string controlName)
        {
            throw new NotImplementedException();
        }

        public void DlgListBoxArray(string controlName, List<string> entries)
        {
            throw new NotImplementedException();
        }

        public List<string> DlgListBoxArray(string controlName)
        {
            throw new NotImplementedException();
        }

        public void DlgText(string controlName, string text)
        {
            throw new NotImplementedException();
        }

        public string DlgText(string controlName)
        {
            throw new NotImplementedException();
        }

        public void DlgValue(string controlName, object value)
        {
            throw new NotImplementedException();
        }

        public void DlgValue(string controlName, int value)
        {
            throw new NotImplementedException();
        }

        public string DlgValue(string controlName)
        {
            throw new NotImplementedException();
        }

        public void DlgVisible(string controlName, bool visible)
        {
            throw new NotImplementedException();
        }

        public bool DlgVisible(string controlName)
        {
            throw new NotImplementedException();
        }

        public void SetDlgFocus(string controlName)
        {
            throw new NotImplementedException();
        }

        public void SetDlgVisible(string controlName, bool visible)
        {
            throw new NotImplementedException();
        }
    }
}
