using System.Collections.Generic;

namespace Sheldier.Common.SaveSystem
{
    public class SaveLoadDatabase : ISaveDatabase
    {
        private List<ISavable> _savables;

        public SaveLoadDatabase()
        {
            _savables = new List<ISavable>();
        }
        
        public void Register(ISavable savable) => _savables.Add(savable);

        public IReadOnlyList<ISavable> GetAll() => _savables;
    }
}