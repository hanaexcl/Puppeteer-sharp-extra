using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuppeteerExtraSharp.Plugins.ExtraStealth.Evasions;
using PuppeteerSharp;

namespace PuppeteerExtraSharp.Plugins.ExtraStealth
{
    public class StealthPlugin : PuppeteerExtraPlugin
    {
        private readonly IPuppeteerExtraPluginOptions[] _options;
        private readonly List<PuppeteerExtraPlugin> _standardEvasions;

        public StealthPlugin(params IPuppeteerExtraPluginOptions[] options) : base("stealth")
        {
            _options = options;
            _standardEvasions = GetStandardEvasions();
        }

        private List<PuppeteerExtraPlugin> GetStandardEvasions()
        {
            return new List<PuppeteerExtraPlugin>()
            {
                new ChromeApp(),
                new ChromeSci(),
                new LoadTimes(),
                new ChromeRuntime(),
                new DefaultArgs(),
                new Codec(),
                new HardwareConcurrency(GetOptionByType<StealthHardwareConcurrencyOptions>()),
                new Languages(GetOptionByType<StealthLanguagesOptions>()),
                new Permissions(),
                new PluginEvasion(),
                new WebDriver(),
                new Vendor(GetOptionByType<StealthVendorSettings>()),
                new OutDimensions(),
                //new UserAgent(),
                //new WebGl(GetOptionByType<StealthWebGLOptions>()),
                //new StackTrace(),
                //new ContentWindow(),
                //new SourceUrl()
            };
        }

        public override ICollection<PuppeteerExtraPlugin> GetDependencies() => _standardEvasions;

        public override async Task OnPageCreated(Page page)
        {
            var utilsScript = Utils.GetScript("Utils.js");
            await page.EvaluateExpressionOnNewDocumentAsync(utilsScript);
        }

        private T GetOptionByType<T>() where T : IPuppeteerExtraPluginOptions
        {
            return _options.OfType<T>().FirstOrDefault();
        }

        public void RemoveEvasionByType<T>() where T : PuppeteerExtraPlugin
        {
            _standardEvasions.RemoveAll(ev => ev is T);
        }
    }
}