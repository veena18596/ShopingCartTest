using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartTest.Extensions;


namespace ShoppingCartTest.Pages
{
    public class CartPage
    {
        private IWebDriver driver;
        private readonly By _removeProductItem = By.XPath(".//td[@class='product-remove']/a");
        private readonly By _restoreProductItem = By.XPath(".//a[@class='restore-item']");
        private readonly By _cartTable = By.XPath("//table[contains(@class, 'shop_table')]");
        private readonly By _cartRow = By.XPath(".//tbody//tr");
        private readonly By _productPrice = By.XPath(".//td[@class='product-price']");
        // Constructor
        public CartPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Method to get the list of cart items
        public List<IWebElement> GetCartItems()
        {
            // Implementation to get the list of cart items
            List<IWebElement> cartItems = new List<IWebElement>();

            // Find the cart item rows in the table
            IWebElement cartTable = driver.FindElement(_cartTable); 
            IReadOnlyCollection<IWebElement> cartItemRows = cartTable.FindElements(_cartRow);

            // Add each cart item row to the list
            cartItems.AddRange(cartItemRows);
            cartItems.RemoveAt(cartItems.Count - 1);
            return cartItems;
        }

        // Method to remove the lowest price item from the cart
        public IWebElement FindLowestPriceItem()
        {
            // Implementation to remove the lowest price item from the cart
            List<IWebElement> cartItems = GetCartItems();

            // Find the lowest price item
            IWebElement lowestPriceItem = null;
            decimal lowestPrice = decimal.MaxValue;

            foreach (IWebElement item in cartItems)
            {
                // Extract the price of the item and compare it with the lowest price
                IWebElement priceElement = item.FindElement(_productPrice);
                string priceText = priceElement.Text.Replace("$", "");
                decimal price = decimal.Parse(priceText);

                if (price < lowestPrice)
                {
                    lowestPrice = price;
                    lowestPriceItem = item;
                }
            }
            return lowestPriceItem;
        }

        public void RemoveLowestPriceItem(IWebElement lowestPriceItem)
        {
            // Click on the remove link for the lowest price item
            var removeLink = lowestPriceItem.FindElement(_removeProductItem);
            removeLink.Click();
            SeleniumExtension.WaitForElement(driver, _restoreProductItem);
        }
    }
}
