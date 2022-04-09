namespace Sheldier.Common.SaveSystem
{
    public interface ISavable
    {
        string GetSaveName();
        string Save();
        void Load(string jsonedData);
    }
}