using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using System.Drawing;

public class MainPage : BaseOperations
{
    public MainPage(AndroidDriver driver) : base(driver)
    {
    }

    public void OpenProduct(string productName)
    {
        string xpath = $"//android.widget.TextView[@text='{productName}']";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public List<ShopElement> GetAllProducts()
    {
        List<ShopElement> productsList = new List<ShopElement>();
        do
        {
            string productXpath = "//parent::android.view.ViewGroup[@content-desc=\"store item\"]";
            List<AppiumElement> products = GetElements(By.XPath(productXpath), 5);
            foreach (AppiumElement product in products)
            {
                string productName = product.FindElement(By.XPath("//android.widget.TextView[@content-desc=\"store item text\"]")).Text;
                string productPrice = product.FindElement(By.XPath("//android.widget.TextView[@content-desc=\"store item price\"]")).Text.Substring(1);
                ShopElement productValues = new ShopElement()
                {
                    Name = productName,
                    Price = double.Parse(productPrice),
                };
                if (!productsList.Exists(x => x.Name == productName))
                {
                    productsList.Add(productValues);
                }
            }
        }
        while (ScrollDownProductList());

        ScrollToProductListTop();
        return productsList;
    }

    public void ClickSortButton()
    {
        string sortButtonXpath = "//android.view.ViewGroup[@content-desc=\"sort button\"]";
        GetElement(By.XPath(sortButtonXpath), 5).Click();
    }

    public void SortByNameAscending()
    {
        ClickSortButton();
        string xpath = "//android.view.ViewGroup[@content-desc=\"nameAsc\"]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void SortByNameDescending()
    {
        ClickSortButton();
        string xpath = "//android.view.ViewGroup[@content-desc=\"nameDesc\"]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void SortByPriceAscending()
    {
        ClickSortButton();
        string xpath = "//android.view.ViewGroup[@content-desc=\"priceAsc\"]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void SortByPriceDescending()
    {
        ClickSortButton();
        string xpath = "//android.view.ViewGroup[@content-desc=\"priceDesc\"]";
        GetElement(By.XPath(xpath), 5).Click();
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

    public void ScrollToProductListTop()
    {
        string topElementXpath = "//android.widget.TextView[@text=\"Products\"]";
        while (!IsElementExists(By.XPath(topElementXpath), 1))
        {
            Size size = driver.Manage().Window.Size;
            int startX = size.Width / 2;
            int startY = (int)(size.Height * 0.2);
            int endY = (int)(size.Height * 0.8);

            Actions actions = new Actions(driver);
            actions.MoveToLocation(startX, startY)
                   .ClickAndHold()
                   .MoveByOffset(0, endY - startY)
                   .Release()
                   .Perform();
        }
    }
}