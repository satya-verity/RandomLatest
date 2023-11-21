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
    public class CheckBoxControlHandler : IControlHandler<CheckBox>
    {
        public void AttachEventHandlers(CheckBox control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null)
        {
            control.CheckedChanged += (sender, e) => callback?.Invoke(control.Name, values, DialogAction.ControlChange);
        }

        public void ApplyElementProperties(CheckBox formElement, JToken element)
        {
            formElement.Text = element["value"]?.ToString();
        }

        public void SetControlValue(CheckBox control, object value)
        {
            if (bool.TryParse(value.ToString(), out bool checkedValue))
            {
                control.Checked = checkedValue;
            }
        }

        public object GetControlValue(CheckBox control)
        {
            return control.Checked;
        }

        public CheckBox CreateControl()
        {
            return new CheckBox();
        }
    }
}
