using ClientApi;
using Connex.DynamicForms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public class ComboBoxControlHandler : IControlHandler<ComboBox>
    {
        public void AttachEventHandlers(ComboBox control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null)
        {
            control.TextChanged += (sender, e) => callback?.Invoke(control.Name, values, DialogAction.TextChange);
        }

        public void ApplyElementProperties(ComboBox formElement, JToken element)
        {
            var values = (JArray)element["value"];
            foreach (var value in values)
            {
                formElement.Items.Add(value.ToString());
            }
        }

        public void SetControlValue(ComboBox control, object value)
        {
            if (control.Items.Contains(value))
            {
                control.SelectedItem = value;
            }
        }

        public object GetControlValue(ComboBox control)
        {
            return control.SelectedItem;
        }

        public ComboBox CreateControl()
        {
            return new ComboBox();
        }

    }

}