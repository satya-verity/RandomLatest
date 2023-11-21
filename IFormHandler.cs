using ClientApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connex.DynamicForms
{
    public interface IFormHandler
    {
        Form ConstructForm(string formDefinition);
        Form InitializeValues(Form form, IDictionary<string, object> values, Action<string, object, DialogAction> callback);
        DialogResult ShowFormAsDialog(Form form);
    }

}
