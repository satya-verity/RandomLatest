using ClientApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public interface IControlHandler<TControl> where TControl : Control
    {
        void AttachEventHandlers(TControl control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null);
        void ApplyElementProperties(TControl formElement, JToken element);
        void SetControlValue(TControl control, object value);
        object GetControlValue(TControl control);
        TControl CreateControl();
    }
}