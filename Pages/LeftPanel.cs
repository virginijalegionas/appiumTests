using OpenQA.Selenium;

public class LeftPanel
{
    private BaseOperations baseOperations;

    public LeftPanel(BaseOperations baseOperations)
    {
        this.baseOperations = baseOperations;
    }

    public void OpenLeftMenu()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"open menu\"]/android.widget.ImageView";
        if (!IsLeftMenuExpanded())
            baseOperations.GetElement(By.XPath(xpath), 5).Click();
    }

    public bool IsLeftMenuExpanded()
    {
        //will be checking if catalog xpath is showing up, that means the whole left menu is expanded
        string xpath = "//android.view.ViewGroup[@content-desc=\"menu item catalog\"]";
        return baseOperations.IsElementExists(By.XPath(xpath), 1);
    }

    public void ClickCatalog()
    {
        string xpath = "//android.view.ViewGroup[@content-desc=\"menu item catalog\"]";
        if (!IsLeftMenuExpanded())
            baseOperations.GetElement(By.XPath(xpath), 5).Click();
    }
}