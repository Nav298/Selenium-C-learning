using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Xml.Linq;
using System.Reflection.Emit;

namespace SeleniumLearning
{
    [Parallelizable(ParallelScope.Self)]
    public class SortWebTables: IDisposable
    {
         IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            // implicit wait can be declared globally
            //driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }


        [Test]
        public void SortTable()
        {

            ArrayList a = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");


            //step 1- Get all veggie names into arraylist

            IList<IWebElement> veggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement element in veggies)
            {
                a.Add(element.Text);
            }

            TestContext.Progress.WriteLine("After Sorting");

            // Sort the arraylist

            a.Sort();
            foreach (string element in a)
            {
                TestContext.Progress.WriteLine(element);

            }

            // Step 3 Clicking Sort icom

            // CSS th[aria-label*="fruit name"]

            driver.FindElement(By.CssSelector("th[aria-label *= 'fruit name']")).Click();

            // by XPath //th[contains(@aria-label, "Veg/fruit name")]


            // Step 4 Get all veggie names

            ArrayList b = new ArrayList();

            IList<IWebElement> sortedveggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement element in sortedveggies)
            {
                b.Add(element.Text);
            }



            // Step 5 Comparing Arraylist a and b whether they are equal

            Assert.AreEqual(a, b);

            



        }


        [TearDown]
        public void TearDown()
        {
            if (driver!=null)
            {
                driver.Quit(); // or driver.Value.Dispose();
                driver = null; // clear the reference if needed
            }
        }

        public void Dispose()
        {
            TearDown(); // Ensure resources are cleaned up when disposing
        }
    }
}
