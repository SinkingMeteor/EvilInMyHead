using Sheldier.Common.SaveSystem;

namespace Sheldier.Data
{
    public class DynamicGeneralNumericalStatsDatabase : Database<DynamicNumericalEntityStatsCollection>
    {
        public DynamicGeneralNumericalStatsDatabase(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }
    }
}