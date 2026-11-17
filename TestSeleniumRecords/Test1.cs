using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace TestSeleniumRecords
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]

        public void LoggedInUserIsAdmin()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://127.0.0.1:5500/");

            // Act
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Id("password")).SendKeys("1234");
            driver.FindElement(By.Id("login")).Click();
            Thread.Sleep(1000);

            // Assert
            string tekst = driver.FindElement(By.Id("logged-in")).Text;
            Assert.AreEqual("Logged in as: admin", tekst);

            driver.Quit();
        }

        [TestMethod]
        public void ShowRecordsButtonIsVisible()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://127.0.0.1:5500/");

            // Act

            // Assert
            bool isVisible = driver.FindElement(By.Id("showRecordsBtn")).Displayed;
            Assert.IsTrue(isVisible);

            driver.Quit();
        }

        [TestMethod]
        public void ShowlistWithButtonClick()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://127.0.0.1:5500/");

            // Act
            driver.FindElement(By.Id("showRecordsBtn")).Click();

            // Assert
            bool isListVisible = driver.FindElement(By.Id("recordsTable")).Displayed;
            Assert.IsTrue(isListVisible);

            driver.Quit();
        }

        [TestMethod]
        public void ShowListHiddenWithButtonClick()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://127.0.0.1:5500/");

            // Act
            driver.FindElement(By.Id("showRecordsBtn")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("hideRecordsBtn")).Click();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(driver.FindElements(By.Id("recordsTable")).Any());

            driver.Quit();
        }

        [TestMethod]
        public void TestFilteringByTitle()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://127.0.0.1:5500/");

            // Act
            driver.FindElement(By.Id("showRecordsBtn")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("filterByTitle")).SendKeys("Kiss");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("filterByTitleBtn")).Click();
            Thread.Sleep(1000);


            // Assert
            var rows = driver.FindElements(By.CssSelector("#recordsTable tbody tr"));
            Assert.AreEqual(1, rows.Count);
            StringAssert.Contains(rows[0].Text, "Kiss");

            driver.Quit();
        }

    }
}
