using Sheldier.Actors.Data;

namespace Sheldier.Actors
{
    public class ActorDataModule
    {
        public ActorMovementDataModule MovementDataModule => _movementDataModule;
        public ActorEffectDataModule EffectDataModule => _effectDataModule;
        
        private ActorMovementDataModule _movementDataModule;
        private ActorEffectDataModule _effectDataModule;

        public void AddStaticData(ActorData data)
        {
            _movementDataModule = new ActorMovementDataModule(data.MovementConfig);
            _effectDataModule = new ActorEffectDataModule();
        }
    }
}