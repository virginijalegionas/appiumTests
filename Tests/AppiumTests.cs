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
        productPage.IncreaseProdutAmount(2);
        productAmount = productPage.GetProductAmount();
        Assert.AreEqual("2", productAmount);
        Assert.IsTrue(productPage.IsAddToCartEnabled(), "Add to Cart button expected to be enabled");
    }
}