using Sheldier.Common.Animation;
using Sheldier.Constants;

namespace Sheldier.Data
{
    public class AnimationCollectionLoader : AssetProvider<ActorAnimationCollection>
    {
        protected override string Path => AssetPathConstants.ANIMATION_COLLECTION_PROVIDER;
    }
}