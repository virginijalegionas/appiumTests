using System.Net.Mail;

namespace appiumTests;

[TestClass]
public class AppiumTests : TestBase
{
    ShopElement[] shopElements = [
        new ShopElement{Name = "Sauce Labs Backpack", Price = 29.99},
        new ShopElement{Name = "Sauce Labs Fleece Jacket", Price = 49.99},
        new ShopElement{Name = "Sauce Labs Bike Light", Price = 9.99},
        new ShopElement{Name = "Sauce Labs Onesie", Price = 7.99},
        new ShopElement{Name = "Test.allTheThings() T-Shirt", Price = 15.99},
        new ShopElement{Name = "Sauce Labs Bolt T-Shirt", Price = 15.99},
    ];

    ShopElement shopElement = new ShopElement { Name = "Sauce Labs Backpack", Price = 29.99 };

    ShopElementInBasket[] basketElements = [
        new ShopElementInBasket{Name = "Sauce Labs Backpack", Price = 29.99, Color = "red", Amount = 3},
        new ShopElementInBasket{Name = "Sauce Labs Bike Light", Price = 9.99, Color = "black", Amount = 2},
        new ShopElementInBasket{Name = "Sauce Labs Bolt T-Shirt", Price = 15.99, Color = "black", Amount = 1},
    ];

    [TestMethod]
    public void ProductSortTest()
    {
        MainPage mainPage = new MainPage(driver);
        mainPage.SortByPriceDescending();
        List<ShopElement> priceDescProducts = mainPage.GetAllProducts();
        mainPage.SortByPriceAscending();
        List<ShopElement> priceAscProducts = mainPage.GetAllProducts();
        mainPage.SortByNameDescending();
        List<ShopElement> nameDescProducts = mainPage.GetAllProducts();
        mainPage.SortByNameAscending();
        List<ShopElement> nameAscProducts = mainPage.GetAllProducts();

        CollectionAssert.AreEqual(shopElements.Select(x => x.Name).OrderBy(x => x).ToList(), nameAscProducts.Select(x => x.Name).ToList());
        CollectionAssert.AreEqual(shopElements.Select(x => x.Name).OrderByDescending(x => x).ToList(), nameDescProducts.Select(x => x.Name).ToList());
        CollectionAssert.AreEqual(shopElements.Select(x => x.Price).OrderBy(x => x).ToList(), priceAscProducts.Select(x => x.Price).ToList());
        CollectionAssert.AreEqual(shopElements.Select(x => x.Price).OrderByDescending(x => x).ToList(), priceDescProducts.Select(x => x.Price).ToList());
    }

    [TestMethod]
    public void ProductPage()
    {
        List<string> expectedColors = ["black", "blue", "gray", "red"];
        MainPage mainPage = new MainPage(driver);
        mainPage.SortByNameAscending();
        mainPage.OpenProduct(shopElement.Name);

        //get product properties
        ProductPage productPage = new ProductPage(driver);
        string productName = productPage.GetProductName();
        double productPrice = productPage.GetProductPrice();
        List<string> productColors = productPage.GetProductColors();
        string productDescription = productPage.GetProductDescription();
        //validate the proterties are as expected
        Assert.AreEqual(shopElement.Name, productName);
        Assert.AreEqual(shopElement.Price, productPrice);
        CollectionAssert.AreEquivalent(expectedColors, productColors);
        StringAssert.Contains(productDescription, "laptop and tablet protection");

        //set a review - not much options to validate what value was set. su just setting it
        productPage.SubmitProductReview("4");

        //validating the current product amount
        string productAmount = productPage.GetProductAmount();
        Assert.AreEqual("1", productAmount);
        Assert.IsTrue(productPage.IsAddToCartEnabled(), "Add to Cart button expected to be enabled");

        //reduce product amount and validate
        productPage.IncreaseProductAmountToNumber(2);
        productAmount = productPage.GetProductAmount();
        Assert.AreEqual("2", productAmount);
        Assert.IsTrue(productPage.IsAddToCartEnabled(), "Add to Cart button expected to be enabled");
    }

