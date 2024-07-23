using Microsoft.Playwright;
using PlaywrightDemoTwo.Config;

namespace PlaywrightDemoTwo.Driver
{
    public class PlaywrightDriver
    {
        public async Task<IPage> InitalizePlaywright(TestSettings testSettings)
        {
            // This will launch playwright
            var playwrightDriver = await Playwright.CreateAsync();

            var browserOptions = new BrowserTypeLaunchOptions();
            browserOptions.Headless = testSettings.Headless;
            browserOptions.Devtools = testSettings.DevTools;
            browserOptions.SlowMo = testSettings.SlowMo;
            browserOptions.Channel = testSettings.Channel;

            // Invoking a Chromium driver
            var chromium = await playwrightDriver.Chromium.LaunchAsync(browserOptions);

            var browserContext = await chromium.NewContextAsync();

            var page = await browserContext.NewPageAsync();

            await page.GotoAsync("http://eaapp.somee.com");

            return page;
        }
    }
}
