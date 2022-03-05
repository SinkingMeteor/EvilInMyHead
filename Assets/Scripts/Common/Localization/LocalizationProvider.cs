using System;
using System.Collections.Generic;

namespace Sheldier.Common.Localization
{
    public class LocalizationProvider : ILocalizationProvider
    {
        public event Action OnLanguageChanged;
        
        public IReadOnlyDictionary<string, string> LocalizedText => _localizedText;
        private Dictionary<string, string> _localizedText;

        //private Language _currentLanguage;
        private ILocalizationLoader _localizationLoader;


        public LocalizationProvider()
        {
            LocalizationPathProvider _pathProvider = new LocalizationPathProvider();
            _localizationLoader = new CsvLocalizationLoader(_pathProvider);
            //TODO: Грузить язык из преференсов

        }

        public void Initialize()
        {
            ChangeLanguage(Language.RU);
        }
        public void ChangeLanguage(Language language)
        {
            //_currentLanguage = Language.RU;
            _localizedText = _localizationLoader.LoadFile(language);
            OnLanguageChanged?.Invoke();
        }
    }
}