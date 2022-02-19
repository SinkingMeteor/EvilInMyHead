using Sheldier.Factories;
using Sheldier.Item;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class ItemInstaller : MonoInstaller
    {
        [SerializeField] private ItemMap itemMap;

        public override void InstallBindings()
        {
            Container.Bind<ItemMap>().FromInstance(itemMap).AsSingle();
            Container.Bind<ItemSpawner>().AsSingle();
            Container.Bind<ItemFactory>().AsSingle();
        }
    }
}