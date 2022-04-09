using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Data
{
    public class SpriteLoader : AssetProvider<Sprite>
    {
        protected override string Path => AssetPathConstants.SPRITES_PROVIDER;
    }
}