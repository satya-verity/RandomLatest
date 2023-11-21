using ClientApi;
using Connex.DynamicForms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public class ButtonControlHandler : IControlHandler<Button>
    {
        public void AttachEventHandlers(Button control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null)
        {
            control.Click += (sender, e) => callback?.Invoke(control.Name, values, DialogAction.ControlChange);
        }

        public void ApplyElementProperties(Button formElement, JToken element)
        {
            formElement.Text = element["value"]?.ToString();
        }

        public void SetControlValue(Button control, object value)
        {
            control.Text = value.ToString();
        }

        public object GetControlValue(Button control)
        {
            return control.Text;
        }

        public Button CreateControl()
        {
            return new Button();
        }
    }
}