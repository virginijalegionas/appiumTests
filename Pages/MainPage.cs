using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;

public class MainPage : BaseOperations
{
    public MainPage(AndroidDriver driver) : base(driver)
    {
    }

    public List<ShopElement> GetAllProducts()
    {
        List<ShopElement> productsList = new List<ShopElement>();

        string bottomXpath = "//android.widget.TextView[@text=\"© 2024 Sauce Labs. All Rights Reserved. Terms of Service | Privacy Policy.\"]";
        while (true)
        {
            string productXpath = "//android.widget.TextView[@content-desc=\"store item price\"]//parent::android.view.ViewGroup[@content-desc=\"store item\"]";
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
            if (!IsElementExists(By.XPath(bottomXpath), 1))
            {
                ScrollDown();
            }
            else
            {
                break;
            }
        }
        ScrollToThePageTop();
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
}