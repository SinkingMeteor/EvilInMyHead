using Sheldier.Common.SaveSystem;

namespace Sheldier.Data
{
    public class DynamicGeneralStringStatsDatabase : Database<DynamicStringEntityStatsCollection>
    {
        public DynamicGeneralStringStatsDatabase(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }
    }
}