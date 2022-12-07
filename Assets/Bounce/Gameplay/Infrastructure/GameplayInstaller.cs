using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using JunityEngine.Maths.Runtime;
using UnityEngine;
using Zenject;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Infrastructure.Runtime
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var sketchBook = new Sketchbook();
            var bounds = new Bounds2D(Vector2.Zero, new Vector2(4));
            var area = new Area(sketchBook, bounds);

            Container.BindInstance(area).AsSingle();

            Container.Bind<DrawLine>().AsSingle().NonLazy();

            Container.Bind<DrawLineView>().FromComponentInHierarchy().AsSingle();
        }
    }
}