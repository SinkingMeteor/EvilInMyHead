using Sheldier.Common.Animation;
using UnityEngine;

namespace Sheldier.Actors.Hand
{
    public interface IHandView
    {
        Transform Transform { get; }
        MonoBehaviour Behaviour { get; }
        SimpleAnimator Animator { get; }
        void AddItem(Sprite itemSprite);
        void ClearItem();
    }
}