
using Sheldier.Data;

namespace Sheldier.Actors
{
    public class ActorDataModule
    {
        public ActorStateDataModule StateDataModule => _stateDataModule;
        
        private ActorStateDataModule _stateDataModule;

        public ActorDataModule()
        {
            _stateDataModule = new ActorStateDataModule();
        }
    }

    public class ActorStateDataModule : Database<StateData>
    {

    }
    
    public class StateData : IDatabaseItem
    {
        public string ID => _stateTypeName;

        private string _stateTypeName;

        public bool StateValue => _stateValue;

        private bool _stateValue;

        public StateData(string stateTypeName)
        {
            _stateTypeName = stateTypeName;
        }
        public void SetState(bool isActive)
        {
            _stateValue = isActive;
        }
    }
}