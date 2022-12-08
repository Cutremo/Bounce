using System.Collections.Generic;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
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
            var player = new Player();
            var sketchBook = new Sketchbook();
            var bounds = new Bounds2D(Vector2.Zero, new Vector2(4));
            var area = new Area(sketchBook, bounds);
            var game = new Game(new Dictionary<Player, Area>() {{player, area}}, 1);
            
            Container.BindInstance(game).AsSingle();
            Container.BindInstance(player).AsSingle();

            Container.Bind<DrawLine>().AsSingle().NonLazy();
            Container.Bind<Application.Runtime.Gameplay>().AsSingle();

            Container.Bind<DrawLineInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DrawLineView>().FromComponentInHierarchy().AsSingle();
        }
    }
}