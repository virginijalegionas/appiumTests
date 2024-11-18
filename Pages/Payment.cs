using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

public class Payment : BaseOperations
{
    public Payment(AndroidDriver driver) : base(driver)
    {
    }

    public void InputFullName(string fullName)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Full Name* input field\"]";
        InputTextField(By.XPath(xpath), fullName);
    }

    public void InputCardNumber(string cardNumber)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Card Number* input field\"]";
        InputTextField(By.XPath(xpath), cardNumber);
    }

    public void InputExpirationDate(DateOnly expDate)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Expiration Date* input field\"]";
        string dateString = expDate.ToString("MM/yy");
        InputTextField(By.XPath(xpath), dateString);
    }

    public void InputSecurityCode(string securityCode)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Security Code* input field\"]";
        InputTextField(By.XPath(xpath), securityCode);
    }

    public void MyBillingAddressIsTheSame(bool checkedUnchecked)
    {
        string needToClickXpath = "//android.view.ViewGroup[@content-desc='checkbox for My billing address is the same as my shipping address.']//android.widget.ImageView";
        if (IsElementExists(By.XPath(needToClickXpath), 2) == checkedUnchecked)
        {
            GetElement(By.XPath("//android.view.ViewGroup[@content-desc='checkbox for My billing address is the same as my shipping address.']/android.view.ViewGroup"), 5).Click();
        }
    }

    public void ClickReviewOrder()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"Review Order button\"]";
        GetElement(By.XPath(xpath), 5).Click();
        Common.Wait(1);
    }
}