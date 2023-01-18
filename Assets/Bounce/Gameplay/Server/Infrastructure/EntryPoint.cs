using System;
using System.Collections;
using Bounce.Gameplay.Domain.Runtime;
using DarkRift.Server;

namespace Bounce.Server.Runtime
{
    public class EntryPoint : Plugin
    {
        readonly ServerContext serverContext;

        public EntryPoint(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            serverContext = new ServerContext(pluginLoadData);
            serverContext.InstallBindings();
        }

        public override Version Version => new Version(0, 0, 0);
        public override bool ThreadSafe => true;
    }
}


