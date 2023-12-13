using ClientApi;
using Connex.DynamicForms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using Connex.DynamicForms.Controls;

namespace Connex.DynamicForms
{
    public class ListBoxControlBinder : BaseControlBinder<ListBox>, IListControl
    {
        public override void AttachEventHandlers(ListBox control, IDictionary<string, object> values, Action<string, object, DialogAction> callback)
        {
            control.SelectedValueChanged += (sender, e) => callback?.Invoke(control.Name, values, DialogAction.TextChange);
        }

        public override void ApplyElementPropertiesFromJson(ListBox formElement, JToken element)
        {
            var values = (JArray)element["text"];
            formElement.Items.Clear();
            foreach (var value in values)
            {
                formElement.Items.Add(value.ToString());
            }
        }

        public override void SetControlValueFromInt(ListBox control, int value)
        {
            if (control.Items.Contains(value))
            {
                control.SelectedIndex = value;
            }
        }

        public override int GetControlValueAsInt(ListBox control)
        {
            return control.SelectedIndex;
        }

        public override void SetControlText(ListBox control, string value)
        {
            if (control.Items.Contains(value))
            {
                control.SelectedItem = value;
            }
        }

        public override string GetControlText(ListBox control)
        {
            return control.SelectedItem.ToString();
        }

        public override void SetValue(ListBox control, object value)
        {
            if (control.Items.Contains(value))
            {
                control.SelectedItem = value;
            }
        }

        public override object GetValue(ListBox control)
        {
            return control.SelectedItem.ToString();
        }       

        public override ListBox CreateControl()
        {
            return new ListBox();
        }

        public void SetListBoxArray(Control control, List<string> entries)
        {
            try
            {
                var listBox = (ListBox)control;
                listBox.Items.Clear();
                foreach (var entry in entries) listBox.Items.Add(entry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetListBoxArray(Control control)
        {
            List<string> listItems = new List<string>();
            try
            {
                var listBox = (ListBox)control;
                foreach (var item in listBox.Items) listItems.Add(item.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listItems;
        }

    }

}
