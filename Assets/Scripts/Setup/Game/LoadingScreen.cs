using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.Setup
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private LoadingViewer _loadingViewer;

        public async Task LoadAsync(IEnumerable<ILoadOperation> loadingOperations)
        {
            _loadingViewer.EnableCanvas();
            foreach (var loadingOperation in loadingOperations)
            {
                _loadingViewer.SetDescription(loadingOperation.LoadLabel);
                await loadingOperation.Load(_loadingViewer.SetProgress);
                _loadingViewer.ResetProgress();
            }
        }

        [Button]
        private void GetComponents()
        {
            _loadingViewer = GetComponent<LoadingViewer>();
        }
    }
}