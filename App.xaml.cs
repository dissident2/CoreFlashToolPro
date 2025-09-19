using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;   // <— ÖNEMLİ

namespace CoreFlashToolPro
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; } = default!;

        public App()
        {
            // appsettings.json yükle (çalışma dizininde olmalı)
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
    }
}
