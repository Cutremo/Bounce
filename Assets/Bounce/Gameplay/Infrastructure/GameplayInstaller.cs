using System.Collections.Generic;
using System.Threading;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using Bounce.Gameplay.Input.Runtime;
using Bounce.Gameplay.Presentation.Runtime;
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
            var sketchBook0 = new Sketchbook {MaxTrampolineLength = 3};
            var bounds0 = new Bounds2D(new Vector2(-5, -8), new Vector2(5, -2));
            var area0 = new Area(sketchBook0, bounds0) {MinTrampolineSize = 1f};
            
            var player1 = new Player("player1");
            var sketchBook1 = new Sketchbook {MaxTrampolineLength = 3};
            var bounds1 = new Bounds2D(new Vector2(-5, 2), new Vector2(5, 8));
            var area1 = new Area(sketchBook1, bounds1) {MinTrampolineSize = 1f};
            
            var players = new List<Player>();
            players.Add(player0);
            players.Add(player1);

            var field = new Field(new Bounds2D(new Vector2(-5, -8), new Vector2(5, 8)));
            var pitch = new Pitch(field, new Dictionary<Player, Area>() { { player0, area0 }, { player1, area1 } });
            var game = new Game(pitch, new Dictionary<Player, Area>() {{player0, area0}, {player1, area1}}, 1);
            
            Container.BindInstance(game).AsSingle();

            Container.Bind<PlayersController>().AsSingle().NonLazy();

            Container.Bind<Application.Runtime.Gameplay>().AsSingle().NonLazy();
            
            Container.Bind<DrawTrampoline>().FromSubContainerResolve().ByMethod(subContainer => InstallPlayer(player0, subContainer)).AsTransient();
            Container.Bind<DrawTrampoline>().FromSubContainerResolve().ByMethod(subContainer => InstallPlayer(player1, subContainer)).AsTransient();
            Container.Bind<DropBall>().AsSingle();
            Container.Bind<BallsView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MoveBall>().AsSingle();
            Container.BindInstance(new CancellationTokenSource());
            Container.Bind<EndGame>().AsSingle();
        }

        void InstallPlayer(Player player, DiContainer subcontainer)
        {
            subcontainer.BindInstance(player);
            var playerInstance = Instantiate(playerPrefab);
            playerInstance.name = player.Id;
            subcontainer.Bind<DrawTrampoline>().AsSingle();
            subcontainer.Bind<DrawTrampolineInput>().FromInstance(playerInstance.GetComponent<DrawTrampolineInput>()).AsSingle();
            subcontainer.Bind<DrawTrampolineView>().FromInstance(playerInstance.GetComponent<DrawTrampolineView>()).AsSingle();
            subcontainer.Bind<TrampolineView>().FromInstance(playerInstance.GetComponent<TrampolineView>()).AsSingle();
        }
    }
}