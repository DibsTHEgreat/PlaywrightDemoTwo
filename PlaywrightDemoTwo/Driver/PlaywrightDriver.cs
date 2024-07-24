using Microsoft.Playwright;
using PlaywrightDemoTwo.Config;

namespace PlaywrightDemoTwo.Driver
{
    public class PlaywrightDriver
    {

        private readonly Lazy<Task<IBrowser>> _browser;
        private readonly Lazy<Task<IBrowserContext>> _browserContext;
        private readonly TestSettings _testSettings;
        private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly Lazy<Task<IPage>> _page;

        public PlaywrightDriver(TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = testSettings;
            _playwrightDriverInitializer = playwrightDriverInitializer;
            _browser = new Lazy<Task<IBrowser>>(InitializePlaywrightAsync);
            _browserContext = new Lazy<Task<IBrowserContext>>(CreateBrowserContext);
            _page = new Lazy<Task<IPage>>(CreatePageAsync);
        }

        public IPage Page => _page.Value.Result;
        public IBrowser Browser => _browser.Value.Result;
        public IBrowserContext BrowserContext => _browserContext.Value.Result;

        private async Task<IBrowser> InitializePlaywrightAsync()
        {
            return _testSettings.DriverType switch
            {
                DriverType.Chromium => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings),
                DriverType.Chrome => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
                DriverType.Edge => await _playwrightDriverInitializer.GetEdgeDriverAsync(_testSettings),
                DriverType.Firefox => await _playwrightDriverInitializer.GetFireFoxDriverAsync(_testSettings),
                _ => await _playwrightDriverInitializer.GetWebKitDriverAsync(_testSettings),
            };
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            return await (await _browser.Value).NewContextAsync();
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await _browserContext.Value).NewPageAsync();
        }
    }
}
