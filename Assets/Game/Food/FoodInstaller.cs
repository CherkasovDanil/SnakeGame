using Zenject;

namespace Game.Food
{
    public class FoodInstaller : Installer<FoodInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<FoodController>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<Food, Food.Pool>()
                .WithInitialSize(2)
                .FromComponentInNewPrefabResource("Food")
                .UnderTransformGroup("Foods");
        }
    }
}