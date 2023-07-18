namespace YConsole.Views.Utils
{
    public class PlayerCommand : IPlayerMetadataCommand
    {
        public event OnDataUpdated? DataUpdated;

        private readonly OnDataUpdated methodToExecute;

        public PlayerCommand(OnDataUpdated methodToExecute)
        {
            this.methodToExecute = methodToExecute;
        }

        public void Execute(int playerDescriptor, int roundDescriptor, bool isUpper, object value)
        {
            methodToExecute.Invoke(playerDescriptor, roundDescriptor, isUpper, value);
        }

        public void UpdateData(int playerDescriptor, int roundDescriptor, bool isUpper, object value)
        {
            DataUpdated?.Invoke(playerDescriptor, roundDescriptor, isUpper, value);
        }
    }
}
