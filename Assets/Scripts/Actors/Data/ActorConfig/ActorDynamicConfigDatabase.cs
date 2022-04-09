using Sheldier.Common.SaveSystem;
using Sheldier.Constants;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    public class ActorDynamicConfigDatabase : Database<ActorDynamicConfigData>
    {
        public override string GetSaveName()
        {
            return SavableConstantNames.ACTOR_DYNAMIC_CONFIG_DATABASE;
        }

        public ActorDynamicConfigDatabase(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }
        
    }
}