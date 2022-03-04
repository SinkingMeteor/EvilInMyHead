using System.Collections.Generic;
using Sheldier.Common.Animation;
using UnityEngine;

namespace Sheldier.Constants
{
    public static class AnimationConstants
    {
        public static readonly Dictionary<AnimationType, int> ANIMATIONS = new Dictionary<AnimationType, int>
        {
            {AnimationType.Idle_Front, Animator.StringToHash("Idle_Front")},
            {AnimationType.Idle_Front_Side, Animator.StringToHash("Idle_Front_Side")},
            {AnimationType.Idle_Back_Side, Animator.StringToHash("Idle_Back_Side")},
            {AnimationType.Idle_Back, Animator.StringToHash("Idle_Back")},
            
            {AnimationType.Run_Front, Animator.StringToHash("Run_Front")},
            {AnimationType.Run_Front_Side, Animator.StringToHash("Run_Front_Side")},
            {AnimationType.Run_Back_Side, Animator.StringToHash("Run_Back_Side")},
            {AnimationType.Run_Back, Animator.StringToHash("Run_Back")},
            
            {AnimationType.Idle_Equipped_Front, Animator.StringToHash("Idle_Equipped_Front")},
            {AnimationType.Idle_Equipped_Front_Side, Animator.StringToHash("Idle_Equipped_Front_Side")},
            {AnimationType.Idle_Equipped_Back_Side, Animator.StringToHash("Idle_Equipped_Back_Side")},
            {AnimationType.Idle_Equipped_Back, Animator.StringToHash("Idle_Equipped_Back")},
            
            {AnimationType.Run_Equipped_Front, Animator.StringToHash("Run_Equipped_Front")},
            {AnimationType.Run_Equipped_Front_Side, Animator.StringToHash("Run_Equipped_Front_Side")},
            {AnimationType.Run_Equipped_Back_Side, Animator.StringToHash("Run_Equipped_Back_Side")},
            {AnimationType.Run_Equipped_Back, Animator.StringToHash("Run_Equipped_Back")},
        };
    }
}