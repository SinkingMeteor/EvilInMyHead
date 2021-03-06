using System;
using System.Collections.Generic;
using Sheldier.Common;
using Sheldier.Common.Localization;
using Sheldier.Common.Pool;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public abstract class UIHintController<T, V> : SerializedMonoBehaviour, IUIInitializable, IUIActivatable, ILocalizationListener, IDeviceListener
    {
        protected abstract IUIItemSwitcher<T> ItemSwitcher { get; }

        [OdinSerialize] private IUIStateAnimationAppearing appearingAnimation;
        [OdinSerialize] private IUIStateAnimationDisappearing disappearingAnimation;

        [SerializeField] protected RectTransform parentContainer;

        protected Dictionary<V, Func<string, bool>> _conditionDictionary;
        protected Dictionary<V, Action> _performDictionary;
        protected Dictionary<V, UIHint> _hintsCollection;
        protected string _currentItemId;
        
        protected UIHintPool _uiHintPool;
        protected ILocalizationProvider _localizationProvider;
        protected IInputBindIconProvider _bindIconProvider;

        public virtual void Initialize()
        {
            appearingAnimation.Initialize();
            disappearingAnimation.Initialize();
        }

        [Inject]
        private void InjectDependencies(UIHintPool uiHintPool, ILocalizationProvider localizationProvider, IInputBindIconProvider bindIconProvider)
        {
            _bindIconProvider = bindIconProvider;
            _localizationProvider = localizationProvider;
            _uiHintPool = uiHintPool;
        }
        public abstract void OnLanguageChanged();
        public abstract void OnDeviceChanged();
        protected abstract void CreateHint(V conditionKey);
        public virtual void OnActivated()
        {
            appearingAnimation.PlayAnimation();
            _localizationProvider.AddListener(this);
            _bindIconProvider.AddListener(this);
            ItemSwitcher.OnCurrentItemChanged += OnItemChanged;
        }

        public virtual void OnDeactivated()
        {
            RemoveAllHints();
            _localizationProvider.RemoveListener(this);
            _bindIconProvider.RemoveListener(this);
            disappearingAnimation.PlayAnimation();
            ItemSwitcher.OnCurrentItemChanged -= OnItemChanged;
        }

        public void Dispose()
        {
        }
        private void OnItemChanged(string id)
        {
            if (id == _currentItemId) return;
            if (id == null)
            {
                _currentItemId = default;
                RemoveAllHints();
                return;
            }
            _currentItemId = id;

            foreach (var condition in _conditionDictionary)
            {
                bool isConditionPerformed = condition.Value(_currentItemId);
                if (!isConditionPerformed)
                    RemoveHint(condition.Key);
                else if (!_hintsCollection.ContainsKey(condition.Key))
                    CreateHint(condition.Key);
            }
        }
        private void RemoveAllHints()
        {
            foreach (var uiHint in _hintsCollection)
            {
                uiHint.Value.Deactivate();
            }
            _hintsCollection.Clear();
        }
        private void RemoveHint(V conditionKey)
        {
            if(!_hintsCollection.ContainsKey(conditionKey)) return;
            var hint = _hintsCollection[conditionKey];
            _hintsCollection.Remove(conditionKey);
            hint.Deactivate();
        }

    }
}