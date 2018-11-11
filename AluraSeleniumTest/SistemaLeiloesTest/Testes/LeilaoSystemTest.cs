using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SistemaLeiloesTest.GeradoDeCenarios;
using SistemaLeiloesTest.Pages;
using System;
using System.IO;
using Xunit;

namespace SistemaLeiloesTest
{
    public class LeilaoSystemTest : IDisposable
    {
        IWebDriver driver;
        LeilaoPage leilaoPage;
        UsuariosPage userPage;

        public LeilaoSystemTest()
        {
            ChromeOptions options = new ChromeOptions();
            // Modo Headless
                //options.AddArgument("--headless");
            string testProjectPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName;
            driver = new ChromeDriver(testProjectPath + "\\Testes\\Driver\\ChromeDriver",options);
            leilaoPage = new LeilaoPage(driver);
            userPage = new UsuariosPage(driver);
            userPage.Visita();
        }
        public void Dispose()
        {
            driver.Close();
        }

        [Fact(DisplayName = "Adicionar Novo Leilão - Produto Usado")]
        public void DeveAdicionarLeilaoUsado()
        {
            userPage.NovoUsuario().Cadastrar("Matheus Mota", "mmota@rdc-ferias.com.br");
            leilaoPage.Visita();
            leilaoPage.Novo().Cadastra("Celular ASUS",550,"Matheus Mota",true);
            Assert.True(leilaoPage.ExisteNaListagem("Celular ASUS", 550, "Matheus Mota", true));
        }

        [Fact(DisplayName = "Adicionar Novo Leilão - Sem Preencher Nome")]
        public void CadastrarLeilaoSemNome()
        {
            userPage.NovoUsuario().Cadastrar("Vitor Freitas", "vitindazl@ig.com.br");
            leilaoPage.Visita();

            leilaoPage.Novo().Cadastra("",500,"Vitor Freitas",true);
            Assert.True(new NovoLeilaoPage(driver).ExisteValidacaoParaNome());
        }

        [Fact(DisplayName = "Adicionar Novo Leilão - Sem Preencher Valor")]
        public void CadastrarLeilaoSemValor()
        {
            userPage.NovoUsuario().Cadastrar("Ana Furtado", "afurtado@ig.com.br");
            leilaoPage.Visita();

            leilaoPage.Novo().CadastraSemPreencherValor("Notebook", 500, "Ana Furtado", true);
            Assert.True(new NovoLeilaoPage(driver).ExisteValidacaoParaValor());
        }


        [Fact(DisplayName = "Adicionar Novo Leilão - Com Valor Igual a Zero")]
        public void CadastrarLeilaoComValorIgualAZero()
        {
            userPage.NovoUsuario().Cadastrar("Pedro Dente", "pdente@ig.com.br");
            leilaoPage.Visita();

            leilaoPage.Novo().Cadastra("Teclado", 0, "Pedro Dente", true);
            Assert.True(new NovoLeilaoPage(driver).ExisteValidacaoParaValor());
        }

        [Fact(DisplayName = "Dar Lance")]
        public void DeveDarLance()
        {
            CriadorDeCenarios criador = new CriadorDeCenarios(driver);
            criador.CadastrarUmUsuario("Pedro Dente", "pdente@ig.com.br");
            criador.CadastrarUmUsuario("Matheus Mota", "mmota@ig.com.br");
            criador.CadastrarUmLeilao("Celular ASUS", 100, "Pedro Dente", true);
            criador.DarUmLance("Celular ASUS","Matheus Mota",600);

            DetalhesDoLeilaoPage detalhesPage = new DetalhesDoLeilaoPage(driver);
            Assert.True(detalhesPage.ExisteLance("Matheus Mota","600"));
        }

        [Fact(DisplayName = "Adicionar Novo Leilão - Produto Novo")]
        public void DeveAdicionarLeilaoNovo()
        {
            CriadorDeCenarios criador = new CriadorDeCenarios(driver);
            criador.CadastrarUmUsuario("Mestre Wallace", "mwallace@senai-sp.com.br");
            criador.CadastrarUmLeilao("Aço ABNT", 1000, "Mestre Wallace", false);
            Assert.True(leilaoPage.ExisteNaListagem("Aço ABNT", 1000, "Mestre Wallace", false));
        }
    }
}
