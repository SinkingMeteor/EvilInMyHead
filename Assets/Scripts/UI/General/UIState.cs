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
        public bool IsActivated => isActivated;
        public bool IsRequirePause => isRequirePause;

        [OdinSerialize] private IUIStateAnimationAppearing[] appearingAnimations;
        [OdinSerialize] private IUIStateAnimationDisappearing[] disappearingAnimations;
        [OdinSerialize] [ReadOnly] private IVisualUIElement[] visualUIElements;
        
        [OdinSerialize] private UICanvas canvas;
        [SerializeField] private bool isRequirePause;
        
        private bool isActivated;
        private TickHandler _tickHandler;

        public void Initialize()
        {
            for (int i = 0; i < visualUIElements.Length; i++)
                visualUIElements[i].Initialize();                
        }

        [Inject]
        private void InjectDependencies(TickHandler tickHandler)
        {
            _tickHandler = tickHandler;
        }
        public void Tick()
        {
            for (int i = 0; i < visualUIElements.Length; i++)
                visualUIElements[i].Tick();    
        }
        public async void Activate()
        {
            _tickHandler.AddListener(this);
            
            for (int i = 0; i < visualUIElements.Length; i++)
                visualUIElements[i].Activate();
            
            Task[] tasks = new Task[appearingAnimations.Length];
            for (int i = 0; i < appearingAnimations.Length; i++)
            {
                tasks[i] = appearingAnimations[i].PlayAnimation();
            }
            await Task.WhenAll(tasks);
            

            
            isActivated = true;
            canvas.OnActivated();
        }

        public async void Deactivate()
        {
            _tickHandler.RemoveListener(this);
            for (int i = 0; i < visualUIElements.Length; i++)
                visualUIElements[i].Deactivate();  
            
            Task[] tasks = new Task[disappearingAnimations.Length];
            for (int i = 0; i < disappearingAnimations.Length; i++)
            {
                tasks[i] = disappearingAnimations[i].PlayAnimation();
            }
            await Task.WhenAll(tasks);
            
            isActivated = false;
            canvas.OnDeactivated();
        }

        public void SetSortingOrder(int order)
        {
            canvas.SetSortingOrder(order);
        }
        public void Dispose()
        {
            for (int i = 0; i < visualUIElements.Length; i++)
                visualUIElements[i].Dispose();          
        }

        [Button]
        private void FindAllElements()
        {
            appearingAnimations = GetComponentsInChildren<IUIStateAnimationAppearing>();
            disappearingAnimations = GetComponentsInChildren<IUIStateAnimationDisappearing>();
            visualUIElements = GetComponentsInChildren<IVisualUIElement>();
        }


    }
}