using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sheldier.Common.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SimpleAnimator : MonoBehaviour, ITickListener
    {
        public bool IsPlaying => _isPlaying;
        public event Action OnAnimationEnd;
        public event Action OnAnimationTriggered;
        
        [SerializeField] private AnimationData currentAnimation;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private bool _playOnInitialize;
        
        private float _currentFrame;
        private bool _isPlaying;
        private TickHandler _tickHandler;
        private List<bool> _triggers;
        private int FramesCount => currentAnimation.Frames.Length;
        
        public void Initialize()
        {
            _triggers = new List<bool>();
            if (_playOnInitialize && currentAnimation != null)
                Play();
        }
        public void SetDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }
        public void Play(AnimationData data)
        {
            currentAnimation = data;
            Play();
        }
        public void Play()
        {
            if(_isPlaying)
                StopPlaying();

            for (int i = 0; i < currentAnimation.TriggerPoints.Count; i++)
                _triggers.Add(true);
            
            _isPlaying = true;
            _tickHandler.AddListener(this);
        }

        public void Tick()
        {
            _currentFrame += _tickHandler.TickDelta * currentAnimation.FrameRate;
            
            if (_currentFrame >= FramesCount)
                if (!RewindAnimation())
                    return;
            
            for (int i = 0; i < currentAnimation.TriggerPoints.Count; i++)
                if (_currentFrame >= currentAnimation.TriggerPoints[i] && _triggers[i])
                {
                    OnAnimationTriggered?.Invoke();
                    _triggers[i] = false;
                }
            
            int frameIndex = (int) _currentFrame;
            spriteRenderer.sprite = currentAnimation.Frames[frameIndex];

        }
        private bool RewindAnimation()
        {
            if (!currentAnimation.IsLoop && (int) _currentFrame >= FramesCount - 1)
            {
                StopPlaying();
                return false;
            }
            
            _currentFrame %= FramesCount;

            for (int i = 0; i < _triggers.Count; i++)
                _triggers[i] = true;
            return true;
        }

        public void StopPlaying()
        {
            _isPlaying = false;
            _currentFrame = 0.0f;
            _triggers.Clear();
            _tickHandler.RemoveListener(this);
            OnAnimationEnd?.Invoke();
        }

        public void Reset()
        {
            if(!_isPlaying)
                return;
            StopPlaying();
            currentAnimation = null;
        }

        public void Dispose() => Reset();

    }
}
