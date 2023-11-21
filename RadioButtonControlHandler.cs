using ClientApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public class RadioButtonControlHandler : IControlHandler<RadioButton>
    {
        public void AttachEventHandlers(RadioButton control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null)
        {
            control.CheckedChanged += (sender, e) => callback?.Invoke(control.Name, values, DialogAction.ControlChange);
        }

        public void ApplyElementProperties(RadioButton formElement, JToken element)
        {
            formElement.Text = element["value"]?.ToString();
        }

        public void SetControlValue(RadioButton control, object value)
        {
            if (bool.TryParse(value.ToString(), out bool checkedValue))
            {
                control.Checked = checkedValue;
            }
        }

        public object GetControlValue(RadioButton control)
        {
            return control.Checked;
        }

        public RadioButton CreateControl()
        {
            return new RadioButton();
        }
    }
}