using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace PuppeteerExtraSharp.Plugins.ExtraStealth.Evasions
{
    internal class DefaultArgs : PuppeteerExtraPlugin
    {
        public DefaultArgs() : base("stealth-defaultArgs") { }

        public override List<PluginRequirements> Requirements { get; set; } = new()
        {
            PluginRequirements.RunLast
        };

        public override void BeforeLaunch(LaunchOptions options)
        {
            if (options.IgnoreDefaultArgs == true) return;

            string[] argsToIgnore = {
                  "--disable-extensions",
                  "--disable-default-apps",
                  "--disable-component-extensions-with-background-pages"
            };

            options.IgnoredDefaultArgs = argsToIgnore;
        }
    }
}