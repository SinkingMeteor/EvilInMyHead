using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sheldier.Graphs.DialogueSystem;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class SpeechCloudController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;
        
        private IPool<SpeechCloud> _speechCloudPool;
        private ILocalizationProvider _localizationProvider;
        private IDialogueReplica _currentReplica;
        private Queue<SpeechCloud> _speechClouds;
        private SpeechCloud _currentSpeechCloud;

        public void Initialize()
        {
            _speechClouds = new Queue<SpeechCloud>();
        }
        [Inject]
        private void InjectDependencies(IPool<SpeechCloud> speechCloudPool, ILocalizationProvider localizationProvider)
        {
            _speechCloudPool = speechCloudPool;
            _localizationProvider = localizationProvider;
        }

        public void RevealCloud(IDialogueReplica currentReplica, Actor actor)
        {
            Transform speechPoint = actor.ActorsView.SpeechPoint;
            _currentReplica = currentReplica;
            SpeechCloud currentSpeechCloud = InstantiateCloud(speechPoint);
            currentSpeechCloud.SetText(_localizationProvider.LocalizedText[currentReplica.Replica], actor);
            _speechClouds.Enqueue(currentSpeechCloud);
        }

        public void DeactivateCurrentCloud()
        {
            if (_speechClouds.Count == 0)
                return;
            _speechClouds.Dequeue().CloseCloud();
        }
        
        private SpeechCloud InstantiateCloud(Transform speechPoint)
        {
            SpeechCloud cloud = _speechCloudPool.GetFromPool();
            cloud.transform.SetParent(canvasRectTransform);
            cloud.transform.position = speechPoint.position;
            return cloud;
        }


    }
}