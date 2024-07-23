using Microsoft.Playwright;
using PlaywrightDemoTwo.Config;

namespace PlaywrightDemoTwo.Driver
{
    public class PlaywrightDriver
    {

        private readonly Task<IBrowser> _browser;
        private readonly Task<IBrowserContext> _browserContext;
        private readonly TestSettings _testSettings;
        private readonly Task<IPage> _page;

        public PlaywrightDriver(TestSettings testSettings)
        {
            _testSettings = testSettings;

            _browser = Task.Run(InitializePlaywrightAsync);
            _browserContext = Task.Run(CreateBrowserContext);
            _page = Task.Run(CreatePageAsync);
        }

        public IPage Page => _page.Result;
        public IBrowser Browser => _browser.Result;
        public IBrowserContext BrowserContext => _browserContext.Result;

        private async Task<IBrowser> InitializePlaywrightAsync()
        {
            return await GetBrowserAsync(_testSettings);
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            return await (await _browser).NewContextAsync();
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await _browserContext).NewPageAsync();
        }

        private async Task<IBrowser> GetBrowserAsync(TestSettings testSettings)
        {

            // This will launch playwright
            var playwrightDriver = await Playwright.CreateAsync();

            var browserOptions = new BrowserTypeLaunchOptions
            {
                Headless = testSettings.Headless,
                Devtools = testSettings.DevTools,
                SlowMo = testSettings.SlowMo,
                Channel = testSettings.Channel
            };

            return testSettings.DriverType switch
            {
                DriverType.Chromium => await playwrightDriver.Chromium.LaunchAsync(browserOptions),
                DriverType.Chrome => await playwrightDriver["chrome"].LaunchAsync(browserOptions),
                DriverType.Edge => await playwrightDriver["msedge"].LaunchAsync(browserOptions),
                DriverType.Firefox => await playwrightDriver.Firefox.LaunchAsync(browserOptions),
                _ => await playwrightDriver.Webkit.LaunchAsync(browserOptions)
            };
        }
    }
}
