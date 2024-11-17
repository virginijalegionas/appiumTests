using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

public class Logout : BaseOperations
{
    public Logout(AndroidDriver driver) : base(driver)
    {
    }

    public void ClickCancel()
    {
        GetElement(By.XPath("//android.widget.Button[@text=\"CANCEL\"]"), 5).Click();
    }

    public void ClickLogOut()
    {
        GetElement(By.XPath("//android.widget.Button[@text=\"LOG OUT\"]"), 5).Click();
    }

    public void ClickOK()
    {
        GetElement(By.XPath("//android.widget.Button[@text=\"OK\"]"), 5).Click();
    }

    public string GetLogOutMessage()
    {
        return GetElement(By.XPath("//android.widget.TextView[@resource-id=\"android:id/alertTitle\"]"), 5).Text;
    }

    public void LogoutUser()
    {
        ClickLogOut();
        ClickOK();
    }
}