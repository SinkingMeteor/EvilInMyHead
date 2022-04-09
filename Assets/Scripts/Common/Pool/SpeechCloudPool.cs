using Sheldier.Actors.Data;
using Sheldier.Common.Audio;
using Sheldier.Common.Localization;
using Sheldier.Data;
using Sheldier.UI;
using Zenject;

namespace Sheldier.Common.Pool
{
    public class SpeechCloudPool : DefaultPool<SpeechCloud>
    {
        private ISoundPlayer _soundPlayer;
        private IFontProvider _fontProvider;
        private Database<ActorStaticDialogueData> _staticDialogueDatabase;
        private Database<ActorDynamicConfigData> _dynamicConfigDatabase;

        [Inject]
        private void InjectDependencies(ISoundPlayer soundPlayer, IFontProvider fontProvider, Database<ActorStaticDialogueData> staticDialogueDatabase,
            Database<ActorDynamicConfigData> dynamicConfigDatabase)
        {
            _dynamicConfigDatabase = dynamicConfigDatabase;
            _staticDialogueDatabase = staticDialogueDatabase;
            _soundPlayer = soundPlayer;
            _fontProvider = fontProvider;
        }
        
        protected override void SetDependenciesToEntity(SpeechCloud entity)
        {
            entity.SetDependencies(_soundPlayer, _fontProvider, _staticDialogueDatabase, _dynamicConfigDatabase);
        }


    }
}