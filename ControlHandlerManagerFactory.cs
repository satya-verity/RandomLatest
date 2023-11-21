using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public static class ControlHandlerManagerFactory
    {
        public static IControlHandlerManager CreateControlHandlerManager(string type)
        {
            switch (type)
            {
                case "Button":
                    return new ControlHandlerManager<Button>(new ButtonControlHandler());
                case "CheckBox":
                    return new ControlHandlerManager<CheckBox>(new CheckBoxControlHandler()); 
                case "ComboBox":
                    return new ControlHandlerManager<ComboBox> (new ComboBoxControlHandler()); 
                case "Label":
                    return new ControlHandlerManager<Label>(new LabelControlHandler()); 
                case "ListBox":
                    return new ControlHandlerManager<ListBox>(new ListBoxControlHandler()); 
                case "RadioButton":
                    return new ControlHandlerManager<RadioButton>(new RadioButtonControlHandler()); 
                case "TextBox":
                    return new ControlHandlerManager<TextBox> (new TextBoxControlHandler()); 
                default:
                    return null;
            }
        }
    }
}
