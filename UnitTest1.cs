using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using CSharpSelFramework.Utilities;
using CSharpSelFramework.pageObjects;

namespace SeleniumLearning


{
    [Parallelizable(ParallelScope.Children)]

    class E2ETest : Base
    {



        [Test,Category("Regression")]


        [TestCaseSource("AddTestDataConfig")]

        //Different types of parallel methods
        //1.run all data sets of Test method in parallel
        //2.run different test methods in same class in parallel
        //3.run all test files in project in parallel

        [Parallelizable(ParallelScope.All)]

        public void EndToEndFlow(String username, String password, String[] expectedproducts)
        {
            

            //String[] expectedproducts = { "iphone X", "Blackberry" };
            String[] actualproducts = new string[2];
            LoginPage loginpage = new LoginPage(getDriver());
            ProductsPage productspage = loginpage.validLogin(username, password);
            productspage.waitForPageDisplay();

            IList<IWebElement> products = productspage.getcards();

            foreach (IWebElement product in products)
            {

                if (expectedproducts.Contains(product.FindElement(productspage.getCardTitle()).Text))
                {
                    product.FindElement(productspage.addToCartButton()).Click();
                }


            }

            CheckoutPage checkoutpage = productspage.checkOut();

            IList<IWebElement> checkoutcards = checkoutpage.getCheckOutCards();

            for (int i = 0; i < checkoutcards.Count; i++)
            {
                actualproducts[i] = checkoutcards[i].Text;

            }


            Assert.AreEqual(expectedproducts, actualproducts);


            PurchasePage purchasepage = checkoutpage.checkout();


            purchasepage.CountryDropdown().SendKeys("ind");
            purchasepage.waitForPageDisplay();
            purchasepage.CountryName();
            purchasepage.checkBox();
            purchasepage.purchaseButton();



            String ConfirmText = purchasepage.getAlertText().Text;
            StringAssert.Contains("Success", ConfirmText);


        }


        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));


        }


        [Test,Category("Smoke")]

        public void Test()
        {
            
            driver.Value.FindElement(By.Name("username")).SendKeys("Rahulshetty");
            driver.Value.FindElement(By.Name("password")).SendKeys("Password");

            //by css selector
            //driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();

            //by xpath

            //driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            //by Css Selector

            driver.Value.FindElement(By.CssSelector(".text-info span:nth-child(1) input")).Click();

            //by Xpath
            driver.Value.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            //Thread.Sleep(3000);

            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(By.Id("signInBtn"), "Sign In"));

            String errormessage = driver.Value.FindElement(By.ClassName("alert-danger")).Text;

            TestContext.Progress.WriteLine(errormessage);


























        }
    }
}
