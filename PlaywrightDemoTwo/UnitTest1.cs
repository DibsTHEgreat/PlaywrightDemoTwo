using Microsoft.Playwright;
using PlaywrightDemoTwo.Driver;

namespace PlaywrightDemoTwo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            PlaywrightDriver driver = new PlaywrightDriver();
            IPage page = await driver.InitalizePlaywright();

            await page.ClickAsync("text=Login");
        }

        [Test]
        public async Task LaunchingBrowserInAnotherOption()
        {
            // This will launch playwright
            var playwrightDriver = await Playwright.CreateAsync();

            var browserOptions = new BrowserTypeLaunchOptions();
            browserOptions.Headless = false;
            browserOptions.Devtools = true;
            browserOptions.Channel = "chrome";

            // Invoking a Chromium driver
            var firefox = await playwrightDriver["chromium"].LaunchAsync(browserOptions);

            var browserContext = await firefox.NewContextAsync();

            var page = await browserContext.NewPageAsync();

            await page.GotoAsync("http://eaapp.somee.com");
        }
    }
}