
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