using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Actors.AI
{
    public class ActorsAIModule : SerializedMonoBehaviour, IExtraActorModule
    {
        public int Priority => 0;
        public AIInputProvider AIInputProvider => _inputProvider;

        [OdinSerialize] private IExtraAIModule[] _aiModules;
        
        private AIInputProvider _inputProvider;
        private Actor _actor;
        private ActorInternalData _data;

        public void Initialize(ActorInternalData data)
        {
            _data = data;
            _actor = data.Actor;
            _inputProvider = new AIInputProvider();
            _inputProvider.Initialize();
            _actor.InputController.SetInputProvider(_inputProvider);

            for (int i = 0; i < _aiModules.Length; i++)
            {
                _aiModules[i].Initialize(_data, this);
            }
        }

        public void Dispose()
        {
            foreach (var aiModule in _aiModules)
            {
                aiModule.Dispose();
            }
        }
    }
}
