using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeiloesTest.Pages
{
    class NovoUsuarioPage
    {
        private IWebDriver driver;

        public NovoUsuarioPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Cadastrar(string nome, string email)
        {
            IWebElement campoNome = driver.FindElement(By.Name("usuario.nome"));
            campoNome.SendKeys(nome);

            driver.FindElement(By.Name("usuario.email")).SendKeys(email);

            campoNome.Submit();
        }

        public bool ExisteMensagemParaNomeObrigatorio()
        {
            return driver.PageSource.Contains("Nome obrigatorio");
        }

        public bool ExisteMensagemParaNomeEmailObrigatorio()
        {
            return (driver.PageSource.Contains("Nome obrigatorio") && driver.PageSource.Contains("E-mail obrigatorio"));
        }

        public void LimparCampos()
        {
            driver.FindElement(By.Name("usuario.nome")).Clear();
            driver.FindElement(By.Name("usuario.email")).Clear();
        }
    }
}
