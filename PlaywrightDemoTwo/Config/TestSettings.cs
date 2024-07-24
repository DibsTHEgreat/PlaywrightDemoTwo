
namespace PlaywrightDemoTwo.Config
{
    public class TestSettings
    {
        public string[] Args { get; set; }

        public float Timeout { get; set; }

        public bool Headless { get; set; }

        public DriverType DriverType { get; set; }

        public bool DevTools { get; set; }

        public int SlowMo { get; set; }
    }

    public enum DriverType
    {
        Chromium,
        Firefox,
        Edge,
        Chrome,
        Webkit
    }
}
