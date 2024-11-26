using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;

public class BaseOperations
{
    protected readonly AndroidDriver driver;

    public BaseOperations(AndroidDriver driver)
    {
        this.driver = driver;
    }

    public void InputTextField(By by, string inputText)
    {
        IWebElement element = GetElement(by, 5);
        element.Clear();
        element.SendKeys(inputText);
    }

    public int GetBasketItemNumber()
    {
        string basketXpath = "//android.view.ViewGroup[@content-desc=\"cart badge\"]/android.widget.TextView";
        if (!IsElementExists(By.XPath(basketXpath), 3))
            return 0;

        return int.Parse(GetElement(By.XPath(basketXpath), 5).Text);
    }

    public void OpenBasket()
    {
        GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"cart badge\"]/android.widget.ImageView"), 5).Click();
    }

    public AppiumElement GetElement(By by, int waitSeconds)
    {
        AppiumElement element = null;
        if (IsElementExists(by, waitSeconds))
        {
            element = driver.FindElement(by);
        }
        return element;
    }

    public List<AppiumElement> GetElements(By by, int waitSeconds)
    {
        List<AppiumElement> myElements = [];
        if (IsElementExists(by, waitSeconds))
        {
            myElements = driver.FindElements(by).ToList();
        }
        return myElements;
    }

    public bool IsElementExists(By by, int waitSeconds)
    {
        for (; waitSeconds > 0; waitSeconds--)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                Common.Wait(1);
            }
        }
        return false;
    }

    public bool IsElementEnabled(By by)
    {
        string attribute = GetElement(by, 5).GetAttribute("enabled");
        return attribute == "true";
    }

    public bool ScrollDownProductList()
    {
        string bottomXpath = "//android.widget.TextView[@text=\"© 2024 Sauce Labs. All Rights Reserved. Terms of Service | Privacy Policy.\"]";
        if (IsElementExists(By.XPath(bottomXpath), 1))
            return false;

        Size size = driver.Manage().Window.Size;
        int startX = size.Width / 2;
        int startY = (int)(size.Height * 0.8);
        int endY = (int)(size.Height * 0.2);

        Actions actions = new Actions(driver);
        actions.MoveToLocation(startX, startY)
               .ClickAndHold() // Press down at the start point
               .MoveByOffset(0, endY - startY) // Move vertically to the end point
               .Release() // Release the press
               .Perform();
        return true;
    }

    public string GetPageHeader()
    {
        /*OpenQA.Selenium.StaleElementReferenceException: The element 
        'By.xpath: //android.view.ViewGroup[@content-desc="container header"]/child::android.widget.TextView' 
        is not linked to the same object in DOM anymore; For documentation on this error, 
        please visit: https://www.selenium.dev/documentation/webdriver/troubleshooting/errors#stale-element-reference-exception*/
        Common.Wait(1);
        return GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"container header\"]/child::android.widget.TextView"), 5).Text;
    }

    public void ScrollDown()
    {
        var size = driver.Manage().Window.Size;
        int startX = size.Width / 2;
        int startY = (int)(size.Height * 0.7);
        int endY = (int)(size.Height * 0.3);

        Actions actions = new Actions(driver);
        actions.MoveToLocation(startX, startY) // Move to the starting point
               .ClickAndHold() // Press down at the start point
               .MoveByOffset(0, endY - startY) // Move vertically to the end point
               .Release() // Release the press
               .Perform();
    }

    public void ScrollUp()
    {
        var size = driver.Manage().Window.Size;
        int startX = size.Width / 2;
        int startY = (int)(size.Height * 0.2);
        int endY = (int)(size.Height * 0.7);

        Actions actions = new Actions(driver);
        actions.MoveToLocation(startX, startY) // Move to the starting point
               .ClickAndHold() // Press down at the start point
               .MoveByOffset(0, endY - startY) // Move vertically to the end point
               .Release() // Release the press
               .Perform();
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