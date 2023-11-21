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
    public class TextBoxControlHandler : IControlHandler<TextBox>
    {
        public void AttachEventHandlers(TextBox control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null)
        {
            control.TextChanged += (sender, e) => callback?.Invoke(control.Name, values, DialogAction.TextChange);
        }

        public void ApplyElementProperties(TextBox formElement, JToken element)
        {
            formElement.Text = element["value"]?.ToString();
        }

        public void SetControlValue(TextBox control, object value)
        {
            control.Text = value.ToString();
        }

        public object GetControlValue(TextBox control)
        {
            return control.Text;
        }

        public TextBox CreateControl()
        {
            return new TextBox();
        }
    }
}
