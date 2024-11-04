using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace AppiumTests;

public class AppiumTests : TestBase
{

    

    
    [TestMethod]
    public void Test1()
    {
         //driver.StartActivity("com.android.settings", ".Settings");
        driver.FindElement(By.XPath("//android.view.ViewGroup[@content-desc=\"cart badge\"]/android.widget.ImageView")).Click();
        Thread.Sleep(10 * 1000);
        	
//android.view.ViewGroup[@content-desc="open menu"]/android.widget.ImageView

//android.view.ViewGroup[@content-desc="sort button"]/android.widget.ImageView
        //Assert.Pass();
    }
}