﻿using Sheldier.ScriptableObjects;
using UnityEngine;

namespace Sheldier.Common.Animation
{
    [CreateAssetMenu(menuName = "Sheldier/Animation/AnimationData", fileName = "AnimationData")]
    public class AnimationData : BaseScriptableObject
    {
        public Sprite[] Frames => frames;
        public bool IsLoop => isLoop;
        public float FrameRate => frameRate;
        
        [SerializeField] private Sprite[] frames;
        [SerializeField] private bool isLoop = true;
        [SerializeField] private float frameRate = 9.5f;
    }
}