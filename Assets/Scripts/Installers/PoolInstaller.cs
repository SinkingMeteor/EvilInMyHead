﻿using Sheldier.Common.Pool;
using UnityEngine;
using Zenject;

namespace Sheldier.Installers
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private ProjectilePool projectilePool;

        public override void InstallBindings()
        {
            Container.Bind<ProjectilePool>().FromInstance(projectilePool).AsSingle();
        }
    }
}