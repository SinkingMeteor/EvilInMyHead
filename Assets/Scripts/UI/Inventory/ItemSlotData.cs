using Sirenix.OdinInspector;
using UnityEngine;

namespace Sheldier.UI
{
    [System.Serializable]
    public class ItemSlotData
    {
        public string Title => title;
        public string Description => description;
        public Sprite PreviewSprite => previewSprite;
        
        [SerializeField] private string title;
        [SerializeField][Multiline] private string description;
        [SerializeField][PreviewField] private Sprite previewSprite;
    }
}