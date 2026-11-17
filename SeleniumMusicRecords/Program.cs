using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

IWebDriver driver = new ChromeDriver();
driver.Navigate().GoToUrl("http://127.0.0.1:5500/");

Thread.Sleep(2000);



driver.Quit();