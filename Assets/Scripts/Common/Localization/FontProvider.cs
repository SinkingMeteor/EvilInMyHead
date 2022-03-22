using System.Collections.Generic;
using TMPro;

namespace Sheldier.Common.Localization
{
    public class FontProvider : ILocalizationListener, IFontProvider
    {
        private List<IFontRequier> _requierers;
        private ILocalizationProvider _localizationProvider;
        private FontMap _fontMap;

        public void Initialize()
        {
            _requierers = new List<IFontRequier>();
            _localizationProvider.AddListener(this);
        }

        public void SetDependencies(ILocalizationProvider localizationProvider, FontMap fontMap)
        {
            _fontMap = fontMap;
            _localizationProvider = localizationProvider;
        }

        public void AddListener(IFontRequier requirer) => _requierers.Add(requirer);

        public void RemoveListener(IFontRequier requirer) => _requierers.Remove(requirer);
        
        public TMP_FontAsset GetActualFont(FontType fontType) => _fontMap.GetFontAsset(fontType, _localizationProvider.CurrentLanguage);

        public void OnLanguageChanged()
        {
            for (int i = 0; i < _requierers.Count; i++)
            {
                FontType fontType = _requierers[i].FontTypeRequirer;
                _requierers[i].UpdateFont(_fontMap.GetFontAsset(fontType, _localizationProvider.CurrentLanguage));
            }
        }

        public void Dispose()
        {
            _localizationProvider.RemoveListener(this);
        }
    }
}