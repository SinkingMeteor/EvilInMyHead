using Sheldier.Actors.Data;
using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.Data;
using Sheldier.UI;

namespace Sheldier.Common.Pool
{
    public class SpeechCloudPool : DefaultPool<SpeechCloud>
    {
        private ISoundPlayer _soundPlayer;
        private IFontProvider _fontProvider;
        private Database<ActorDynamicDialogueData> _dynamicDialogueDatabase;

        protected override void SetDependenciesToEntity(SpeechCloud entity)
        {
            entity.SetDependencies(_soundPlayer, _fontProvider, _dynamicDialogueDatabase);
        }

        public void SetDependencies(ISoundPlayer soundPlayer, IFontProvider fontProvider, Database<ActorDynamicDialogueData> dynamicDialogueDatabase)
        {
            _dynamicDialogueDatabase = dynamicDialogueDatabase;
            _soundPlayer = soundPlayer;
            _fontProvider = fontProvider;
        }
    }
}