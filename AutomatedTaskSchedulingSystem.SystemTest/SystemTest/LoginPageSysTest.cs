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


    public class LoginPageSysTest
    {
        private IWebDriver driver;




        [TestInitialize]



        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--remote-allow-origins=*"); // helps resolve some driver mismatches

            driver = new ChromeDriver(@"C:\WebDrivers", options);
            driver.Manage().Window.Maximize();


        }

        





        [TestMethod]
        public void LoginPage_WithValidCredentials_Successful()
        {
            try
            {
                driver.Navigate().GoToUrl("https://localhost:44318/LoginPage.aspx");

                driver.FindElement(By.Id("txtEmpID")).SendKeys("6686328");
                driver.FindElement(By.Id("txtPassword")).SendKeys("SuperAdmin@1234");
                driver.FindElement(By.Id("btnLogin")).Click();

                // Wait for an element that ONLY exists on the Dashboard
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                var dashboardHeader = wait.Until(d =>
                    d.FindElement(By.XPath("//*[contains(text(), 'Dashboard')]"))
                );

                Assert.IsNotNull(dashboardHeader, "Dashboard did not load properly after login.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Login test failed: " + ex.Message);
            }
        }





        [TestMethod]
        public void LoginPage_WithInvalidCredentials_Unsuccessful()
        {

           
     
            try
            {
                driver.Navigate().GoToUrl("https://localhost:44318/LoginPage.aspx");

                // Input invalid credentials
                driver.FindElement(By.Id("txtEmpID")).SendKeys("invalidUser");
                driver.FindElement(By.Id("txtPassword")).SendKeys("invalidPassword");
                driver.FindElement(By.Id("btnLogin")).Click();

                // Wait up to 60 seconds for the alert to appear
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                IAlert alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

                // Assert the alert text
                Assert.AreEqual("Invalid Username or Password", alert.Text);
                Assert.AreEqual("success", alert.Text);

                // Let the alert stay open for 30 seconds so you can see it (optional)
                Thread.Sleep(10000); // 30 seconds pause

                // Now accept it
                alert.Accept();
            }
            catch (Exception ex)
            {
                Assert.Fail("Alert test failed: " + ex.Message);
            }

        }




        





    }




}











