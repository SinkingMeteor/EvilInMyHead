using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.UI;

namespace Sheldier.Common.Pool
{
    public class SpeechCloudPool : DefaultPool<SpeechCloud>
    {
        private ISoundPlayer _soundPlayer;
        private IFontProvider _fontProvider;
        
        protected override void SetDependenciesToEntity(SpeechCloud entity)
        {
            entity.SetDependencies(_soundPlayer, _fontProvider);
        }

        public void SetDependencies(ISoundPlayer soundPlayer, IFontProvider fontProvider)
        {
            _soundPlayer = soundPlayer;
            _fontProvider = fontProvider;
        }
    }
}