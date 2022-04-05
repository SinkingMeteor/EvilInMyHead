using Sheldier.Item;

namespace Sheldier.Factories
{
    public interface ISubItemFactory
    {
        public void CreateItemData(string guid, string typeName);
        public SimpleItem GetItem(string guid, string typeName);

    }
}