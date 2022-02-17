using Sheldier.Gameplay.Effects;

namespace Sheldier.Actors.Data
{
    public interface ISubDataHandler
    {
        void Initialize();
        void Tick();
        void AddEffect(IEffect newEffect);
    }
}