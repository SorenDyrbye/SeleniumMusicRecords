using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestSeleniumRecords
{
    [TestClass]
    public sealed class Test1
    {
        private IWebDriver driver = null!;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://127.0.0.1:5500/");
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void LoggedInUserIsAdmin()
        {
            // Act
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Id("password")).SendKeys("1234");
            driver.FindElement(By.Id("login")).Click();
            Thread.Sleep(1000);

            // Assert
            string tekst = driver.FindElement(By.Id("logged-in")).Text;
            Assert.AreEqual("Logged in as: admin", tekst);
        }

        [TestMethod]
        public void ShowRecordsButtonIsVisible()
        {
            // Assert
            Assert.IsTrue(driver.FindElement(By.Id("showRecordsBtn")).Displayed);
        }

        [TestMethod]
        public void ShowListWithButtonClick()
        {
            // Act
            driver.FindElement(By.Id("showRecordsBtn")).Click();
            Thread.Sleep(1000);

            // Assert
            Assert.IsTrue(driver.FindElement(By.Id("recordsTable")).Displayed);
        }

        [TestMethod]
        public void ShowListHiddenWithButtonClick()
        {
            // Act
            driver.FindElement(By.Id("showRecordsBtn")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("hideRecordsBtn")).Click();
            Thread.Sleep(1000);

            // Assert
            Assert.IsFalse(driver.FindElements(By.Id("recordsTable")).Any());
        }

        [TestMethod]
        public void TestFilteringByTitle()
        {
            // Act
            driver.FindElement(By.Id("showRecordsBtn")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("filterByTitle")).SendKeys("Kiss");
            driver.FindElement(By.Id("filterByTitleBtn")).Click();
            Thread.Sleep(1000);

            // Assert
            var rows = driver.FindElements(By.CssSelector("#recordsTable tbody tr"));
            Assert.AreEqual(1, rows.Count);
            StringAssert.Contains(rows[0].Text, "Kiss");
        }

        [TestMethod]
        public void TestFilteringByArtist()
        {
            // Act
            driver.FindElement(By.Id("showRecordsBtn")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("filterByArtist")).SendKeys("Batman");
            driver.FindElement(By.Id("filterByArtistBtn")).Click();
            Thread.Sleep(1000);

            // Assert
            var rows = driver.FindElements(By.CssSelector("#recordsTable tbody tr"));
            Assert.AreEqual(1, rows.Count);
            StringAssert.Contains(rows[0].Text, "Batman");
        }

        [TestMethod]
        public void TestUpdateRecord()
        {
            // Act
            driver.FindElement(By.Id("showRecordsBtn")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("updateBtn")).Click();
            Thread.Sleep(1000);

            var titel = driver.FindElement(By.CssSelector("tbody tr input"));
            titel.Clear();
            titel.SendKeys("Warhammer Song");

            driver.FindElement(By.Id("saveBtn")).Click();
            Thread.Sleep(1000);

            // Assert
            string tekst = driver.FindElement(By.CssSelector("tbody tr td")).Text;
            Assert.AreEqual("Warhammer Song", tekst);
        }
    }
}