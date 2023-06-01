using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
using ShoppingCartTest.Pages;

namespace ShoppingCartTest.StepDefitnition
{
    [Binding]
    public class ShoppingCartSteps
    {
        private IWebDriver driver;
        private HomePage homePage;
        private CartPage cartPage;
        private int initialItemCount;
        private IWebElement lowestPriceItem;

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new ChromeDriver();
            homePage = new HomePage(driver);
            cartPage = null;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }

        [Given(@"I have added four random items to my cart")]
        public void GivenIHaveAddedFourRandomItemsToMyCart()
        {
            int itemCount = 4;
            homePage.AddRandomItemsToCart(itemCount);
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            cartPage = homePage.GoToCartPage();
            initialItemCount = cartPage.GetCartItems().Count;
        }

        [Then(@"I should find a total of four items listed in my cart")]
        public void ThenIShouldFindTotalFourItemsListedInMyCart()
        {
            int expectedItemCount = 4;
            int actualItemCount = cartPage.GetCartItems().Count;
            Assert.AreEqual(expectedItemCount, actualItemCount);
        }

        [When(@"I search for the lowest price item")]
        public void WhenISearchForTheLowestPriceItem()
        {
            lowestPriceItem = cartPage.FindLowestPriceItem();
        }

        [When(@"I remove the lowest price item from my cart")]
        public void WhenIRemoveTheLowestPriceItemFromMyCart()
        {
            cartPage.RemoveLowestPriceItem(lowestPriceItem);
        }

        [Then(@"I should verify that three items are left in my cart")]
        public void ThenIShouldVerifyThatThreeItemsAreLeftInMyCart()
        {
            var actualItemCount = cartPage.GetCartItems().Count;
            Assert.AreEqual(3, actualItemCount, " The expected value in the cart is incorrect");
        }
    }
}
