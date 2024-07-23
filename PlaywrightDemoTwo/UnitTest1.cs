using PlaywrightDemoTwo.Config;
using PlaywrightDemoTwo.Driver;

namespace PlaywrightDemoTwo
{
    public class Tests
    {
        private PlaywrightDriver _driver;

        [SetUp]
        public async Task Setup()
        {
            TestSettings testSettings = new TestSettings
            {
                Channel = "chrome",
                DevTools = true,
                Headless = false,
                SlowMo = 1500,
                DriverType = DriverType.Chromium
            };

            _driver = new PlaywrightDriver(testSettings);
            await _driver.Page.Result.GotoAsync("http://eaapp.somee.com");
        }

        [Test]
        public async Task Test1()
        {
            await _driver.Page.Result.ClickAsync("text=Login");
        }

        [Test]
        public async Task LoginTest()
        {
            await _driver.Page.Result.ClickAsync("text=Login");
            await _driver.Page.Result.GetByLabel("Username").FillAsync("admin");
            await _driver.Page.Result.GetByLabel("Password").FillAsync("password");
        }

        //[Test]
        //public async Task LaunchingBrowserInAnotherOption()
        //{
        //    // This will launch playwright
        //    var playwrightDriver = await Playwright.CreateAsync();

        //    var browserOptions = new BrowserTypeLaunchOptions();
        //    browserOptions.Headless = false;
        //    browserOptions.Devtools = true;
        //    browserOptions.Channel = "chrome";

        //    // Invoking a Chromium driver
        //    var firefox = await playwrightDriver["chromium"].LaunchAsync(browserOptions);

        //    var browserContext = await firefox.NewContextAsync();

        //    var page = await browserContext.NewPageAsync();

        //    await page.GotoAsync("http://eaapp.somee.com");
        //}

        [TearDown]
        public async Task TearDown()
        {
            await _driver.Browser.Result.CloseAsync();
            await _driver.Browser.Result.DisposeAsync();
        }

    }
}