using Sheldier.Common.Localization;
using Sheldier.UI;

namespace Sheldier.Common.Pool
{
    public class ChoiceSlotPool : DefaultPool<ChoiceSlot>
    {
        private IFontProvider _fontProvider;

        protected override void SetDependenciesToEntity(ChoiceSlot entity)
        {
            entity.SetDependencies(_fontProvider);
        }

        public void SetDependencies(IFontProvider fontProvider)
        {
            _fontProvider = fontProvider;
        }
    }
}