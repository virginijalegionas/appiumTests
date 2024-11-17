using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

public class Login : BaseOperations
{
    public Login(AndroidDriver driver) : base(driver)
    {
    }

    public void InputUserName(string userName)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Username input field\"]";
        InputTextField(By.XPath(xpath), userName);
    }

    public void InputPassword(string password)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Password input field\"]";
        InputTextField(By.XPath(xpath), password);
    }

    public void ClickLogin()
    {
        GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"Login button\"]"), 5).Click();
    }

    public void LoginUser(string userName, string password)
    {
        InputUserName(userName);
        InputPassword(password);
        ClickLogin();
    }
}