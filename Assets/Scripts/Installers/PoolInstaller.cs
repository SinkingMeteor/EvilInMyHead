using Sheldier.Common.Pool;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private ProjectilePool projectilePool;
        [SerializeField] private WeaponBlowPool weaponBlowPool;

        public override void InstallBindings()
        {
            Container.Bind<ProjectilePool>().FromInstance(projectilePool).AsSingle();
            Container.Bind<WeaponBlowPool>().FromInstance(weaponBlowPool).AsSingle();
            Container.Bind<PoolInstaller>().FromInstance(this).AsSingle();
        }

        public void InjectPoolObject<T>(T obj) where T : MonoBehaviour
        {
            Container.InjectGameObject(obj.transform.gameObject);
        }
    }
}