using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace appiumTests
{
    public class TestBase
    {
        public AndroidDriver driver;

        public void StartDriver()
        {
            Uri serverUri = new Uri("http://127.0.0.1:4723/");
            AppiumOptions driverOptions = new AppiumOptions()
            {
                AutomationName = AutomationName.AndroidUIAutomator2,
                PlatformName = "Android",
                DeviceName = "Mi A1",
                PlatformVersion = "9",
            };

            driverOptions.AddAdditionalAppiumOption("appPackage", "com.saucelabs.mydemoapp.rn");
            driverOptions.AddAdditionalAppiumOption("appActivity", ".MainActivity");
            driverOptions.AddAdditionalAppiumOption("udid", "28f136550704");
            driverOptions.AddAdditionalAppiumOption("shouldTerminateApp", true);
            driverOptions.AddAdditionalAppiumOption("allowInvisibleElements", true);
            // NoReset assumes the app com.google.android is preinstalled on the emulator
            driverOptions.AddAdditionalAppiumOption("noReset", true);
            driver = new AndroidDriver(serverUri, driverOptions, TimeSpan.FromSeconds(180));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        [TestInitialize]
        public void AutoStartDriver()
        {
            StartDriver();
        }

        [TestCleanup]
        public void CleanUp()
        {
            //Empty Basket
            Basket basket = new Basket(driver);
            basket.OpenBasket();
            basket.EmptyBasket();
            //Log out from app
            LeftPanel leftPanel = new LeftPanel(driver);
            leftPanel.OpenLogout();
            Logout logout = new Logout(driver);
            logout.LogoutUser();
            driver.Quit();
        }
    }
}