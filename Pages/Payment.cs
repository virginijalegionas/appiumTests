using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

public class Payment : BaseOperations
{
    public Payment(AndroidDriver driver) : base(driver)
    {
    }

    public void InputCardOwnerFullName(string fullName)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Full Name* input field\"][1]";
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

    public void SetCheckboxMyBillingAddressIsTheSame(bool enabled)
    {
        string needToClickXpath = "//android.view.ViewGroup[@content-desc='checkbox for My billing address is the same as my shipping address.']//android.widget.ImageView";
        if (IsElementExists(By.XPath(needToClickXpath), 2) != enabled)
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

    public void InputBillingFullName(string fullName)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Full Name* input field\"][2]";
        InputTextField(By.XPath(xpath), fullName);
    }

    public void InputBillingAddressLine1(string addresLine1)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Address Line 1* input field\"]";
        InputTextField(By.XPath(xpath), addresLine1);
    }

    public void InputBillingAddressLine2(string addresLine2)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Address Line 2 input field\"]";
        InputTextField(By.XPath(xpath), addresLine2);
    }

    public void InputBillingCity(string city)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"City* input field\"]";
        InputTextField(By.XPath(xpath), city);
    }

    public void InputBillingStateRegion(string stateRegion)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"State/Region input field\"]";
        InputTextField(By.XPath(xpath), stateRegion);
    }

    public void InputBillingZipCode(string zipCode)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Zip Code* input field\"]";
        InputTextField(By.XPath(xpath), zipCode);
    }

    public void InputBillingCountry(string country)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Country* input field\"]";
        InputTextField(By.XPath(xpath), country);
    }

    public string GetErrorMessageCardFullName()
    {
        string xpath = "//android.view.ViewGroup[@content-desc='Full Name*-error-message'][1]//android.widget.TextView";
        return GetElement(By.XPath(xpath), 5).Text;
    }

    public string GetErrorMessageBillingFullName()
    {
        string xpath = "//android.view.ViewGroup[@content-desc='Full Name*-error-message'][2]//android.widget.TextView";
        return GetElement(By.XPath(xpath), 5).Text;
    }
}