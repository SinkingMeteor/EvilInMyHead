using Sheldier.Common.Cutscene;
using Sheldier.Constants;

namespace Sheldier.Data
{
    public class CutsceneLoader : AssetProvider<Cutscene>
    {
        protected override string Path => AssetPathConstants.CUTSCENE_PROVIDER;
    }
}