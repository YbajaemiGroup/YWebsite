using System.Threading.Tasks;

namespace YConsole.ViewModels;

public interface IDataLoadable
{
    public void LoadData();
    public Task LoadDataAsync();
}
