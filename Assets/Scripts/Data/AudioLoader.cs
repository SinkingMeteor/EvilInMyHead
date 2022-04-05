using Sheldier.Common.Audio;
using Sheldier.Constants;

namespace Sheldier.Data
{
    public class AudioLoader : AssetProvider<AudioUnit>
    {
        protected override string Path => AssetPathProvidersPaths.AUDIO_PROVIDER;
    }
}