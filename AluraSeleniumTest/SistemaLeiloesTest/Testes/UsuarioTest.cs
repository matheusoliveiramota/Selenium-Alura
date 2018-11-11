using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SistemaLeiloesTest.GeradoDeCenarios;
using SistemaLeiloesTest.Pages;
using System;
using Xunit;

namespace SistemaLeiloesTest
{
    public class UsuarioTest : IDisposable
    {
        IWebDriver driver;
        UsuariosPage usuariosPage;

        //ANTES DOS TESTES
        public UsuarioTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            this.driver = new ChromeDriver(@"C:\Users\Matheus\Documents\SELENIUM\ChromeDriver",options);
            usuariosPage = new UsuariosPage(driver);
            usuariosPage.Visita();
        }

        //DEPOIS DOS TESTES
        public void Dispose()
        {
            driver.Close();
        }

        [Fact(DisplayName = "Cadastrar Novo Usuário")]
        public void DeveCadastrarUsuario()
        {
            usuariosPage.NovoUsuario().Cadastrar("Higor Hernandes", "hhernandes@ig.com.br");

            Assert.True(usuariosPage.ExisteNaListagem("Higor Hernandes", "hhernandes@ig.com.br"));
        }

        [Fact(DisplayName = "Cadastrar Novo Usuário - Sem Preencher Nome")]
        public void CadastrarUsuarioSemPreencherNome()
        {
            NovoUsuarioPage novoUserPage = usuariosPage.NovoUsuario();
            novoUserPage.Cadastrar("", "washingtonluiz@uol.com");

            Assert.True(novoUserPage.ExisteMensagemParaNomeObrigatorio());
        }

        [Fact(DisplayName = "Cadastrar Novo Usuário - Sem Preencher Nome e E-mail")]
        public void CadastrarUsuarioSemPreencherNome_E_Email()
        {
            NovoUsuarioPage newUserPage = usuariosPage.NovoUsuario();
            newUserPage.Cadastrar("", "");

            Assert.True(newUserPage.ExisteMensagemParaNomeEmailObrigatorio());
        }

        [Fact(DisplayName = "Remover Usuário - Confirmando Exclusão")]
        public void RemoverUsuario()
        {
            CriadorDeCenarios criador = new CriadorDeCenarios(driver);
            criador.CadastrarUmUsuario("Higor Hernandes","hhernandes@ig.com.br");

            usuariosPage.ExcluirUsuario("hhernandes@ig.com.br");
            Assert.DoesNotContain("hhernandes@ig.com.br", driver.PageSource);
        }

        [Fact(DisplayName = "Remover Usuário - Cancelar a Exclusão")]
        public void CancelarExclusaoDeUsuario()
        {
            CriadorDeCenarios criador = new CriadorDeCenarios(driver);

            criador.CadastrarUmUsuario("Higor Hernandes", "hhernandes@ig.com.br");
            Assert.True(usuariosPage.CancelamentoDeExclusaoEhRealizado());
        }

        [Fact(DisplayName = "Editar Usuário")]
        public void EditarUsuario()
        {
            CriadorDeCenarios criador = new CriadorDeCenarios(driver);
            criador.CadastrarUmUsuario("Pedro Dente", "pdente@ig.com.br");

            usuariosPage.EditarUsuario("pdente@ig.com.br", "Pedro Bolado","pboladao@uol.com.br");
            Assert.True(usuariosPage.ExisteNaListagem("Pedro Bolado", "pboladao@uol.com.br"));
        }
    }
}
