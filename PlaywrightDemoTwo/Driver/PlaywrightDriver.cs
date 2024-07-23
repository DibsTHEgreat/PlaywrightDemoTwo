using Microsoft.Playwright;
using PlaywrightDemoTwo.Config;

namespace PlaywrightDemoTwo.Driver
{
    public class PlaywrightDriver
    {

        public Task<IBrowser> Browser;
        public Task<IBrowserContext> BrowserContext;
        private readonly TestSettings _testSettings;
        public Task<IPage> Page;

        public PlaywrightDriver(TestSettings testSettings)
        {
            _testSettings = testSettings;

            Browser = Task.Run(InitializePlaywrightAsync);
            BrowserContext = Task.Run(CreateBrowserContext);
            Page = Task.Run(CreatePageAsync);
        }

        private async Task<IBrowser> InitializePlaywrightAsync()
        {
            return await GetBrowserAsync(_testSettings);
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            return await (await Browser).NewContextAsync();
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await BrowserContext).NewPageAsync();
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
