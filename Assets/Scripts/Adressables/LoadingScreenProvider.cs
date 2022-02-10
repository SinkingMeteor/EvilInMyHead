using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sheldier.Setup
{
    public class LoadingScreenProvider : AdressablesAssetLoader
    {
        private const string LOADING_SCREEN_TAG = "LoadingScreen";
        
        public async Task LoadAndDestroy(IEnumerable<ILoadOperation> loadOperations)
        {
            var loadingScreen = await LoadAssetAsync<LoadingScreen>(LOADING_SCREEN_TAG);
            await loadingScreen.LoadAsync(loadOperations);
            Unload();
        }
        public Task<LoadingScreen> LoadAsync() => LoadAssetAsync<LoadingScreen>(LOADING_SCREEN_TAG);
        public void Unload() => UnloadAsset();
    }
}