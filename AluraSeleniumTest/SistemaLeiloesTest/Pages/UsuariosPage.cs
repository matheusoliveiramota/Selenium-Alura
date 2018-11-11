using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeiloesTest.Pages
{
    class UsuariosPage
    {
        private IWebDriver driver;

        public UsuariosPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Visita()
        {
            driver.Navigate().GoToUrl(new UrlBase().GetUrlBase() + "/apenas-teste/limpa");
            driver.Navigate().GoToUrl(new UrlBase().GetUrlBase() + "/usuarios");
        }

        public NovoUsuarioPage NovoUsuario()
        {
            driver.FindElement(By.LinkText("Novo Usuário")).Click();
            return new NovoUsuarioPage(driver);
        }

        public bool ExisteNaListagem(string nome, string email)
        {
            return (driver.PageSource.Contains(nome) && driver.PageSource.Contains(email));
        }

        public void ExcluirUsuario(string nomeOuEmail)
        {
            int i;
            List<IWebElement> nomesDeUsuarios = driver.FindElements(By.XPath("//table/tbody/tr/td")).ToList();
            for(i=0; i < nomesDeUsuarios.Count; i++)
            {
                if(nomesDeUsuarios[i].Text == nomeOuEmail)
                {
                    int posicao = (i / 3) + 1;
                    driver.FindElements(By.TagName("button"))[posicao - 1].Click();

                    IAlert alert = driver.SwitchTo().Alert();
                    alert.Accept();
                    break;
                }
            }

            if(i == nomesDeUsuarios.Count)
                throw new NoSuchElementException("Nenhum Usuário Foi Encontrado!");
        }

        public bool CancelamentoDeExclusaoEhRealizado()
        {
            string nomeUsuario = driver.FindElement(By.XPath("//table/tbody/tr[2]/td[1]")).Text;
            driver.FindElement(By.TagName("button")).Click();

            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();

            return driver.PageSource.Contains(nomeUsuario);
        }

        public void EditarUsuario(string nomeOuEmailDoUsuarioExistene, string novoNomeUsuario, string novoEmailUsuario)
        {
            int i = -1;
            List<IWebElement> listaColunas = driver.FindElements(By.XPath("//table/tbody/tr/td")).ToList();
            foreach(var ele in listaColunas)
            {
                if (ele.Text == nomeOuEmailDoUsuarioExistene)
                {
                    i = listaColunas.FindIndex(e => e.Equals(ele));
                    // i = 'Posição da Linha Onde Se Encontra o Nome'
                    i = (i / 3) + 2;
                    break;
                }
            }

            if(i < 0)
                throw new NoSuchElementException("O usuario informado não existe!");
            else
            {
                driver.FindElement(By.XPath("//table/tbody/tr[" + i + "]//a[text() = 'editar']")).Click();

                NovoUsuarioPage newUSerPage = new NovoUsuarioPage(driver);
                newUSerPage.LimparCampos();
                newUSerPage.Cadastrar(novoNomeUsuario,novoEmailUsuario); 
            }
        }
    }
}
