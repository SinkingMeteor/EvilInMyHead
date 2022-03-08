using System;
using UnityEngine;
using Zenject;

namespace Sheldier.Common.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SimpleAnimator : MonoBehaviour, ITickListener
    {
        public bool IsPlaying => _isPlaying;

        public event Action OnAnimationEnd; 
        
        [SerializeField] private AnimationData currentAnimation;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private bool _playOnInitialize;
        
        private float _currentFrame;
        private bool _isPlaying;
        private TickHandler _tickHandler;

        private int FramesCount => currentAnimation.Frames.Length;
        
        public void Initialize()
        {
            if (_playOnInitialize && currentAnimation != null)
                Play();
        }
        [Inject]
        public void InjectDependencies(TickHandler tickHandler)
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
            _isPlaying = true;
            _tickHandler.AddListener(this);
        }

        public void Tick()
        {
            int frameCount = FramesCount;
            _currentFrame = (_currentFrame + Time.deltaTime * currentAnimation.FrameRate) % frameCount;
            int frameIndex = (int) _currentFrame;
            spriteRenderer.sprite = currentAnimation.Frames[frameIndex];
            if (!currentAnimation.IsLoop && frameIndex == frameCount - 1)
                StopPlaying();
        }
        public void StopPlaying()
        {
            _isPlaying = false;
            _currentFrame = 0.0f;
            _tickHandler.RemoveListener(this);
            OnAnimationEnd?.Invoke();
        }

        public void Reset()
        {
            StopPlaying();
            currentAnimation = null;
        }

        public void Dispose()
        {
            Reset();
            _tickHandler.RemoveListener(this);
        }
    }
}
