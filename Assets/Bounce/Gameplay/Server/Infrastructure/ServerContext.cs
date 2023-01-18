using Bounce.Gameplay.Application.Runtime;
using DarkRift.Server;
using Zenject;

namespace Bounce.Server.Runtime
{
    public class ServerContext
    {
        readonly PluginLoadData pluginLoadData;

        public ServerContext(PluginLoadData pluginLoadData)
        {
            this.pluginLoadData = pluginLoadData;
        }

        DiContainer Container { get; set; }

        public void InstallBindings()
        {
            Container = new DiContainer();

            Container.BindInstance(pluginLoadData.ClientManager).WhenInjectedInto<IClientManager>();
            Container.Bind<MatchMaking>().AsSingle().NonLazy();
            Container.BindMemoryPool<Match, MatchFactory>()
                .FromSubContainerResolve()
                .ByMethod(InstallGame);
        }

        void InstallGame(DiContainer obj)
        {
            
        }
    }

    public class MatchFactory : MemoryPool<Match>
    {
        
    }
}