    [TestMethod]
    public void Basket_MISC()
    {
        //STEP1: add some products to basket, validate in basket
        MainPage mainPage = new MainPage(driver);
        mainPage.SortByNameAscending();
        //Open and add first product to Cart
        mainPage.OpenProduct(basketElements[0].Name);
        ProductPage productPage = new ProductPage(driver);
        productPage.AddProductToBasket(basketElements[0].Color, basketElements[0].Amount);
        //Validate if Cart icon is changed
        int itemsOnBasketIcon = mainPage.GetBasketItemNumber();
        int expectedTotalAmount = basketElements[0].Amount;
        Assert.AreEqual(expectedTotalAmount, itemsOnBasketIcon);

        //Open product list and add one more product to Cart       
        LeftPanel leftPanel = new LeftPanel(driver);
        leftPanel.OpenCatalog();
        mainPage.OpenProduct(basketElements[1].Name);
        productPage.AddProductToBasket(basketElements[1].Color, basketElements[1].Amount);
        //validate Cart icon is changed again 
        itemsOnBasketIcon = mainPage.GetBasketItemNumber();
        expectedTotalAmount += basketElements[1].Amount;
        Assert.AreEqual(expectedTotalAmount, itemsOnBasketIcon);

        //add third product
        leftPanel.OpenCatalog();
        mainPage.OpenProduct(basketElements[2].Name);
        productPage.AddProductToBasket();
        //validate Cart icon is changed again 
        itemsOnBasketIcon = mainPage.GetBasketItemNumber();
        expectedTotalAmount += basketElements[2].Amount;
        Assert.AreEqual(expectedTotalAmount, itemsOnBasketIcon);

        //Validate in basket, that products are the same amount color as you added it
        mainPage.OpenBasket();
        Basket basket = new Basket(driver);
        List<ShopElementInBasket> basketProducts = basket.GetProductsValues();
        CollectionAssert.AreEquivalent(basketElements, basketProducts);
        //Validate total Items and amount values        
        int totalItems = basket.GetTotalItems();
        Assert.AreEqual(expectedTotalAmount, totalItems);
        //Currently app has an issue with total Price field, not testing until fixed
        double totalPrice = basket.GetTotalPrice();
        double expectedTotalPrice = basketElements.Sum(x => x.Price * x.Amount);
        Assert.AreEqual(expectedTotalPrice, totalPrice);

        //STEP2: Make amount changes in basket, validate
        //set first product amount to 1, last product to 3
        basket.ReduceProductAmountToNumber(basketElements[0].Name, 1);
        totalItems = basket.GetTotalItems();
        itemsOnBasketIcon = mainPage.GetBasketItemNumber();
        basketProducts = basket.GetProductsValues();
        Assert.AreEqual(1, basketProducts[0].Amount); //product amount is changed       
        expectedTotalAmount = basketProducts.Sum(x => x.Amount);
        Assert.AreEqual(expectedTotalAmount, totalItems); //validating total items changed in basket
        Assert.AreEqual(expectedTotalAmount, itemsOnBasketIcon); //totals changed in basket icon
        totalPrice = basket.GetTotalPrice();
        expectedTotalPrice = basketProducts.Sum(x => x.Price * x.Amount);
        Assert.AreEqual(expectedTotalPrice, totalPrice); //totals in price changed in basket icon

        //update amount for last product
        basket.ScrollDown();
        basket.IncreaseProductAmountToNumber(basketElements[2].Name, 3);
        basket.ScrollUp();
        totalItems = basket.GetTotalItems();
        itemsOnBasketIcon = mainPage.GetBasketItemNumber();
        basketProducts = basket.GetProductsValues();
        Assert.AreEqual(3, basketProducts[2].Amount); //product amount is changed
        expectedTotalAmount = basketProducts.Sum(x => x.Amount);
        Assert.AreEqual(expectedTotalAmount, totalItems); //validating total items changed in basket
        Assert.AreEqual(expectedTotalAmount, itemsOnBasketIcon); //totals changed in basket icon
        totalPrice = basket.GetTotalPrice();
        expectedTotalPrice = basketProducts.Sum(x => x.Price * x.Amount);
        Assert.AreEqual(expectedTotalPrice, totalPrice); //totals in price changed in basket icon

        //STEP3: empty basket - remove products one by one an validate totals
        foreach (ShopElementInBasket basketProduct in basketProducts.ToList()) //making list copy
        {
            basket.RemoveProductFromBasket(basketProduct.Name);
            basketProducts.Remove(basketProduct);
            Common.Wait(1);
            List<ShopElementInBasket> basketProductsAfterRemoval = basket.GetProductsValues();
            if (basketProductsAfterRemoval.Count > 0)
            {
                totalItems = basket.GetTotalItems();
                itemsOnBasketIcon = mainPage.GetBasketItemNumber();
                totalPrice = basket.GetTotalPrice();

                expectedTotalPrice = basketProducts.Sum(x => x.Price * x.Amount);
                expectedTotalAmount = basketProducts.Sum(x => x.Amount);
                Assert.AreEqual(expectedTotalPrice, totalPrice);
                Assert.AreEqual(expectedTotalAmount, itemsOnBasketIcon);
                Assert.AreEqual(expectedTotalAmount, totalItems);
                Assert.AreEqual(basketProductsAfterRemoval.Count, basketProducts.Count);
            }
            else
            {
                Assert.IsTrue(basket.IsBasketEmpty(), "Expected basket to be empty");
                basket.ClickGoShopping();
                StringAssert.Contains("Products", mainPage.GetPageHeader()); //products page opened
            }
        }
    }

