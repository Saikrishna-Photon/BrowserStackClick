using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace BrowserStackClick
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            //ChromeDriver driver = new ChromeDriver();

            ChromeOptions androidCapability = new ChromeOptions();
            Dictionary<string, object?> browserstackAndroidOptions = new Dictionary<string, object?>();
            Dictionary<string, object?> androidChromeOptions = new Dictionary<string, object?>();
            
            browserstackAndroidOptions.Add("browser", "chrome");
            browserstackAndroidOptions.Add("deviceName", "Samsung Galaxy S22");
            browserstackAndroidOptions.Add("osVersion", "12");

            browserstackAndroidOptions.Add("realMobile", "true");
            browserstackAndroidOptions.Add("local", false);
            browserstackAndroidOptions.Add("userName", "saikrishnadama_MAFzsL");
            browserstackAndroidOptions.Add("accessKey", "Lsau5WFLLbMpZNLbyxsE");
            browserstackAndroidOptions.Add("appiumVersion", "2.0.1");
            browserstackAndroidOptions.Add("interactiveDebugging", true);
            browserstackAndroidOptions.Add("sessionName", "Click Test"); //Test name
            browserstackAndroidOptions.Add("buildName", "Android Testing"); //Build name
            androidChromeOptions.Add("allowAllCookies", true);
            browserstackAndroidOptions.Add("chrome", androidChromeOptions);
            androidCapability.AddAdditionalOption("bstack:options", browserstackAndroidOptions);
            IWebDriver driver = new RemoteWebDriver(
              new Uri("https://hub.browserstack.com/wd/hub/"), androidCapability
            );

            // Test url available publicly, this navigates to the login page of our application
            driver.Navigate().GoToUrl("https://wellrxpremier.scriptsavetest.com/1406/login");
            driver.Manage().Window.Maximize();

            /// Enter valid credentials in username and password text fields.
            /// Observe that while entering the email and password, the keypad of the mobile device
            /// is displayed.
            driver.FindElement(By.Id("txtUsername")).SendKeys("ss.photon.qa+1406+08212023@gmail.com");
            driver.FindElement(By.Id("loginInlineModel_Password")).SendKeys("Quality2");
            
            /// Click on Sign In button after entering valid email and password
            /// Observe that clicking on Sign In button is dismissing the keypad of the device
            /// but on clicking on the button.
            var signin = WaitForElementPresent(driver, By.Id("btnLogin"));
            signin.Click();

            /// Add the same click action for the second time as below and run the same test again
            /// Observe that this time, the button is clicked and the test is passed.

            //signin.Click();

            /// After successful login, user is redirected to a dashboard page with text "My Dashboard"
            var dashboard = WaitForElementPresent(driver, By.CssSelector("div h1"));
            Assert.IsTrue(dashboard.Displayed);
            Assert.AreEqual("My Dashboard", dashboard.Text);
            driver.Dispose();
        }

        // Wait
        public static IWebElement WaitForElementPresent(IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            Thread.Sleep(1000);
            try
            {
                wait.Until(d =>
                {
                    var e = driver.FindElement(by);
                    return e.Displayed;
                }
                );
            }
            catch (Exception)
            {
                Console.WriteLine("\t\telement is not present.");
            }
            return driver.FindElement(by);
        }
    }
}