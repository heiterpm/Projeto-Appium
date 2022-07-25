using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.UiAutomator;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using NUnit;

namespace Appiumtentativa1
{
    public class Tests
    {

        private AppiumDriver<AndroidElement> _driver;
        [SetUp]
        public void Setup()
        {
            var appPath = @"D:\dev\ApiDemos-debug.apk";

            var driverOption = new AppiumOptions();
            driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Appium");
            driverOption.AddAdditionalCapability(MobileCapabilityType.App, appPath);

            driverOption.AddAdditionalCapability("chromedriverExecutable", @"C:\Users\heite\Downloads\chromedriver_win32 (1)\chromedriver.exe");

            _driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4733/wd/hub"), driverOption);

            var contexts = ((IContextAware)_driver).Contexts;
            string? webviewContext = null;
            for (var i=0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webviewContext = contexts[i];
                    break;
                }
            }

            ((IContextAware)_driver).Context = webviewContext;

            _driver.FindElementByXPath("//android.widget.TextView[@content-desc=\"App\"]").Click();
            _driver.FindElementByXPath("//android.widget.TextView[@content-desc=\"Alert Dialogs\"]").Click();
            _driver.FindElementByXPath("//android.widget.Button[@content-desc=\"Text Entry dialog\"]").Click();
            _driver.FindElementById("io.appium.android.apis:id/username_edit").SendKeys("Joãozinho");
            _driver.FindElementById("io.appium.android.apis:id/password_edit").SendKeys("senhasecreta");
            _driver.FindElementById("android:id/button1").Click();

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}