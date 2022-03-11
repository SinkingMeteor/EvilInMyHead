using Sheldier.Common;
using Sheldier.Common.Animation;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class HandView : MonoBehaviour
    {
        public SimpleAnimator Animator => simpleAnimator;
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SimpleAnimator simpleAnimator;

        public void Initialize(TickHandler tickHandler)
        {
            simpleAnimator.InjectDependencies(tickHandler);
            simpleAnimator.Initialize();
        }
        public void AddItem(Sprite itemSprite) => spriteRenderer.sprite = itemSprite;
        public void ClearItem() => spriteRenderer.sprite = null;
        public void Dispose()
        {
            simpleAnimator.Dispose();
        }
    }
}