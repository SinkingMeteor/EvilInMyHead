using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sheldier.UI
{
    public class SpeechRectResizer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmp;
        [SerializeField] private RectTransform tmpTransform;
        [SerializeField] private RectTransform parentTransform;

        [SerializeField] private float topMargin;
        [SerializeField] private float bottomMargin;
        [SerializeField] private float leftMargin;
        [SerializeField] private float rightMargin;
    
        [Button]
        private void GetComponents()
        {
            tmp = GetComponent<TextMeshProUGUI>();
            tmpTransform = GetComponent<RectTransform>();
            parentTransform = transform.parent.GetComponent<RectTransform>();
        }

        public void Resize(string text)
        {
            tmp.text = text;
            var rect = tmpTransform.rect;
            var width = rect.width;
            var height = LayoutUtility.GetPreferredHeight(tmpTransform);
            tmp.text = String.Empty;

            tmpTransform.sizeDelta = new Vector2(width, height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(tmpTransform);
            parentTransform.sizeDelta = new Vector2(width + leftMargin + rightMargin, height + topMargin + bottomMargin);
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentTransform);
        }
    }

}
