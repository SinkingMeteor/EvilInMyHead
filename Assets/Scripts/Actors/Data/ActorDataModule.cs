using Sheldier.Actors.Data;

namespace Sheldier.Actors
{
    public class ActorDataModule
    {
        public ActorConfig ActorConfig => _actorConfig;
        public ActorMovementDataModule MovementDataModule => _movementDataModule;
        public ActorEffectDataModule EffectDataModule => _effectDataModule;
        public ActorDialogueDataModule DialogueDataModule => _dialogueDataModule;
        public ActorStateDataModule StateDataModule => _stateDataModule;

        private ActorMovementDataModule _movementDataModule;
        private ActorEffectDataModule _effectDataModule;
        private ActorDialogueDataModule _dialogueDataModule;
        private ActorConfig _actorConfig;
        private ActorStateDataModule _stateDataModule;


        public void AddStaticData(ActorConfig actorConfig, ActorData data)
        {
            _actorConfig = actorConfig;
            _stateDataModule = new ActorStateDataModule();
            _movementDataModule = new ActorMovementDataModule(data.MovementConfig);
            _effectDataModule = new ActorEffectDataModule();
            _dialogueDataModule = new ActorDialogueDataModule(data.DialogueConfig);
        }
    }

    public class ActorStateDataModule
    {
        public bool IsFalling => _isFalling;
        public bool IsJumping => _isJumping;
        
        private bool _isJumping;
        private bool _isFalling;

        public bool SetJump(bool isJump) => _isJumping = isJump;
        public bool SetFall(bool isFall) => _isFalling = isFall;
    }
}