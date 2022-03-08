using Sheldier.Actors.Data;

namespace Sheldier.Actors
{
    public class ActorMovementDataModule
    {
        public ActorMovementConfig InitialData => _movementConfig;
        public float CurrentSpeed => _currentSpeed;
        
        private float _currentSpeed;
        private readonly ActorMovementConfig _movementConfig;

        public ActorMovementDataModule(ActorMovementConfig movementConfig)
        {
            _movementConfig = movementConfig;
            ResetSpeed();
        }

        public void SetSpeed(float speedValue) => _currentSpeed = speedValue;
        public void ResetSpeed() => _currentSpeed = _movementConfig.Speed;
    }
}