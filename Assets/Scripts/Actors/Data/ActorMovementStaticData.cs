using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    public class ActorMovementStaticData : IDatabaseItem
    {
        public string ID => _ownerOwnerID;
        public string DefaultMovementSoundID => _defaultMovementSoundID;
        public float DefaultSpeed => _defaultSpeed;
        
        private string _ownerOwnerID;
        private float _defaultSpeed;
        private string _defaultMovementSoundID;
    }
}