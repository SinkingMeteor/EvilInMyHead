using Sheldier.Common.SaveSystem;

namespace Sheldier.Data
{
    public class DynamicStringStatsDatabase : Database<DynamicStringEntityStatsCollection>
    {
        public DynamicStringStatsDatabase(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }
    }
}