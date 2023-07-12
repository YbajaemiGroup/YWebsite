using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YConsole.ViewModels.Dialogs
{
    public class DialogViewModel : ViewModelBase
    {
        public string QuestionText { get; set; }

        public DialogViewModel()
        {
            QuestionText = string.Empty;
        }

        public DialogViewModel(string questionText)
        {
            QuestionText = questionText;
        }
    }
}
