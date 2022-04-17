using Sheldier.Constants;
using UnityEngine.Rendering;

namespace Sheldier.Data
{
    public class PostProcessingLoader : AssetProvider<VolumeProfile>
    {
        protected override string Path => AssetPathConstants.POST_PROCESSING_PROVIDER;
    }
}