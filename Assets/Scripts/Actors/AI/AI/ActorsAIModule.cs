using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Actors.AI
{
    public class ActorsAIModule : IExtraActorModule
    {
        public AIInputProvider AIInputProvider => _inputProvider;

        private List<IExtraAIModule> _aiModules;
        private AIInputProvider _inputProvider;
        private Actor _actor;
        private ActorInternalData _data;

        public ActorsAIModule()
        {
            _aiModules = new List<IExtraAIModule>();
        }
        
        public void Initialize(ActorInternalData data)
        {
            _data = data;
            _actor = data.Actor;
            _inputProvider = new AIInputProvider();
            _inputProvider.Initialize();
            _actor.InputController.SetInputProvider(_inputProvider);

            for (int i = 0; i < _aiModules.Count; i++)
            {
                _aiModules[i].Initialize(_data, this);
            }
        }

        public void AddAIModule(IExtraAIModule extraActorModule)
        {
            _aiModules.Add(extraActorModule);
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
