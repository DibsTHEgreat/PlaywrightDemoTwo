using Microsoft.Playwright;
using PlaywrightDemoTwo.Config;

namespace PlaywrightDemoTwo.Driver
{
    public class PlaywrightDriver
    {

        public IBrowser Browser;
        public IBrowserContext BrowserContext;
        private readonly TestSettings _testSettings;
        public Task<IPage> Page;

        public PlaywrightDriver(TestSettings testSettings)
        {
            _testSettings = testSettings;

            Page = Task.Run(InitializePlaywrightAsync);
        }

        private async Task<IPage> InitializePlaywrightAsync()
        {
            Browser = await GetBrowserAsync(_testSettings);
            BrowserContext = await Browser.NewContextAsync();
            var page = await BrowserContext.NewPageAsync();

            await page.GotoAsync("http://eaapp.somee.com");

            return page;
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
