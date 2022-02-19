using UnityEngine;

namespace Sheldier.Actors
{
    public class ActorsHandedItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void AddItem(Sprite itemSprite) => spriteRenderer.sprite = itemSprite;
        public void ClearItem() => spriteRenderer.sprite = null;
    }
}