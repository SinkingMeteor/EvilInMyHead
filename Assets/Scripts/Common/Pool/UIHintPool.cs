using Sheldier.Common.Localization;
using Sheldier.UI;
using Zenject;

namespace Sheldier.Common.Pool
{
    public class UIHintPool : DefaultPool<UIHint>
    {
        private IFontProvider _fontProvider;

        protected override void SetDependenciesToEntity(UIHint entity)
        {
            entity.SetDependencies(_fontProvider);
        }

        [Inject]
        private void InjectDependencies(IFontProvider fontProvider)
        {
            _fontProvider = fontProvider;
        }
    }
}