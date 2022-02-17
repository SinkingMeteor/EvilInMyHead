using Sheldier.Gameplay.Effects;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Sheldier.Actors.Data
{
    public class ActorDataHandler : SerializedMonoBehaviour, IActorModule, IActorsEffectListener
    {
        public int Priority => 1;

        [OdinSerialize] private ISubDataHandler[] _subDataHandlers;
        public void Initialize(IActorModuleCenter actorModuleCenter)
        {
            foreach (var subHandler in _subDataHandlers)
                subHandler.Initialize();
        }
        public void OnEffectAdded(IEffect effect)
        {
            foreach (var subHandler in _subDataHandlers)
                subHandler.AddEffect(effect);   
        }
        public void Tick()
        {
            foreach (var subHandler in _subDataHandlers)
                subHandler.Tick();
        }

        

   
    }
}