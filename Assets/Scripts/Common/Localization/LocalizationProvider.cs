using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Common.Localization
{
    public class LocalizationProvider : ILocalizationProvider
    {
        public Language CurrentLanguage => _currentLanguage;
        public IReadOnlyDictionary<string, string> LocalizedText => _localizedText;

        private Dictionary<string, string> _localizedText;
        private List<ILocalizationListener> _localizationListeners;
        private ILocalizationLoader _localizationLoader;
        private Language _currentLanguage;
        
        public void Initialize()
        {
            LocalizationPathProvider _pathProvider = new LocalizationPathProvider();
            _localizationLoader = new CsvLocalizationLoader(_pathProvider);
            _localizationListeners = new List<ILocalizationListener>();
            ChangeLanguage(Language.RU);
            
        }
        public void AddListener(ILocalizationListener listener)
        {
            _localizationListeners.Add(listener);
        }

        public void RemoveListener(ILocalizationListener listener)
        {
            _localizationListeners.Remove(listener);
        }

        private void ChangeLanguage(Language language)
        {
            _currentLanguage = Language.RU;
            _localizedText = _localizationLoader.LoadFile(language);

            for (int i = 0; i < _localizationListeners.Count; i++)
                _localizationListeners[i].OnLanguageChanged();
        }
    }
}