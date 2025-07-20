using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomatedTaskSchedulingSystem.SystemTest
{
  


        [TestClass]
        public class GenerateScheduleSysTest
        {
            private IWebDriver driver;

            [TestInitialize]
            public void Setup()
            {
                driver = new ChromeDriver(@"C:\WebDrivers");
                driver.Manage().Window.Maximize();
            }



       


        [TestMethod]
        public void GenerateSchedule_WhenEmployeesAvailable_Successful()
        {
            try
            {
                driver.Navigate().GoToUrl("https://localhost:44318/LoginPage.aspx");

                // 1. Log in
                driver.FindElement(By.Id("txtEmpID")).SendKeys("6686328");
                driver.FindElement(By.Id("txtPassword")).SendKeys("SuperAdmin@1234");
                driver.FindElement(By.Id("btnLogin")).Click();

                // 2. Wait for Dashboard or redirect
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                wait.Until(d => d.Url.Contains("Dashboard") || d.Url.Contains("GenerateSchedule"));

                // 3. Navigate to GenerateSchedule page
                if (!driver.Url.Contains("GenerateSchedule"))
                {
                    driver.Navigate().GoToUrl("https://localhost:44318/GenerateSchedule.aspx");
                }

                // 4. Set today's date
                string today = DateTime.Today.ToString("MM-dd-yyyy");
                IWebElement dateInput = driver.FindElement(By.Id("txtdate"));
                dateInput.Clear();
                dateInput.SendKeys(today);

                // 5. Click Generate
                driver.FindElement(By.Id("btngenerate")).Click();

                // 6. Wait for report viewer
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
                wait.Until(d => d.FindElements(By.Id("StiWebViewer1")).Count > 0);

                // ✅ 7. Assert report viewer is loaded
                Assert.IsTrue(driver.FindElements(By.Id("StiWebViewer1")).Count > 0, "Report viewer was not loaded.");

                // Optional: Pause for manual inspection
                System.Threading.Thread.Sleep(60000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Generate Schedule System Test failed: " + ex.Message);
            }
        }






        [TestMethod]
        public void NotGenerateSchedule_WhenNoEmployeeAvailable()
        {
            try
            {
                driver.Navigate().GoToUrl("https://localhost:44318/LoginPage.aspx");

                // 1. Log in as Super Admin
                driver.FindElement(By.Id("txtEmpID")).SendKeys("6686328");
                driver.FindElement(By.Id("txtPassword")).SendKeys("SuperAdmin@1234");
                driver.FindElement(By.Id("btnLogin")).Click();

                // 2. Wait for redirection
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                wait.Until(d => d.Url.Contains("Dashboard") || d.Url.Contains("GenerateSchedule"));

                // 3. Go to GenerateSchedule page
                if (!driver.Url.Contains("GenerateSchedule"))
                {
                    driver.Navigate().GoToUrl("https://localhost:44318/GenerateSchedule.aspx");
                }

                // 4. Choose a date where no employee is available
                // Pick a far future date unlikely to have availabilities
                string unavailableDate = DateTime.Today.AddDays(30).ToString("MM-dd-yyyy");
                IWebElement dateInput = driver.FindElement(By.Id("txtdate"));
                dateInput.Clear();
                dateInput.SendKeys(unavailableDate);

                // 5. Click Generate button
                driver.FindElement(By.Id("btngenerate")).Click();

                // 6. Wait for the alert label to appear and contain the expected message
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElement(By.Id("lblmsg")).Displayed);

                // 7. Validate message
                string message = driver.FindElement(By.Id("lblmsg")).Text;
                Assert.AreEqual("No employee available.  Go and Set Employee Availability for the Selected Date!!!", message);

                // Optional: Keep browser open briefly
                System.Threading.Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                Assert.Fail("Generate Schedule (No Employee Available) test failed: " + ex.Message);
            }
        }
















    }




}







