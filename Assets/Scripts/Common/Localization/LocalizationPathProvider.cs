using UnityEngine;

namespace Sheldier.Common.Localization
{
    public class LocalizationPathProvider
    {
        private readonly string _mainPath;
        private readonly string[] _filenames;
        public LocalizationPathProvider()
        {
            _mainPath = $"{Application.streamingAssetsPath}/Localization";
            _filenames = new string[] {"/RU", "/EN"};
        }

        public string GetPath(Language language, string extension) => _mainPath + _filenames[(int) language]+extension;
    }
}