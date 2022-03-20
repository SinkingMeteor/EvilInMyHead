using System.Collections.Generic;
using Sheldier.Actors;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sheldier.Graphs.DialogueSystem;
using UnityEngine;

namespace Sheldier.UI
{
    public class SpeechCloudController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;
        
        private SpeechCloudPool _speechCloudPool;
        private ILocalizationProvider _localizationProvider;
        private SpeechCloud _currentSpeechCloud;
        private IDialogueReplica _currentReplica;
        private Queue<SpeechCloud> _speechClouds;

        public void Initialize()
        {
            _speechClouds = new Queue<SpeechCloud>();
        }
        public void SetDependencies(SpeechCloudPool speechCloudPool, ILocalizationProvider localizationProvider)
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