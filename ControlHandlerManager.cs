using ClientApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public class ControlHandlerManager<TControl> : IControlHandlerManager
                                                                where TControl : Control
    {
        private readonly IControlHandler<TControl> controlHandler;

        public ControlHandlerManager(IControlHandler<TControl> controlHandler)
        {
            this.controlHandler = controlHandler;
        }

        public void AttachEventHandlers(Control control, IDictionary<string, object> values, Action<string, object, DialogAction> callback = null)
        {
            if (callback == null) return;

            if (control is TControl typedControl)
            {
                controlHandler.AttachEventHandlers(typedControl, values, callback);
            }

            // Attach LostFocus event handler for any control
            control.LostFocus += (sender, e) => callback?.Invoke(control.Name, values, DialogAction.FocusChange);
        }


        public void ApplyElementProperties(Control formElement, JToken element)
        {
            if (formElement is TControl typedControl)
            {
                controlHandler.ApplyElementProperties(typedControl, element);
            }
        }

        public void SetControlValue(Control control, object value)
        {
            if (control is TControl typedControl)
            {
                controlHandler.SetControlValue(typedControl, value);
            }
        }

        public object GetControlValue(Control control)
        {
            return (control is TControl typedControl) ? controlHandler.GetControlValue(typedControl) : null;
        }

        public Control CreateControl()
        {
            return controlHandler.CreateControl();
        }
    }
}
