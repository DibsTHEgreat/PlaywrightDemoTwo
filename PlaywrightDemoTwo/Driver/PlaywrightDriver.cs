using Microsoft.Playwright;
using PlaywrightDemoTwo.Config;

namespace PlaywrightDemoTwo.Driver
{
    public class PlaywrightDriver
    {
        public async Task<IPage> InitalizePlaywright(TestSettings testSettings)
        {
            var browser = await GetBrowserAsync(testSettings);
            var browserContext = await browser.NewContextAsync();
            var page = await browserContext.NewPageAsync();

            await page.GotoAsync("http://eaapp.somee.com");

            return page;
        }

        public async Task<IBrowser> GetBrowserAsync(TestSettings testSettings)
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
