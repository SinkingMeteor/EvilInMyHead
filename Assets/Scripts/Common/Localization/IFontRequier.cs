using TMPro;

namespace Sheldier.Common.Localization
{
    public interface IFontRequier
    {
        FontType FontTypeRequirer { get; }

        void UpdateFont(TMP_FontAsset textAsset);
    }
}