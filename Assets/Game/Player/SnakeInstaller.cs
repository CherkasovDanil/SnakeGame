using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class SnakeInstaller :  Installer<SnakeInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<Snake, Snake.Factory>()
                .FromComponentInNewPrefabResource("Player");
           
            Container
                .BindInterfacesAndSelfTo<SnakeMovement>()
                .AsSingle()
                .NonLazy();
        }
    }
}