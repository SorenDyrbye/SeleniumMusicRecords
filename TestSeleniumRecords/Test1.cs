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
    }
}
