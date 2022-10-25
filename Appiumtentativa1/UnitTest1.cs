using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.UiAutomator;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;

namespace Appiumtentativa1
{
    public class Tests
    {

        private AppiumDriver<AndroidElement> _driver;
        [SetUp]
        public void Setup()
        {
            var appPath = @"D:\dev\Projetowebcsharp\Appiumtentativa1\AFVNanuque_TESTE_v2.06.011-2.apk";


            //Configurações do driver
            var driverOption = new AppiumOptions();
            driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Appium");
            driverOption.AddAdditionalCapability(MobileCapabilityType.App, appPath);
            //driverOption.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            //driverOption.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "io.appium.android.apis.ApiDemos");

            driverOption.AddAdditionalCapability("chromedriverExecutable", @"C:\Users\heite\Downloads\chromedriver_win32 (1)\chromedriver.exe");

            //Inicia o Driver
            _driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4733/wd/hub"), driverOption);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);


            var contexts = ((IContextAware)_driver).Contexts;
            string? webviewContext = null;
            for (var i = 0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webviewContext = contexts[i];
                    break;
                }
            }

            //((IContextAware)_driver).Context = webviewContext;

        }

        [Test]
        public void Test()
        {
            //Login/Sync
            _driver.FindElementById("com.android.permissioncontroller:id/permission_deny_button").Click();
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/edt_chave_acesso").SendKeys("tnanuquev2");
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/btn_avancar").Click();
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/edt_cpf").SendKeys("82431221687");
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/edt_login").SendKeys("CNN0000004");
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/btn_entrar").Click();
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/edt_senha").SendKeys("04040404");
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/btn_entrar").Click();
            _driver.FindElementById("com.android.permissioncontroller:id/permission_allow_button").Click();
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/btn_sincronizar").Click();

            WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 5, 0));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("btn_sincronizar")));



            var infoCadastrais = _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/txv_status_informacoes").GetAttribute("text");
            var bensGruposPrecos = _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/txv_status_grupos_precos").GetAttribute("text");
            var leadsClientes = _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/txv_cliente_lead").GetAttribute("text");
            if (infoCadastrais == "Atualizado" || bensGruposPrecos == "Atualizado" || leadsClientes == "Atualizado")
            {
                //Dados
                _driver.FindElementById("btn_sincronizar").Click();
                _driver.FindElementById("img_inicio_leads").Click();
                _driver.FindElementById("action_adicionar").Click();
                _driver.FindElementById("edt_cliente_fisica_nome_completo").SendKeys("Appium do heiter");
                _driver.FindElementById("edt_cliente_fisica_cpf").SendKeys("45837625852");
                _driver.FindElementById("edt_cliente_email").SendKeys("appium@heiter.com.br");
                _driver.FindElementById("edt_cliente_celular").SendKeys("17988136868");
                _driver.FindElementById("edt_cliente_valor_simulado").SendKeys("1231233");
                _driver.FindElementById("edt_cliente_data_esperada").SendKeys("23082023");
                _driver.FindElementById("auto_endereco_cidade").Click();
                _driver.FindElementById("newm_popupauto_complete_buscar").SendKeys("Rio Preto");
                _driver.FindElementById("spinner_default_text").Click();
                _driver.FindElementById("btn_continuar").Click();
                //Endereço
                _driver.FindElementById("edt_endereco_cep").SendKeys("15051‑681");
                _driver.FindElementById("btn_endereco_preencher_cep").Click();
                _driver.FindElementById("edt_endereco_numero").SendKeys("2043");
                _driver.FindElementById("btn_continuar").Click();
                //Dados Bancarios
                _driver.FindElementById("btn_cadastrar").Click();
                //Lead
                _driver.FindElementById("btn_dados_lead").Click();
                //Status
                int i = 0;
                while (i == 0)
                {
                    if (_driver.FindElements(By.XPath("//android.widget.TextView[@text='Perdido']")).LongCount() > 0)
                    {
                        _driver.FindElement(By.XPath("//android.widget.TextView[@text='Perdido']")).Click();
                        i = 1;
                    }
                    else
                    {
                        TouchAction action = new TouchAction(_driver);
                        action.Press(500, 1700).MoveTo(500, 600).Release().Perform();
                    }
                }
                //Motivo remoçao lead
                i = 0;
                while (i == 0)
                {
                    if (_driver.FindElements(By.XPath("//android.widget.TextView[@text='Não tem interesse']")).LongCount() > 0)
                    {
                        _driver.FindElement(By.XPath("//android.widget.TextView[@text='Não tem interesse']")).Click();
                        i = 1;
                    }
                    else
                    {
                        TouchAction action = new TouchAction(_driver);
                        action.Press(500, 1700).MoveTo(500, 600).Release().Perform();
                    }
                }
                Assert.Pass();

            }

        }

        [Test]
        public void BuscaLista()
        {

            _driver.FindElementById("com.android.permissioncontroller:id/permission_deny_button").Click();
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/edt_chave_acesso").SendKeys("tnanuquev2");
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/btn_avancar").Click();
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/edt_cpf").SendKeys("82431221687");
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/edt_login").SendKeys("CNN0000004");
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/btn_entrar").Click();
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/edt_senha").SendKeys("04040404");
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/btn_entrar").Click();
            _driver.FindElementById("com.android.permissioncontroller:id/permission_allow_button").Click();
            _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/btn_sincronizar").Click();

            WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 5, 0));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("btn_sincronizar")));



            var infoCadastrais = _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/txv_status_informacoes").GetAttribute("text");
            var bensGruposPrecos = _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/txv_status_grupos_precos").GetAttribute("text");
            var leadsClientes = _driver.FindElementById("br.newm.afvconsorcio.nanuque:id/txv_cliente_lead").GetAttribute("text");
            if (infoCadastrais == "Atualizado" || bensGruposPrecos == "Atualizado" || leadsClientes == "Atualizado")
            {
                _driver.FindElementById("btn_sincronizar").Click();
                _driver.FindElementById("img_inicio_leads").Click();


                int i = 0;
                while (i == 0) { 
                    if (_driver.FindElements(By.XPath("//android.widget.TextView[@text='WALLACE REINGER']")).LongCount() > 0)
                    {
                        _driver.FindElement(By.XPath("//android.widget.TextView[@text='WALLACE REINGER']")).Click();
                        i = 1;
                    }
                    else
                    {
                        TouchAction action = new TouchAction(_driver);
                        action.Press(500, 1700).MoveTo(500, 600).Release().Perform();
                    }
                }

            }
        }
    }
}