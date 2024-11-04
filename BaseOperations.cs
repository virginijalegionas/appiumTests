using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

public class BaseOperations
{
    protected readonly AndroidDriver driver;
    public LeftPanel LeftPanel { get; private set; }
    

    public BaseOperations(AndroidDriver driver)
    {
        this.driver = driver;
        LeftPanel = new LeftPanel(this);
        
    }

 /*    public void GoToHomePage()
    {
        string xpath = "//header/a/img";
        GetElement(By.XPath(xpath), 5).Click();
    } */

   /*  public void InputTextField(By by, string inputText)
    {
        IWebElement element = GetElement(by, 5);
        element.Clear();
        element.SendKeys(inputText);
    }
 */
   

  /*   public void ClickOnRadio(By by)
    {
        Actions act = new Actions(driver);
        IWebElement element = GetElement(by, 5);
        act.MoveToElement(element).Click().Build().Perform();
    } */

 /*    public void ClickOnCheckBox(By by)
    {
        Actions act = new Actions(driver);
        IWebElement element = GetElement(by, 5);
        act.MoveToElement(element).Click().Build().Perform();
    } */

   /*  public void SelectDateFromPicker(By by, DateOnly date)
    {
        GetElement(by, 5).Click();
        SelectElement yearDropDown = new SelectElement(driver.FindElement(By.ClassName("react-datepicker__year-select")));
        yearDropDown.SelectByValue(date.Year.ToString());

        SelectElement monthDropDown = new SelectElement(driver.FindElement(By.ClassName("react-datepicker__month-select")));
        //Month Value and text are different need to get Value from the Text
        string monthXpath = $"//option[contains(text(),'{date.ToString("MMMM")}')]";
        string monthValue = GetElement(By.XPath(monthXpath), 5).GetAttribute("value");
        monthDropDown.SelectByValue(monthValue);

        string dayXpath = $"//div[@class='react-datepicker__week']//div[text()='{date.Day}' and contains(@aria-label,'{date.ToString("MMMM")}')]";
        GetElement(By.XPath(dayXpath), 5).Click();
    } */

 

   

 

    public AppiumElement GetElement(By by, int waitSeconds)
    {
        AppiumElement element = null;
        if (IsElementExists(by, waitSeconds))
        {
            element = driver.FindElement(by);
            ((IJavaScriptExecutor)driver)
        .ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        return element;
    }

  /*   public List<IWebElement> GetElements(By by, int waitSeconds)
    {
        List<IWebElement> myElements = [];
        if (IsElementExists(by, waitSeconds))
        {
            myElements = driver.FindElements(by).ToList();
            ((IJavaScriptExecutor)driver)
        .ExecuteScript("arguments[0].scrollIntoView();", myElements.Last());
        }
        return myElements;
    } */

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

    /* public bool IsElementDisabled(By by)
    {
        string attribute = GetElement(by, 5).GetAttribute("disabled");
        return attribute == "true";
    } */
}

