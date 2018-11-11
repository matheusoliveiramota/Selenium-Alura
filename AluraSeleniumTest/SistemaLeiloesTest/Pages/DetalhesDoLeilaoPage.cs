using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeiloesTest.Pages
{
    class DetalhesDoLeilaoPage
    {
        IWebDriver driver;

        public DetalhesDoLeilaoPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void DarLance(string nomeUsuario, double lance)
        {
            SelectElement selectUsers = new SelectElement(driver.FindElement(By.Name("lance.usuario.id")));
            selectUsers.SelectByText(nomeUsuario);

            driver.FindElement(By.Name("lance.valor")).SendKeys(lance.ToString());
            driver.FindElement(By.Id("btnDarLance")).Click();
        }

        public bool ExisteLance(string nomeUsuario, string valorLance)
        {
            
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.Id("lancesDados")));
                return true;  
             } 
            catch(WebDriverTimeoutException e)
            {
                e.Source = "Tempo limite para encontrar elemento esgotado.";
                return false;
            }
        }
    }
}
