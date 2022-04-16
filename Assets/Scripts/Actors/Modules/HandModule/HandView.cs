using Sheldier.Common;
using Sheldier.Common.Animation;
using Sheldier.Constants;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public class HandView : MonoBehaviour, IHandView
    {
        public Transform Transform => transform;
        public MonoBehaviour Behaviour => this;
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
            if (!_dataModule.TryGet(GameplayConstants.FALL_STATE_DATA, out var fallingState))
                return;
            if (!_dataModule.TryGet(GameplayConstants.JUMP_STATE_DATA, out var jumpingState))
                return;
            spriteRenderer.enabled = !(fallingState.StateValue || jumpingState.StateValue);
        }
        public void AddItem(Sprite itemSprite) => spriteRenderer.sprite = itemSprite;
        public void ClearItem() => spriteRenderer.sprite = null;
        public void Dispose()
        {
            simpleAnimator.Dispose();
        }


    }
}