    [TestMethod]
    public void ByProduct_WithLogin()
    {
        //login into app
        LeftPanel leftPanel = new LeftPanel(driver);
        leftPanel.OpenLogin();
        Login login = new Login(driver);
        login.LoginUser("bob@example.com", "10203040"); //later will make runsettings
        //Add product to basket
        MainPage mainPage = new MainPage(driver);
        mainPage.SortByNameAscending();
        mainPage.OpenProduct(shopElement.Name);
        ProductPage productPage = new ProductPage(driver);
        string productColor = "blue";
        int productAmount = 3;
        productPage.AddProductToBasket(productColor, productAmount);
        productPage.OpenBasket();
        //proceed to checkout
        Basket basket = new Basket(driver);
        basket.ClickProceedToCheckout();
        //Adding address info
        Checkout checkout = new Checkout(driver);
        string fullName = $"Full Name {Common.GenerateRandom()}";
        string addressLine1 = $"adl1 {Common.GenerateRandom()}";
        string addressLine2 = $"adl2 {Common.GenerateRandom()}";
        string city = $"ci {Common.GenerateRandom()}";
        string stateRegion = $"str {Common.GenerateRandom()}";
        string zipCode = $"zp {Common.GenerateRandom()}";
        string country = $"co {Common.GenerateRandom()}";
        checkout.InputFullName(fullName);
        checkout.InputAddressLine1(addressLine1);
        checkout.InputAddressLine2(addressLine2);
        checkout.InputCity(city);
        checkout.InputStateRegion(stateRegion);
        checkout.InputZipCode(zipCode);
        checkout.InputCountry(country);
        checkout.ClickToPayment();
        //Adding payment information
        Payment payment = new Payment(driver);
        payment.InputFullName(fullName);
        string cardNumber = "1234 4321 7896 256";
        payment.InputCardNumber(cardNumber);
        DateOnly expirationDate = new DateOnly(2029, 02, 09);
        payment.InputExpirationDate(expirationDate);
        string securityCode = "556";
        payment.InputSecurityCode(securityCode);
        payment.MyBillingAddressIsTheSame(true);
        payment.ClickReviewOrder();
        //Review Order: products info
        ReviewOrder reviewOrder = new ReviewOrder(driver);
        List<ShopElementInBasket> reviewProducts = reviewOrder.GetProductsValues();
        Assert.AreEqual(1, reviewProducts.Count);
        Assert.AreEqual(shopElement.Name, reviewProducts[0].Name);
        Assert.AreEqual(shopElement.Price, reviewProducts[0].Price);
        Assert.AreEqual(productColor, reviewProducts[0].Color);
        //Delivery info
        Dictionary<string, string> reviewDeliveryAddress = reviewOrder.GetDeliveryAddressInfo();
        Assert.AreEqual(fullName, reviewDeliveryAddress["Full Name"]);
        Assert.AreEqual($"{addressLine1}, {addressLine2}", reviewDeliveryAddress["Address Lines"]);
        Assert.AreEqual($"{city}, {stateRegion}", reviewDeliveryAddress["City, State"]);
        Assert.AreEqual(country, reviewDeliveryAddress["Country"]);
        Assert.AreEqual(zipCode, reviewDeliveryAddress["Zip Code"]);
        //Payment info
        Dictionary<string, string> reviewPaymentMethod = reviewOrder.GetPaymentMethodInfo();
        Assert.AreEqual(fullName, reviewPaymentMethod["Full Name"]);
        Assert.AreEqual(cardNumber, reviewPaymentMethod["Card Number"]);
        Assert.AreEqual(expirationDate.ToString("MM/yy"), reviewPaymentMethod["Expiration Date"]);
        //Billing address
        Dictionary<string, string> reviewBillingAddress = reviewOrder.GetBilingAddressInfo();
        Assert.AreEqual(0, reviewBillingAddress.Count);
        //Shipping costs
        double reviewShippingCosts = reviewOrder.GetShippingCosts();
        Assert.AreEqual(5.99, reviewShippingCosts);
        //totals
        int totalAmount = reviewOrder.GetTotalItems();
        Assert.AreEqual(productAmount, totalAmount);
        double totalPrice = reviewOrder.GetTotalPrice();
        Assert.AreEqual(shopElement.Price * productAmount + reviewShippingCosts, totalPrice);
        reviewOrder.ClickPlaceOrder();
        reviewOrder.ClickContinueShopping();
        string pageHeader = mainPage.GetPageHeader();
        StringAssert.Contains(pageHeader, "Products");
    }
}