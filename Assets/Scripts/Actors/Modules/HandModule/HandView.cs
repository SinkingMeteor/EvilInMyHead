using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class HandView : MonoBehaviour
    {
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        public void AddItem(Sprite itemSprite) => spriteRenderer.sprite = itemSprite;
        public void ClearItem() => spriteRenderer.sprite = null;

    }
}