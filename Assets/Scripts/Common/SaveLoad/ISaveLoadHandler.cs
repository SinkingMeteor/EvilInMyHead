namespace Sheldier.Common.SaveSystem
{
    public interface ISaveLoadHandler
    {
        void SaveAll(string filename);
        void LoadAll(string filename);
    }
}