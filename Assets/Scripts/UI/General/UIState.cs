using System.Threading.Tasks;
using Sheldier.Common;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Sheldier.UI
{
    public class UIState : SerializedMonoBehaviour, ITickListener
    {
        public bool IsRequirePause => isRequirePause;

        [OdinSerialize] [ReadOnly] private IUIStateAnimationAppearing[] appearingAnimations;
        [OdinSerialize] [ReadOnly] private IUIStateAnimationDisappearing[] disappearingAnimations;
        [OdinSerialize] [ReadOnly] private IUIElement[] UIElements;
        
        [OdinSerialize] private UICanvas canvas;
        [SerializeField] private bool isRequirePause;
        
        private bool _isActivated;
        private TickHandler _tickHandler;
        private IInputProvider _inputProvider;

        public void Initialize()
        {
            for (int i = 0; i < appearingAnimations.Length; i++)
                appearingAnimations[i].Initialize();
            for (int i = 0; i < disappearingAnimations.Length; i++)
                disappearingAnimations[i].Initialize();
            for (int i = 0; i < UIElements.Length; i++)
                UIElements[i].Initialize(_inputProvider);                
        }

        [Inject]
        private void InjectDependencies(TickHandler tickHandler, IInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _tickHandler = tickHandler;
        }
        public void Tick()
        {
            for (int i = 0; i < UIElements.Length; i++)
                UIElements[i].Tick();    
        }

        public async void Show()
        {
            Activate();
            
            Task[] tasks = new Task[appearingAnimations.Length];
            for (int i = 0; i < appearingAnimations.Length; i++)
            {
                tasks[i] = appearingAnimations[i].PlayAnimation();
            }
            await Task.WhenAll(tasks);
            
            _isActivated = true;
        }

        public async void Hide()
        {
            Deactivate();
            
            Task[] tasks = new Task[disappearingAnimations.Length];
            for (int i = 0; i < disappearingAnimations.Length; i++)
            {
                tasks[i] = disappearingAnimations[i].PlayAnimation();
            }
            await Task.WhenAll(tasks);
            
            _isActivated = false;
        }

        public void KillAllAppearingAnimations()
        {
            if(_isActivated) return; 
            for (int i = 0; i < appearingAnimations.Length; i++)
            {
                appearingAnimations[i].Reset();
            }
        }
        public void KillAllDisapearingAnimations()
        {
            if(!_isActivated) return; 
            for (int i = 0; i < disappearingAnimations.Length; i++)
            {
                disappearingAnimations[i].Reset();
            }
        }
        public void Activate()
        {
            _tickHandler.AddListener(this);
            for (int i = 0; i < UIElements.Length; i++)
                UIElements[i].OnActivated();
            
            canvas.OnActivated();

        }

        public void Deactivate()
        {
            _tickHandler.RemoveListener(this);
            for (int i = 0; i < UIElements.Length; i++)
                UIElements[i].OnDeactivated();  
            
            canvas.OnDeactivated();
        }

        public void SetSortingOrder(int order)
        {
            canvas.SetSortingOrder(order);
        }
        public void Dispose()
        {
            for (int i = 0; i < UIElements.Length; i++)
                UIElements[i].Dispose();          
        }

        #if UNITY_EDITOR
        [Button]
        private void FindAllElements()
        {
            appearingAnimations = GetComponentsInChildren<IUIStateAnimationAppearing>();
            disappearingAnimations = GetComponentsInChildren<IUIStateAnimationDisappearing>();
            UIElements = GetComponentsInChildren<IUIElement>();
        }
        #endif

    }
}