namespace YConsole.Views.Utils;

public delegate void OnDataUpdated(int playerDescriptor, int roundDescriptor, bool isUpper, object value);

public interface IPlayerMetadataCommand
{
    event OnDataUpdated? DataUpdated;
    void UpdateData(int playerDescriptor, int roundDescriptor, bool isUpper, object value);
    void Execute(int playerDescriptor, int roundDescriptor, bool isUpper, object value);
}
