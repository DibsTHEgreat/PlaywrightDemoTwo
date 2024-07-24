using PlaywrightDemoTwo.Config;
using PlaywrightDemoTwo.Driver;

namespace PlaywrightDemoTwo
{
    public class Tests
    {
        private PlaywrightDriver _driver;
        private PlaywrightDriverInitializer _playwrightDriverinitializer;

        [SetUp]
        public void Setup()
        {
            TestSettings testSettings = new TestSettings
            {
                DevTools = true,
                Headless = false,
                SlowMo = 500,
                DriverType = DriverType.Chromium
            };

            _playwrightDriverinitializer = new PlaywrightDriverInitializer();

            _driver = new PlaywrightDriver(testSettings, _playwrightDriverinitializer);
        }

        [Test]
        public async Task Test1()
        {
            var page = await _driver.Page;
            await page.GotoAsync("http://eaapp.somee.com");
            await page.ClickAsync("text=Login");
        }

        [Test]
        public async Task LoginTest()
        {
            var page = await _driver.Page;
            await page.GotoAsync("http://eaapp.somee.com");
            await page.ClickAsync("text=Login");
            await page.GetByLabel("Username").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");
        }

        [TearDown]
        public async Task TearDown()
        {
            var browser = await _driver.Browser;
            await browser.CloseAsync();
            await browser.DisposeAsync();
        }

    }
}