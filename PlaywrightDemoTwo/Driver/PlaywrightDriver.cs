using Microsoft.Playwright;
using PlaywrightDemoTwo.Config;

namespace PlaywrightDemoTwo.Driver
{
    public class PlaywrightDriver
    {

        private readonly AsyncTask<IBrowser> _browser;
        private readonly AsyncTask<IBrowserContext> _browserContext;
        private readonly TestSettings _testSettings;
        private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly AsyncTask<IPage> _page;

        public PlaywrightDriver(TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
        {
            _testSettings = testSettings;
            _playwrightDriverInitializer = playwrightDriverInitializer;
            _browser = new AsyncTask<IBrowser>(InitializePlaywrightAsync);
            _browserContext = new AsyncTask<IBrowserContext>(CreateBrowserContext);
            _page = new AsyncTask<IPage>(CreatePageAsync);
        }

        public Task<IPage> Page => _page.Value;
        public Task<IBrowser> Browser => _browser.Value;
        public Task<IBrowserContext> BrowserContext => _browserContext.Value;

        private async Task<IBrowser> InitializePlaywrightAsync()
        {
            return _testSettings.DriverType switch
            {
                DriverType.Chromium => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings),
                DriverType.Chrome => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
                DriverType.Edge => await _playwrightDriverInitializer.GetEdgeDriverAsync(_testSettings),
                DriverType.Firefox => await _playwrightDriverInitializer.GetFireFoxDriverAsync(_testSettings),
                _ => await _playwrightDriverInitializer.GetWebKitDriverAsync(_testSettings),
            };
        }

        private async Task<IBrowserContext> CreateBrowserContext()
        {
            return await (await _browser).NewContextAsync();
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await _browserContext).NewPageAsync();
        }
    }
}
