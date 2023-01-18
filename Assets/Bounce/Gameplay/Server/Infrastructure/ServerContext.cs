using System.Threading;
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

            Container.Bind<MatchMaking>().AsSingle().NonLazy();

            Container.BindInstance(pluginLoadData.ClientManager).WhenInjectedInto<IClientManager>();
            Container.BindInstance(new CancellationTokenSource().Token);
            Container.BindMemoryPool<Match>()
                .FromSubContainerResolve()
                .ByInstaller<MatchInstaller>();
        } 
    }
}