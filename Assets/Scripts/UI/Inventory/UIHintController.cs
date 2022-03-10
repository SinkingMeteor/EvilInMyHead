using System;
using System.Collections.Generic;
using Sheldier.Common;
using Sheldier.Common.Pool;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public abstract class UIHintController<T, V> : SerializedMonoBehaviour, IUIInitializable, IUIActivatable
    {
        protected abstract IUIItemSwitcher<T> ItemSwitcher { get; }

        [OdinSerialize] private IUIStateAnimationAppearing appearingAnimation;
        [OdinSerialize] private IUIStateAnimationDisappearing disappearingAnimation;

        [SerializeField] protected RectTransform parentContainer;

        protected Dictionary<V, Func<T, bool>> _conditionDictionary;
        protected Dictionary<V, Action> _performDictionary;
        protected Dictionary<V, UIHint> _hintsCollection;
        protected T _currentItem;
        
        protected IUIInputProvider _uiInputProvider;
        protected UIHintPool _uiHintPool;

        public virtual void Initialize(IInputProvider inputProvider)
        {
            appearingAnimation.Initialize();
            disappearingAnimation.Initialize();
        }

        [Inject]
        private void InjectDependencies(IUIInputProvider uiInputProvider, UIHintPool uiHintPool)
        {
            _uiHintPool = uiHintPool;
            _uiInputProvider = uiInputProvider;
        }

        public virtual void OnActivated()
        {
            appearingAnimation.PlayAnimation();
            ItemSwitcher.OnCurrentItemChanged += OnItemChanged;
        }

        private void OnItemChanged(T item)
        {
            if (Equals(item, _currentItem)) return;
            _currentItem = item;

            foreach (var condition in _conditionDictionary)
            {
                bool isConditionPerformed = condition.Value(_currentItem);
                if (!isConditionPerformed)
                    RemoveHint(condition.Key);
                else if (!_hintsCollection.ContainsKey(condition.Key))
                    CreateHint(condition.Key);
            }
        }

        protected abstract void CreateHint(V conditionKey);

        private void RemoveHint(V conditionKey)
        {
            if(!_hintsCollection.ContainsKey(conditionKey)) return;
            var hint = _hintsCollection[conditionKey];
            _hintsCollection.Remove(conditionKey);
            hint.Deactivate();
        }

        public virtual void OnDeactivated()
        {
            disappearingAnimation.PlayAnimation();
            ItemSwitcher.OnCurrentItemChanged -= OnItemChanged;
        }

        public void Dispose()
        {
        }


        
    }
}