using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class SnakeInstaller :  Installer<SnakeInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<VectorDirectionController>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindFactory<Snake, Snake.Factory>()
                .FromComponentInNewPrefabResource("Player");
            
            Container
                .BindFactory<SnakeMovePosition, SnakeBody, SnakeBody.Factory>()
                .FromComponentInNewPrefabResource("Body")
                .UnderTransformGroup("BodyParts");;

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
                .Bind<SnakeController>()
                .AsSingle()
                .NonLazy();
           
            Container
                .Bind<SnakeMovement>()
                .AsSingle()
                .NonLazy();

        }
    }
}