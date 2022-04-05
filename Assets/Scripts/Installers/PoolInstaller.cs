using Sheldier.Common.Pool;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class PoolInstaller : MonoInstaller<PoolInstaller>
    {
        [SerializeField] private ProjectilePool projectilePool;
        [SerializeField] private WeaponBlowPool weaponBlowPool;
        [SerializeField] private InventorySlotPool inventorySlotPool;
        [SerializeField] private SpeechCloudPool speechCloudPool;
        [SerializeField] private ChoiceSlotPool choiceSlotPool;
        [SerializeField] private UIHintPool uiHintPool;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProjectilePool>().FromInstance(projectilePool).AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponBlowPool>().FromInstance(weaponBlowPool).AsSingle();
            Container.BindInterfacesAndSelfTo<SpeechCloudPool>().FromInstance(speechCloudPool).AsSingle();
            Container.BindInterfacesAndSelfTo<InventorySlotPool>().FromInstance(inventorySlotPool).AsSingle();
            Container.BindInterfacesAndSelfTo<ChoiceSlotPool>().FromInstance(choiceSlotPool).AsSingle();
            Container.BindInterfacesAndSelfTo<UIHintPool>().FromInstance(uiHintPool).AsSingle();
        }
    }
}