using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;

public class Basket : BaseOperations
{
    public Basket(AndroidDriver driver) : base(driver)
    {
    }

    public void ClickProceedToCheckout()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"Proceed To Checkout button\"]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public ShopElementInBasket GetProductValues(string productName)
    {
        string productXpath = $"//android.widget.TextView[@text=\"{productName}\"]//parent::android.view.ViewGroup";
        AppiumElement product = GetElement(By.XPath(productXpath), 5);
        string currentAmount = product.FindElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter amount\"]/child::android.widget.TextView")).Text;
        string currentColor = product.FindElement(By.XPath("//android.widget.TextView[@text=\"Color:\"]/following-sibling::android.view.ViewGroup[1]")).GetAttribute("content-desc");
        string currentPrice = product.FindElement(By.XPath("//android.widget.TextView[@content-desc=\"product price\"]")).Text.Substring(1);
        ShopElementInBasket productValues = new ShopElementInBasket()
        {
            Name = productName,
            Price = double.Parse(currentPrice),
            Color = currentColor.Substring(0, currentColor.Length - 7),
            Amount = int.Parse(currentAmount),
        };
        return productValues;
    }

    public void RemoveProductFromBasket(string productName)
    {
        string productXpath = $"//android.widget.TextView[@text=\"{productName}\"]//parent::android.view.ViewGroup";
        AppiumElement product = GetElement(By.XPath(productXpath), 5);
        product.FindElement(By.XPath("//android.widget.TextView[@text=\"Remove Item\"]")).Click();
    }

    public void IncreaseProductAmountToNumber(string productName, int number)
    {
        string productXpath = $"//android.widget.TextView[@text=\"{productName}\"]//parent::android.view.ViewGroup";
        AppiumElement product = GetElement(By.XPath(productXpath), 5);
        int currentAmount = int.Parse(product.FindElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter amount\"]/child::android.widget.TextView")).Text);
        int clickTimes = number - currentAmount;
        if (clickTimes > 0)
        {
            AppiumElement element = product.FindElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter plus button\"]"));
            for (int i = 1; i <= clickTimes; i++)
            {
                element.Click();
            }
        }
    }

    public void ReduceProductAmountToNumber(string productName, int number)
    {
        string productXpath = $"//android.widget.TextView[@text=\"{productName}\"]//parent::android.view.ViewGroup";
        AppiumElement product = GetElement(By.XPath(productXpath), 5);
        int currentAmount = int.Parse(product.FindElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter amount\"]/child::android.widget.TextView")).Text);
        int clickTimes = currentAmount - number;
        if (clickTimes > 0)
        {
            AppiumElement element = product.FindElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter minus button\"]/android.widget.ImageView"));
            for (int i = 1; i <= clickTimes; i++)
            {
                element.Click();
            }
        }
    }

    public void EmptyBasket()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"remove item\"]";
        while (IsElementExists(By.XPath(xpath), 2))
        {
            GetElement(By.XPath(xpath), 5).Click();
            Common.Wait(1);
        }
    }

    public List<ShopElementInBasket> GetProductsValues()
    {
        List<ShopElementInBasket> basketProducts = new List<ShopElementInBasket>();
        string productXpath = "//android.view.ViewGroup[@content-desc=\"product row\"]";
        List<AppiumElement> products = GetElements(By.XPath(productXpath), 5);
        foreach (AppiumElement product in products)
        {
            string productName = product.FindElement(By.XPath("//android.widget.TextView[@content-desc=\"product label\"]")).Text;
            string productPrice = product.FindElement(By.XPath("//android.widget.TextView[@content-desc=\"product price\"]")).Text.Substring(1);
            string productColor = product.FindElement(By.XPath("//android.widget.TextView[@text=\"Color:\"]/following-sibling::android.view.ViewGroup[1]")).GetAttribute("content-desc");
            string productAmount = product.FindElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter amount\"]/child::android.widget.TextView")).Text;
            ShopElementInBasket productValues = new ShopElementInBasket()
            {
                Name = productName,
                Price = double.Parse(productPrice),
                Color = productColor.Substring(0, productColor.Length - 7),
                Amount = int.Parse(productAmount),
            };
            if (!basketProducts.Exists(x => x.Name == productName))
            {
                basketProducts.Add(productValues);
            }
        }
        return basketProducts;
    }

    public void ScrollToBasketListTop()
    {
        string topElementXpath = "//android.widget.TextView[@text=\"My Cart\"]";
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

    public int GetTotalItems()
    {
        string items = GetElement(By.XPath("//android.widget.TextView[@content-desc=\"total number\"]"), 5).Text;
        return int.Parse(items.Substring(0, items.Length - 6));
    }

    public double GetTotalPrice()
    {
        string price = GetElement(By.XPath("//android.widget.TextView[@content-desc=\"total price\"]"), 5).Text.Substring(1);
        return double.Parse(price);
    }

    public bool IsBasketEmpty()
    {
        return GetPageHeader() == "No Items";
    }

    public void ClickGoShopping()
    {
        GetElement(By.XPath("//android.view.ViewGroup[@content-desc='Go Shopping button']"), 5).Click();
    }
}
