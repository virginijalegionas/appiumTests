using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

public class ReviewOrder : BaseOperations
{
    public ReviewOrder(AndroidDriver driver) : base(driver)
    {
    }

    public void ClickPlaceOrder()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"Place Order button\"]";
        GetElement(By.XPath(xpath), 5).Click();
    }

    public void ClickContinueShopping()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"Continue Shopping button\"]";
        GetElement(By.XPath(xpath), 5).Click();
        Common.Wait(1);
    }

    public int GetTotalItems()
    {
        string items = GetElement(By.XPath("//android.widget.TextView[@content-desc='total number']"), 5).Text;
        return int.Parse(items.Substring(0, items.Length - 6));
    }

    public double GetTotalPrice()
    {
        string price = GetElement(By.XPath("//android.widget.TextView[@content-desc=\"total price\"]"), 5).Text.Substring(1);
        return double.Parse(price);
    }

    public List<ShopElementInBasket> GetProductsValues()
    {
        List<ShopElementInBasket> basketProducts = new List<ShopElementInBasket>();
        string productXpath = "//android.view.ViewGroup[@content-desc='product row']";
        List<AppiumElement> products = GetElements(By.XPath(productXpath), 5);
        foreach (AppiumElement product in products)
        {
            string productName = product.FindElement(By.XPath("//android.widget.TextView[@content-desc=\"product label\"]")).Text;
            string productPrice = product.FindElement(By.XPath("//android.widget.TextView[@content-desc=\"product price\"]")).Text.Substring(1);
            string productColor = product.FindElement(By.XPath("//android.widget.TextView[@text=\"Color:\"]/following-sibling::android.view.ViewGroup[1]")).GetAttribute("content-desc");
            ShopElementInBasket productValues = new ShopElementInBasket()
            {
                Name = productName,
                Price = double.Parse(productPrice),
                Color = productColor.Substring(0, productColor.Length - 7),
            };
            basketProducts.Add(productValues);
        }
        return basketProducts;
    }

    public Dictionary<string, string> GetDeliveryAddressInfo()
    {
        Dictionary<string, string> deliveryAddressInfo = new Dictionary<string, string>();
        AppiumElement deliveryAddressObject = GetElement(By.XPath("//android.view.ViewGroup[@content-desc='checkout delivery address']"), 5);

        string fullName = deliveryAddressObject.FindElement(By.XPath("//android.widget.TextView[2]")).Text;
        deliveryAddressInfo.Add("Full Name", fullName);
        string addressLines = deliveryAddressObject.FindElement(By.XPath("//android.widget.TextView[3]")).Text;
        deliveryAddressInfo.Add("Address Lines", addressLines);
        string cityState = deliveryAddressObject.FindElement(By.XPath("//android.widget.TextView[4]")).Text;
        deliveryAddressInfo.Add("City, State", cityState);
        string countryZip = deliveryAddressObject.FindElement(By.XPath("//android.widget.TextView[5]")).Text;
        string[] addresses = countryZip.Split(',');
        deliveryAddressInfo.Add("Country", addresses[0].Trim());
        deliveryAddressInfo.Add("Zip Code", addresses[1].Trim());
        return deliveryAddressInfo;
    }

    public Dictionary<string, string> GetBilingAddressInfo()
    {
        Dictionary<string, string> billingAddressInfo = new Dictionary<string, string>();
        if (!IsBillingAddressTheSame())
        {
            string billingAddressXpath = "//android.view.ViewGroup[@content-desc='checkout billing address']";
            AppiumElement billingAddressObject = GetElement(By.XPath(billingAddressXpath), 5);
            string fullName = billingAddressObject.FindElement(By.XPath("//android.widget.TextView[2]")).Text;
            billingAddressInfo.Add("Full Name", fullName);
            string addressLines = billingAddressObject.FindElement(By.XPath("//android.widget.TextView[3]")).Text;
            billingAddressInfo.Add("Address Lines", addressLines);
            string cityState = billingAddressObject.FindElement(By.XPath("//android.widget.TextView[4]")).Text;
            billingAddressInfo.Add("City, State", cityState);
            string countryZip = billingAddressObject.FindElement(By.XPath("//android.widget.TextView[5]")).Text;
            string[] addresses = countryZip.Split(',');
            billingAddressInfo.Add("Country", addresses[0].Trim());
            billingAddressInfo.Add("Zip Code", addresses[1].Trim());
        }
        return billingAddressInfo;
    }

    public bool IsBillingAddressTheSame()
    {
        string xpath = "//android.view.ViewGroup[@content-desc='checkout billing address']/android.widget.TextView[1]";
        string billingAddress = GetElement(By.XPath(xpath), 5).Text;
        return billingAddress == "Billing address is the same as shipping address";
    }

    public Dictionary<string, string> GetPaymentMethodInfo()
    {
        Dictionary<string, string> PaymentMethodInfo = new Dictionary<string, string>();
        AppiumElement PaymentMethodObject = GetElement(By.XPath("//android.view.ViewGroup[@content-desc='checkout payment info']"), 5);
        string fullName = PaymentMethodObject.FindElement(By.XPath("//android.widget.TextView[2]")).Text;
        PaymentMethodInfo.Add("Full Name", fullName);
        string cardNumber = PaymentMethodObject.FindElement(By.XPath("//android.widget.TextView[3]")).Text;
        PaymentMethodInfo.Add("Card Number", cardNumber);
        string expirationDate = PaymentMethodObject.FindElement(By.XPath("//android.widget.TextView[4]")).Text;
        PaymentMethodInfo.Add("Expiration Date", expirationDate.Substring(5));
        return PaymentMethodInfo;
    }

    public double GetShippingCosts()
    {
        string xpath = "//android.view.ViewGroup[@content-desc='checkout delivery details']//android.widget.TextView[2]";
        string shippingCostsString = GetElement(By.XPath(xpath), 5).Text.Substring(1);
        return double.Parse(shippingCostsString);
    }
}