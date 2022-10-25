using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiumtentativa1.Pages
{
    class LoginPage
    {
        private readonly AndroidDriver<AndroidElement> _driver;

        public LoginPage(AndroidDriver<AndroidElement> driver)
        {
            _driver = driver;
        }
    }
}
