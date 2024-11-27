using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

public class LeftPanel : BaseOperations
{
    public LeftPanel(AndroidDriver driver) : base(driver)
    {
    }

    public void OpenLeftMenu()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"open menu\"]/android.widget.ImageView";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void ClickCatalog()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"menu item catalog\"]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void OpenCatalog()
    {
        OpenLeftMenu();
        ClickCatalog();
    }

    public void ClickLogIn()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"menu item log in\"]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void ClickLogOut()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"menu item log out\"]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void OpenLogin()
    {
        OpenLeftMenu();
        ClickLogIn();
    }

    public void OpenLogout()
    {
        OpenLeftMenu();
        ClickLogOut();
    }
}