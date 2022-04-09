using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Data
{
    public class DataLoader : AssetProvider<TextAsset>
    {
        protected override string Path => AssetPathConstants.DATA_PROVIDER;
    }
}