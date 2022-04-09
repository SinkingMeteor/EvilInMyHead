using Sheldier.Common.SaveSystem;
using Sheldier.Constants;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    public class ActorDynamicMovementDatabase : Database<ActorDynamicMovementData>
    {
        public ActorDynamicMovementDatabase(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }

        public override string GetSaveName()
        {
            return SavableConstantNames.ACTOR_DYNAMIC_MOVEMENT_DATABASE;
        }
    }
}