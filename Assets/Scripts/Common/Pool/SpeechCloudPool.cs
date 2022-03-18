using Sheldier.Common.Audio;
using Sheldier.UI;

namespace Sheldier.Common.Pool
{
    public class SpeechCloudPool : DefaultPool<SpeechCloud>
    {
        private ISoundPlayer _soundPlayer;

        public void SetDependencies(ISoundPlayer soundPlayer)
        {
            _soundPlayer = soundPlayer;
        }
        
        protected override void SetDependenciesToEntity(SpeechCloud entity)
        {
            entity.SetDependencies(_soundPlayer);
        }
    }
}