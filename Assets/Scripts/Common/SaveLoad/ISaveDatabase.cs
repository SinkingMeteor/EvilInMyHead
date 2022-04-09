using System.Collections.Generic;

namespace Sheldier.Common.SaveSystem
{
    public interface ISaveDatabase
    {
        void Register(ISavable savable);

        IReadOnlyList<ISavable> GetAll();
    }
}