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
        List<string> colors = colorObject.Select(x => x.GetAttribute("content-desc")).ToList();
        return colors.Select(x => x.Substring(0, x.Length - 7)).ToList();
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

    public void IncreaseProdutAmount(int number)
    {
        for (int i = 1; i < number; i++)
        {
            GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter plus button\"]"), 5).Click();
        }
    }

    public void ReduceProdutAmount(int number)
    {
        for (int i = 1; i < number; i++)
        {
            GetElement(By.XPath("//android.view.ViewGroup[@content-desc=\"counter minus button\"]"), 5).Click();
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
}