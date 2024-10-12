using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumLearning
{

    [Parallelizable(ParallelScope.Self)]
    public class WindowHandlers: IDisposable
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            // implicit wait can be declared globally
            //driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(5);
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
        }

        [Test]

        public void WindowHandle()
        {

            String email = "mentor@rahulshettyacademy.com";
            String Parentwindow= driver.CurrentWindowHandle;
            driver.FindElement(By.ClassName("blinkingText")).Click();

            Assert.AreEqual(2, driver.WindowHandles.Count);

            String ChildWindowName = driver.WindowHandles[1];

            driver.SwitchTo().Window(ChildWindowName);

            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
            String text = driver.FindElement(By.CssSelector(".red")).Text;

            //Please email us at mentor@rahulshettyacademy.com with below template to receive response

            String[] splittedtext = text.Split("at");

            // mentor@rahulshettyacademy.com with below templ

            String[] splitext1 = splittedtext[1].Trim().Split(" ");

            Assert.AreEqual(email, splitext1[0]);

            driver.SwitchTo().Window(Parentwindow);
            driver.FindElement(By.Name("username")).SendKeys(splitext1[1]);
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
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
