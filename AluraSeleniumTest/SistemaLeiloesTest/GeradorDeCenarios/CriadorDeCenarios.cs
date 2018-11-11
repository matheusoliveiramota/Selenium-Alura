using OpenQA.Selenium;
using SistemaLeiloesTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLeiloesTest.GeradoDeCenarios
{
    class CriadorDeCenarios
    {
        IWebDriver driver;

        public CriadorDeCenarios(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void CadastrarUmUsuario(string nome, string email)
        {
            UsuariosPage usuarioPage = new UsuariosPage(driver);
            usuarioPage.NovoUsuario().Cadastrar(nome, email);
        }

        public void CadastrarUmLeilao(string nomeProduto, int precoProduto, string usuarioProduto, bool ehUsado)
        {
            LeilaoPage leilaoPage = new LeilaoPage(driver);
            leilaoPage.Visita();
            leilaoPage.Novo().Cadastra(nomeProduto,precoProduto,usuarioProduto,ehUsado);
        }

        public void DarUmLance(string nomeProduto, string nomeUsuario, int valorLance)
        {
            LeilaoPage leilaoPage = new LeilaoPage(driver);
            leilaoPage.NovoLance(nomeProduto);
            DetalhesDoLeilaoPage detalhesPage = new DetalhesDoLeilaoPage(driver);
            detalhesPage.DarLance(nomeUsuario, valorLance);
        }
    }
}
