using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SistemaLeiloesTest.Pages
{
    class NovoLeilaoPage
    {
        IWebDriver driver;

        public NovoLeilaoPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Cadastra(string nomeProduto, double valorProduto, string nomeUsuario, bool EhUsado)
        {
            driver.FindElement(By.Name("leilao.nome")).SendKeys(nomeProduto);
            driver.FindElement(By.Name("leilao.valorInicial")).SendKeys(valorProduto.ToString());

            SelectElement selectUsuario = new SelectElement(driver.FindElement(By.Name("leilao.usuario.id")));
            selectUsuario.SelectByText(nomeUsuario);

            IWebElement cbxUsado = driver.FindElement(By.Name("leilao.usado"));
            if (!cbxUsado.Selected)
            {
                if (EhUsado)
                    cbxUsado.Click();                      
            }
            else
            {
                if (!EhUsado)
                    cbxUsado.Click();
            }
            cbxUsado.Submit();
        }
        public void CadastraSemPreencherValor(string nomeProduto, double valorProduto, string nomeUsuario, bool EhUsado)
        {
            driver.FindElement(By.Name("leilao.nome")).SendKeys(nomeProduto);

            SelectElement selectUsuario = new SelectElement(driver.FindElement(By.Name("leilao.usuario.id")));
            selectUsuario.SelectByText(nomeUsuario);

            IWebElement cbxUsado = driver.FindElement(By.Name("leilao.usado"));
            if (!cbxUsado.Selected)
            {
                if (EhUsado)
                    cbxUsado.Click();
            }
            else
            {
                if (!EhUsado)
                    cbxUsado.Click();
            }
            cbxUsado.Submit();
        }
        public bool ExisteValidacaoParaNome()
        {
            return driver.PageSource.Contains("Nome obrigatorio");
        }
        public bool ExisteValidacaoParaValor()
        {
            return driver.PageSource.Contains("Valor inicial deve ser maior que zero");
        }
    }
}
