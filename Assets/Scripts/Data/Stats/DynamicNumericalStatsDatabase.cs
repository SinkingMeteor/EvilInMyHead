using Sheldier.Common.SaveSystem;

namespace Sheldier.Data
{
    public class DynamicNumericalStatsDatabase : Database<DynamicNumericalEntityStatsCollection>
    {
        public DynamicNumericalStatsDatabase(ISaveDatabase saveDatabase)
        {
            saveDatabase.Register(this);
        }
    }
}