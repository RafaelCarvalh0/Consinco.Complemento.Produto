using Consinco.Complemento.Produto.Application.Services;
using Consinco.Complemento.Produto.Data.Configuration;
using Consinco.Complemento.Produto.Data.Repositories;
using Consinco.Complemento.Produto.Domain.Interfaces;
using Consinco.Complemento.Produto.Domain.Interfaces.Services;
using Consinco.Complemento.Produto.UI.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using WinForms = System.Windows.Forms;

namespace Consinco.Complemento.Produto.UI
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            WinForms.Application.EnableVisualStyles();
            WinForms.Application.SetCompatibleTextRenderingDefault(false);

            ConfigurarServicos();

            var frmListagem = ServiceProvider.GetRequiredService<FrmListagem>();
            WinForms.Application.Run(frmListagem);
        }

        private static void ConfigurarServicos()
        {
            var services = new ServiceCollection();

            var connectionString = ConfigurationManager.ConnectionStrings["OracleConsinco"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'OracleConsinco' não encontrada no App.config.");

            services.AddLogging(builder =>
            {
                builder.AddDebug();
            });

            services.AddTransient<IUniversal, Universal>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<Universal>>();
                return new Universal(connectionString, logger);
            });

            services.AddScoped<IComplementoRepository, ComplementoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IComplementoService, ComplementoService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddTransient<FrmListagem>();
            services.AddTransient<FrmCadastro>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}