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
    public interface IControlHandlerManager
    {
        void AttachEventHandlers(Control control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null);
        void ApplyElementProperties(Control formElement, JToken element);
        void SetControlValue(Control control, object value);
        object GetControlValue(Control control);
        Control CreateControl();
    }
}
