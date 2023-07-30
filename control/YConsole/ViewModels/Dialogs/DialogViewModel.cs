namespace YConsole.ViewModels.Dialogs;

public class DialogViewModel : ViewModelBase, IDialogViewModel
{
    public string QuestionText { get; set; }

    public DialogViewModel()
    {
        QuestionText = "Вы действительно хотите удалить выбранную запись из базы данных? Все связанные данные будут также удалены.";
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
