using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace ShoppingCartTest.Pages
{
    public class HomePage
    {
        private IWebDriver driver;
        private readonly By _productsList = By.XPath(".//ul[@class='products columns-3']//li");
        private readonly By _addToCart = By.XPath(".//a[contains(text(), 'Add to cart')]");
        private readonly By _viewCart = By.XPath(".//a[contains(text(), 'View cart')]");
        // Constructor
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Method to add random items to the cart
        public void AddRandomItemsToCart(int numberOfItemsToAdd)
        {
            // Launch the home page
            driver.Navigate().GoToUrl("https://cms.demo.katalon.com/");

            // Get the list of items
            IReadOnlyCollection<IWebElement> itemList = driver.FindElements(_productsList); 
            
            bool viewCartAvailable = false;

            for (int itemIndex = 0; itemIndex < numberOfItemsToAdd; itemIndex++)
            {
                IWebElement randomItem = itemList.ElementAt(itemIndex);

                // Hover over the random item
                Actions action = new Actions(driver);
                action.MoveToElement(randomItem).Perform();
                // Click on the "Add to Cart" button
                IWebElement addToCartButton = randomItem.FindElement(_addToCart); 
                addToCartButton.Click();
                try
                {
                    IWebElement viewCartOption = randomItem.FindElement(_viewCart); 
                    viewCartAvailable = true;
                }
                catch (NoSuchElementException)
                {
                    // View Cart option is not available
                }
            }
        }

        // Method to navigate to the cart page and return the CartPage instance
        public CartPage GoToCartPage()
        {
            // Construct the cart page URL
            string cartPageUrl = "https://cms.demo.katalon.com/cart/"; 

            // Navigate to the cart page
            driver.Navigate().GoToUrl(cartPageUrl);

            // Return an instance of the CartPage
            return new CartPage(driver);
        }
    }
}
