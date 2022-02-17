using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors.Data
{
    public interface IActorsEffectListener
    {
        void OnEffectAdded(IEffect effect);
    }
}