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
        private ActorStateDataModule _dataModule;

        public void Initialize()
        {
            simpleAnimator.Initialize();
        }

        public void SetDependencies(TickHandler tickHandler, ActorStateDataModule dataModule)
        {
            _dataModule = dataModule;
            simpleAnimator.SetDependencies(tickHandler);
        }

        public void Tick()
        {
            spriteRenderer.enabled = !(_dataModule.IsFalling || _dataModule.IsJumping);
        }
        public void AddItem(Sprite itemSprite) => spriteRenderer.sprite = itemSprite;
        public void ClearItem() => spriteRenderer.sprite = null;
        public void Dispose()
        {
            simpleAnimator.Dispose();
        }
    }
}