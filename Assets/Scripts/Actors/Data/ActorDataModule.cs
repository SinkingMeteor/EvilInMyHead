using Sheldier.Actors.Data;

namespace Sheldier.Actors
{
    public class ActorDataModule
    {
        public ActorConfig ActorConfig => _actorConfig;
        public ActorMovementDataModule MovementDataModule => _movementDataModule;
        public ActorEffectDataModule EffectDataModule => _effectDataModule;
        public ActorDialogueDataModule DialogueDataModule => _dialogueDataModule;
        
        private ActorMovementDataModule _movementDataModule;
        private ActorEffectDataModule _effectDataModule;
        private ActorDialogueDataModule _dialogueDataModule;
        private ActorConfig _actorConfig;

        public void AddStaticData(ActorConfig actorConfig, ActorData data)
        {
            _actorConfig = actorConfig;
            _movementDataModule = new ActorMovementDataModule(data.MovementConfig);
            _effectDataModule = new ActorEffectDataModule();
            _dialogueDataModule = new ActorDialogueDataModule(data.DialogueConfig);
        }
    }
}