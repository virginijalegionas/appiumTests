using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
public class ProductPage : BaseOperations
{
    public ProductPage(AndroidDriver driver) : base(driver)
    {
    }

    public List<string> GetProductColors()
    {
        List<AppiumElement> colorObject = GetElements(By.XPath("//android.view.ViewGroup[contains(@content-desc, 'circle')]"), 5);
        List<string> colors = colorObject
            .Select(x => x.GetAttribute("content-desc"))
            .Select(x => x.Substring(0, x.Length - 7))
            .ToList();
        return colors;
    }

    public double GetProductPrice()
    {
        return double.Parse(GetElement(By.XPath("//android.widget.TextView[@content-desc=\"product price\"]"), 5).Text.Substring(1));
    }

    public string GetProductName()
    {
        return GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"container header\"]/android.widget.TextView"), 5).Text;
    }

    public string GetProductDescription()
    {
        return GetElement(By.XPath("//android.widget.TextView[@content-desc=\"product description\"]"), 5).Text;
    }

    public void ClickAddToCart()
    {
        GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"Add To Cart button\"]"), 5).Click();
    }

    public void IncreaseProductAmountToNumber(int number)
    {
        int currentAmount = int.Parse(GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter amount\"]//android.widget.TextView"), 5).Text);
        int clickTimes = number - currentAmount;
        if (clickTimes > 0)
        {
            AppiumElement element = GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter plus button\"]"), 5);
            for (int i = 1; i <= clickTimes; i++)
            {
                element.Click();
            }
        }
    }

    public void ReduceProductAmountToNumber(int number)
    {
        int currentAmount = int.Parse(GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter amount\"]//android.widget.TextView"), 5).Text);
        int clickTimes = currentAmount - number;
        if (clickTimes > 0)
        {
            AppiumElement element = GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter minus button\"]"), 5);
            for (int i = 1; i <= clickTimes; i++)
            {
                element.Click();
            }
        }
    }

    public void SubmitProductReview(string starRate)
    {
        GetElement(By.XPath($"//android.view.ViewGroup[@content-desc=\"review star {starRate}\"]"), 5).Click();
        GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"Close Modal button\"]"), 5).Click();
    }

    public string GetProductAmount()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"counter amount\"]//android.widget.TextView";
        return GetElement(By.XPath(xpath), 5).Text;
    }

    public bool IsAddToCartEnabled()
    {
        return IsElementEnabled(By.XPath("//android.view.ViewGroup[@content-desc=\"Add To Cart button\"]"));
    }

    public void SelectProductColor(string color)
    {
        string xpath = $"//android.view.ViewGroup[contains(@content-desc, \"{color}\")]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void AddProductToBasket(string color = "black", int amount = 1)
    {
        SelectProductColor(color);
        IncreaseProductAmountToNumber(amount);
        ClickAddToCart();
    }
}