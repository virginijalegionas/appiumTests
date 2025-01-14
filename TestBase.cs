using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace appiumTests
{
    [TestClass]
    public class TestBase
    {
        public static string appPackage;
        public static string appActivity;
        public static string udid;
        public static string userName;
        public static string userPassword;
        public AndroidDriver driver;

        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            appPackage = testContext.Properties["appPackage"] as string;
            appActivity = testContext.Properties["appActivity"] as string;
            udid = testContext.Properties["udid"] as string;
            userName = testContext.Properties["userName"] as string;
            userPassword = testContext.Properties["userPassword"] as string;
        }

        public void StartDriver()
        {
            Uri serverUri = new Uri("http://127.0.0.1:4723/");
            AppiumOptions driverOptions = new AppiumOptions()
            {
                AutomationName = AutomationName.AndroidUIAutomator2,
                PlatformName = "Android",
                DeviceName = "My Phone",
                PlatformVersion = "9",
            };

            driverOptions.AddAdditionalAppiumOption("appPackage", appPackage);
            driverOptions.AddAdditionalAppiumOption("appActivity", appActivity);
            driverOptions.AddAdditionalAppiumOption("udid", udid);
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