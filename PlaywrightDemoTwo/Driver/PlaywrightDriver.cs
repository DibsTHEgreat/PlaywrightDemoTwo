using Microsoft.Playwright;

namespace PlaywrightDemoTwo.Driver
{
    public class PlaywrightDriver
    {
        public async Task<IPage> InitalizePlaywright()
        {
            // This will launch playwright
            var playwrightDriver = await Playwright.CreateAsync();

            var browserOptions = new BrowserTypeLaunchOptions();
            browserOptions.Headless = false;
            browserOptions.Devtools = true;
            browserOptions.SlowMo = 1500;
            browserOptions.Channel = "Chrome";

            // Invoking a Chromium driver
            var chromium = await playwrightDriver.Chromium.LaunchAsync(browserOptions);

            var browserContext = await chromium.NewContextAsync();

            var page = await browserContext.NewPageAsync();

            await page.GotoAsync("http://eaapp.somee.com");

            return page;
        }
    }
}
