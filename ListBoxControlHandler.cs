using ClientApi;
using Connex.DynamicForms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public class ListBoxControlHandler : IControlHandler<ListBox>
    {
        public void AttachEventHandlers(ListBox control, IDictionary<string, object> values, Action<string, object, DialogAction> callback=null)
        {
            control.SelectedValueChanged += (sender, e) => callback?.Invoke(control.Name, values, DialogAction.TextChange);
        }

        public void ApplyElementProperties(ListBox formElement, JToken element)
        {
            var values = (JArray)element["value"];
            foreach (var value in values)
            {
                formElement.Items.Add(value.ToString());
            }
        }

        public void SetControlValue(ListBox control, object value)
        {
            if (control.Items.Contains(value))
            {
                control.SelectedItem = value;
            }
        }

        public object GetControlValue(ListBox control)
        {
            return control.SelectedItem;
        }

        public ListBox CreateControl()
        {
            return new ListBox();
        }

    }

}