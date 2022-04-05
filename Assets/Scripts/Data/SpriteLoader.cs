using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Data
{
    public class SpriteLoader : AssetProvider<Sprite>
    {
        protected override string Path => AssetPathProvidersPaths.SPRITES_PROVIDER;
    }
}