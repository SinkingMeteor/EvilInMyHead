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
        private Database<DynamicStringEntityStatsCollection> _dynamicStringStatsDatabase;
        private Database<DynamicNumericalEntityStatsCollection> _dynamicNumericStatsDatabase;

        [Inject]
        private void InjectDependencies(ISoundPlayer soundPlayer, IFontProvider fontProvider, Database<DynamicStringEntityStatsCollection> dynamicStringStatsDatabase,
            Database<DynamicNumericalEntityStatsCollection> dynamicNumericStatsDatabase)
        {
            _dynamicNumericStatsDatabase = dynamicNumericStatsDatabase;
            _dynamicStringStatsDatabase = dynamicStringStatsDatabase;
            _soundPlayer = soundPlayer;
            _fontProvider = fontProvider;
        }
        
        protected override void SetDependenciesToEntity(SpeechCloud entity)
        {
            entity.SetDependencies(_soundPlayer, _fontProvider, _dynamicStringStatsDatabase, _dynamicNumericStatsDatabase);
        }


    }
}