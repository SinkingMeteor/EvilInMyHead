using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    public class ActorMovementDynamicData : IStorageItem
    {
        public string OwnerID => _currentOwnerID;
        public float CurrentSpeed => _currentSpeed;
        public string CurrentMovementSoundID => _currentMovementSoundID;

        private float _currentSpeed;
        private string _currentOwnerID;
        private string _currentMovementSoundID;

        public ActorMovementDynamicData(ActorMovementStaticData staticData)
        {
            _currentSpeed = staticData.DefaultSpeed;
            _currentOwnerID = staticData.OwnerID;
            _currentMovementSoundID = staticData.DefaultMovementSoundID;
        }

        public ActorMovementDynamicData(string ownerID, string movementSoundID, float speed)
        {
            _currentOwnerID = ownerID;
            _currentMovementSoundID = movementSoundID;
            _currentSpeed = speed;
        }
        
        public void SetSpeed(float speed) => _currentSpeed = speed;
        public void SetOwnerID(string ID) => _currentOwnerID = ID;
        public void SetMovementSoundID(string ID) => _currentMovementSoundID = ID;
    }
}