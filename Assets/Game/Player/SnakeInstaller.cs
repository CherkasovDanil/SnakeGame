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
                .Bind<SnakeConfig>()
                .FromScriptableObjectResource("SnakeConfig")
                .AsSingle()
                .NonLazy();

                Container
                .BindInterfacesAndSelfTo<InputController>()
                .AsSingle()
                .NonLazy();
           
            Container
                .Bind<SnakeMovement>()
                .AsSingle()
                .NonLazy();
        }
    }
}