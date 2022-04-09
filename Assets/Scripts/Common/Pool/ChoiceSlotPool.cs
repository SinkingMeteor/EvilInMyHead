using Sheldier.Common.Localization;
using Sheldier.UI;
using Zenject;

namespace Sheldier.Common.Pool
{
    public class ChoiceSlotPool : DefaultPool<ChoiceSlot>
    {
        private IFontProvider _fontProvider;

        protected override void SetDependenciesToEntity(ChoiceSlot entity)
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