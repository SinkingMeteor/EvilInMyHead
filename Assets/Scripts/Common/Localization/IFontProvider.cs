using TMPro;

namespace Sheldier.Common.Localization
{
    public interface IFontProvider
    {
        void AddListener(IFontRequier requirer);
        void RemoveListener(IFontRequier requirer);

        TMP_FontAsset GetActualFont(FontType fontType);
    }
}