using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Selenium
{
   public class Selenium
   {
      IWebDriver driver;
      bool isChrome = false;

      public Selenium(bool isChrome = true) { this.isChrome = isChrome; }
      [SetUp]
      public void startBrowser()
      {
         driver = new ChromeDriver(@"P:\Testing\Selenium\");
      }
      public void Maximize()
      {
         if (driver != null)
            driver.Manage().Window.Maximize();
      }

      [Test]
      public void test()
      {
         if (driver != null)
            driver.Url = "http://www.google.com";
      }

      public void OpenUrl(string url)
      {
         if (driver != null)
            driver.Url = url;
      }
      public void SendKeysByXPath(string xPath, string key)
      {
         IWebElement element = driver.FindElement(By.XPath(xPath));
         element.SendKeys(key);
      }
      public void SummitByXPath(string xPath)
      {
         IWebElement element = driver.FindElement(By.XPath(xPath)); 
         element.Submit();
      }
      public void SelectByTextXPath(string xPath, string selectedText)
      {
         IWebElement element = driver.FindElement(By.XPath(xPath));
         SelectElement select = new SelectElement(element);
         select.SelectByText(selectedText);
      }
      public void ClickByXPath(string xPath)
      {
         IWebElement element = driver.FindElement(By.XPath(xPath)); 
         element.Click();
      }
      public string GetTextByXPath(string xPath)
      {
         IWebElement element = driver.FindElement(By.XPath(xPath));
         return element.Text;
      }
      public string GetAttributeByXPath(string xPath, string attribute = "value")
      {
         IWebElement element = driver.FindElement(By.XPath(xPath));
         return element.GetAttribute(attribute);
      }
      public bool isDisplayedByXPath(string xPath)
      {
         IWebElement element = driver.FindElement(By.XPath(xPath));
         return element.Displayed;
      }
      public bool isEnableByXPath(string xPath)
      {
         IWebElement element = driver.FindElement(By.XPath(xPath));
         return element.Enabled;
      }
      public void ClearByXPath(string xPath)
      {
         IWebElement element = driver.FindElement(By.XPath(xPath));
         element.Clear();
      }
      [TearDown]
      public void closeBrowser()
      {
         driver.Close();
         driver.Dispose();
      }

   }
}