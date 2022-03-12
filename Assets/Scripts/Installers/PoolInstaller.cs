using Sheldier.Common.Pool;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private ProjectilePool projectilePool;
        [SerializeField] private WeaponBlowPool weaponBlowPool;
        [SerializeField] private InventorySlotPool inventorySlotPool;
        [SerializeField] private UIHintPool uiHintPool;

        public override void InstallBindings()
        {
            Container.Bind<ProjectilePool>().FromInstance(projectilePool).AsSingle();
            Container.Bind<WeaponBlowPool>().FromInstance(weaponBlowPool).AsSingle();
            Container.Bind<InventorySlotPool>().FromInstance(inventorySlotPool).AsSingle();
            Container.Bind<UIHintPool>().FromInstance(uiHintPool).AsSingle();
        }
    }
}