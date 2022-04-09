using Sheldier.Common.Animation;
using Sheldier.Constants;

namespace Sheldier.Data
{
    public class AnimationLoader : AssetProvider<AnimationData>
    {
        protected override string Path => AssetPathConstants.ANIMATION_PROVIDER;
    }
}