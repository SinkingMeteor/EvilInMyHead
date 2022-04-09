namespace Sheldier.Common.SaveSystem
{
    public interface ISaveUtility
    {
        bool IsLoadFileExists(string fileName);
        void SaveData(string data, string key);
        string GetData(string key);
    }
}