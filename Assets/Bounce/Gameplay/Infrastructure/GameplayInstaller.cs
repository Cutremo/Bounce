using System.Collections.Generic;
using System.Threading;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
using Bounce.Gameplay.Presentation.Runtime;
using Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime;
using JunityEngine.Maths.Runtime;
using UnityEngine;
using Zenject;
using BallsView = Bounce.Gameplay.Application.Runtime.BallsView;
using Vector2 = JunityEngine.Maths.Runtime.Vector2;

namespace Bounce.Gameplay.Infrastructure.Runtime
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] GameObject playerPrefab;
        public override void InstallBindings()
        {
            var player0 = new Player("player0");
            var bounds0 = new Bounds2D(new Vector2(-5, -9), new Vector2(5, -2));
            var area0 = new Area(bounds0, Vector2.Down, 1f, 3f, 1);
            
            var player1 = new Player("player1");
            var bounds1 = new Bounds2D(new Vector2(-5, 2), new Vector2(5, 9));
            var area1 = new Area(bounds1, Vector2.Up, 1f, 3f, 1);
            
            var players = new List<Player>();
            players.Add(player0);
            players.Add(player1);

            var field = new Field(new Bounds2D(new Vector2(-5, -8), new Vector2(5, 8)));
            var pitch = new Pitch(field, new Dictionary<Player, Area>() { { player0, area0 }, { player1, area1 } });
            var game = new Game(pitch, new[] {player0, player1}, 5);
            
            Container.BindInstance(game).AsSingle();

            Container.Bind<PlayerDrawing>().AsSingle().NonLazy();

            Container.BindInstance(new Ball(pitch.Center, Vector2.Down, 1f) { Speed = 4 });
            Container.Bind<Application.Runtime.Gameplay>().AsSingle().NonLazy();
            Container.Bind<DrawTrampoline>().FromSubContainerResolve().ByNewPrefabMethod(playerPrefab, subContainer => InstallPlayer(player0, area0, subContainer)).AsCached().NonLazy();
            Container.Bind<DrawTrampoline>().FromSubContainerResolve().ByNewPrefabMethod(playerPrefab,subContainer => InstallPlayer(player1, area1, subContainer)).AsCached().NonLazy();
            Container.Bind<BallRef>().AsSingle();
            Container.Bind<BallsView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MoveBall>().AsSingle();
            Container.BindInstance(new CancellationTokenSource());
            Container.Bind<EndGame>().AsSingle();
            Container.Bind<PointController>().AsSingle();
            Container.Bind<ScoreView>().To<UnityScoreView>().AsSingle();
            Container.Bind<PointsText>().FromComponentsInHierarchy().AsCached().Lazy();
        }

        void InstallPlayer(Player player, Area area, DiContainer subcontainer)
        {
            subcontainer.DefaultParent.gameObject.name = player.Id;
            subcontainer.BindInstance(player);
            subcontainer.BindInstance(area);
            subcontainer.Bind<DrawTrampoline>().AsSingle();
            subcontainer.Bind<DrawTrampolineInput>().FromComponentInHierarchy().AsSingle();
            subcontainer.Bind<SketchbookView>().FromComponentInHierarchy().AsSingle();
            subcontainer.Bind<TrampolinesView>().FromComponentInHierarchy().AsSingle();
            subcontainer.Bind<PointsText>().FromComponentInHierarchy().AsSingle();
        }
    }
}