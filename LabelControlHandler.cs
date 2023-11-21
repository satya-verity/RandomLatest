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
    public class LabelControlHandler : IControlHandler<Label>
    {
        public void AttachEventHandlers(Label control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null)
        {
            // No event handlers to handle.
        }

        public void ApplyElementProperties(Label formElement, JToken element)
        {
            formElement.Text = element["value"]?.ToString();
        }

        public void SetControlValue(Label control, object value)
        {
            control.Text = value.ToString();
        }

        public object GetControlValue(Label control)
        {
            return control.Text;
        }

        public Label CreateControl()
        {
            return new Label();
        }
    }
}
