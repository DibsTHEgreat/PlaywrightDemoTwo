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
                DevTools = true,
                Headless = false,
                SlowMo = 1500,
                DriverType = DriverType.Chromium
            };

            _driver = new PlaywrightDriver(testSettings);
            await _driver.Page.GotoAsync("http://eaapp.somee.com");
        }

        [Test]
        public async Task Test1()
        {
            await _driver.Page.ClickAsync("text=Login");
        }

        [Test]
        public async Task LoginTest()
        {
            await _driver.Page.ClickAsync("text=Login");
            await _driver.Page.GetByLabel("Username").FillAsync("admin");
            await _driver.Page.GetByLabel("Password").FillAsync("password");
        }

        [TearDown]
        public async Task TearDown()
        {
            await _driver.Browser.CloseAsync();
            await _driver.Browser.DisposeAsync();
        }

    }
}