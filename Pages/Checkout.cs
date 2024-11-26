using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

public class Checkout : BaseOperations
{
    public Checkout(AndroidDriver driver) : base(driver)
    {
    }

    public void ClickToPayment()
    {
        string xpath = "//android.widget.TextView[@text=\"To Payment\"]";
        GetElement(By.XPath(xpath), 5).Click();
        Common.Wait(1);
    }

    public void InputFullName(string fullName)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Full Name* input field\"]";
        InputTextField(By.XPath(xpath), fullName);
    }

    public void InputAddressLine1(string addresLine1)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Address Line 1* input field\"]";
        InputTextField(By.XPath(xpath), addresLine1);
    }

    public void InputAddressLine2(string addresLine2)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Address Line 2 input field\"]";
        InputTextField(By.XPath(xpath), addresLine2);
    }

    public void InputCity(string city)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"City* input field\"]";
        InputTextField(By.XPath(xpath), city);
    }

    public void InputStateRegion(string stateRegion)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"State/Region input field\"]";
        InputTextField(By.XPath(xpath), stateRegion);
    }

    public void InputZipCode(string zipCode)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Zip Code* input field\"]";
        InputTextField(By.XPath(xpath), zipCode);
    }

    public void InputCountry(string country)
    {
        string xpath = "//android.widget.EditText[@content-desc=\"Country* input field\"]";
        InputTextField(By.XPath(xpath), country);
    }

    public string GetErrorMessageForField(string fieldName)
    {
        string errorMessage = "";
        string xpath = $"//android.view.ViewGroup[starts-with(@content-desc, '{fieldName}') and ends-with(@content-desc, '-error-message')]//android.widget.TextView";
        if (IsElementExists(By.XPath(xpath), 2))
        {
            errorMessage = GetElement(By.XPath(xpath), 5).Text;
            return errorMessage;
        }
        return errorMessage;
    }    
}
