using Sheldier.Common.Localization;
using Sheldier.UI;

namespace Sheldier.Common.Pool
{
    public class UIHintPool : DefaultPool<UIHint>
    {
        private IFontProvider _fontProvider;

        protected override void SetDependenciesToEntity(UIHint entity)
        {
            entity.SetDependencies(_fontProvider);
        }

        public void SetDependencies(IFontProvider fontProvider)
        {
            _fontProvider = fontProvider;
        }
    }
}