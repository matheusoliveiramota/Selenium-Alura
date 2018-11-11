using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace SistemaLeiloesTest.Pages
{
    class LeilaoPage
    {
        IWebDriver driver;

        public LeilaoPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Visita()
        {
            driver.Navigate().GoToUrl(new UrlBase().GetUrlBase() +"/leiloes");
        }

        public NovoLeilaoPage Novo()
        {
            driver.FindElement(By.LinkText("Novo Leilão")).Click();
            return new NovoLeilaoPage(driver);
        }

        public bool ExisteNaListagem(string nomeProduto, double valorProduto, string usuario, bool ehUsado)
        {
            return driver.PageSource.Contains(nomeProduto) &&
                    driver.PageSource.Contains(valorProduto.ToString()) &&
                        driver.PageSource.Contains(usuario) &&
                            driver.PageSource.Contains(ehUsado ? "Sim" : "Não");
        }

        public void NovoLance(string nomeDoProduto)
        {
            int i = -1;
            List<IWebElement> listaProdutos = driver.FindElements(By.XPath("//table/tbody/tr/td[1]")).ToList();
            foreach(var prod in listaProdutos)
            {
                if(prod.Text == nomeDoProduto)
                {
                    i = listaProdutos.FindIndex(e => e.Equals(prod));
                    driver.FindElements(By.XPath("//a[text()='exibir']"))[i].Click();
                    break;
                }
            }
            if (i < 0)
                throw new NoSuchElementException("Produto informado não encontrado!");
        }
    }
}
