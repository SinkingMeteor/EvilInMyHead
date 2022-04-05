using System;
using System.Collections.Generic;
using Sheldier.Item;

namespace Sheldier.Factories
{
    public class AmmoItemFactory: ISubItemFactory
    {
        private Dictionary<string, AmmoItem> _ammosCollection;

        public AmmoItemFactory()
        {
            _ammosCollection = new Dictionary<string, AmmoItem>()
            {
                {"PistolAmmo", new AmmoItem(Guid.NewGuid().ToString())}
            };
        }

        public void CreateItemData(string guid, string typeName)
        {
            
        }

        public SimpleItem GetItem(string guid,string typeName)
        {
            return _ammosCollection[typeName].CleanClone(guid);
        }
    }
}