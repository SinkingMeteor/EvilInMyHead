using System.Collections.Generic;
using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.UI
{
    [CreateAssetMenu(menuName = "Sheldier/UI/UIRequest", fileName = "UIStateRequest")]
    public class UIStatesRequest : BaseScriptableObject
    {
        public IReadOnlyList<UIType> UITypes => uiTypes;

        [SerializeField] private UIType[] uiTypes;
    }
}