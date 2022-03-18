using Sheldier.Common.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class DialogueChoiceViewer : SerializedMonoBehaviour
    {
        [SerializeField] private RectTransform worldCanvasTransform;
        
        private SpeechCloudPool _speechCloudPool;

        [Inject]
        private void InjectDependencies(SpeechCloudPool speechCloudPool)
        {
            _speechCloudPool = speechCloudPool;
        }
        
        
    }
}