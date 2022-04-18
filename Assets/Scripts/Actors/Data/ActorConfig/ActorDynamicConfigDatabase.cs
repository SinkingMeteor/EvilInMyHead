using Sheldier.Common.SaveSystem;
using Sheldier.Constants;
using Sheldier.Data;

namespace Sheldier.Actors.Data
{
    public class ActorDynamicConfigDatabase : Database<ActorDynamicConfigData>
    {
        public ActorDynamicConfigDatabase(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }
        
    }
}