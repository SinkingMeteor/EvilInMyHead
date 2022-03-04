using Sheldier.ScriptableObjects;
using Sheldier.UI;
using UnityEngine;

namespace Sheldier.Setup
{
    [CreateAssetMenu(menuName = "Sheldier/Scene/Data", fileName = "SceneData")]
    public class SceneData : BaseScriptableObject
    {
        public string SceneName => sceneName;
        public UIStatesRequest UIStatesRequest => uiStatesRequest;

        [SerializeField] private string sceneName;
        [SerializeField] private UIStatesRequest uiStatesRequest;
    }
}