using System.Collections.Generic;

namespace Sheldier.Actors.AI
{
    public class ActorsAIModule : IExtraActorModule
    {

        private List<IExtraAIModule> _aiModules;
        private ActorInternalData _data;
        
        public void Initialize(ActorInternalData data)
        {
            _data = data;
            _aiModules = new List<IExtraAIModule>();
        }

        public void AddAIModule(IExtraAIModule extraActorModule)
        {
            _aiModules.Add(extraActorModule);
            extraActorModule.Initialize(_data);
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
