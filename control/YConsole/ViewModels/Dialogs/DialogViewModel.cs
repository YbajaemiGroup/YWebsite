namespace YConsole.ViewModels.Dialogs;

public class DialogViewModel : ViewModelBase, IDialogViewModel
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

    public void SetQuestionText(string text)
    {
        QuestionText = text;
    }
}
