namespace YConsole.ViewModels.Dialogs;

public class DeleteConfirmationViewModel : ViewModelBase
{
    public string QuestionText { get; set; } = "Вы действительно хотите удалить выбранную запись из базы данных?";
}
