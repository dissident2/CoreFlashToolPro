using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows; // WPF
using MtkClient.Library.Abstractions;
using MtkClient.Library.Services;

namespace CoreFlashToolPro
{
    public partial class MainWindow : Window
    {
        private readonly IMtkService _mtk;

        public MainWindow()
        {
            InitializeComponent();

            // Çalıştırılacak mtk aracının yolu (çıkış klasörüne kopyalanmış olmalı)
            var exe = Path.Combine(AppContext.BaseDirectory, "mtk_old.exe"); // veya "mtk.exe"
            _mtk = new MtkService(exe);
        }

        private async void BtnReadInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var info = await _mtk.ReadInfoAsync(CancellationToken.None);
                MessageBox.Show(
                    $"Cihaz bilgisi:\n" +
                    $"CPU: {info.Cpu}\n" +
                    $"HW: {info.HwCode}/{info.HwSubCode}\n" +
                    $"HW Ver: {info.HwVer}\n" +
                    $"SW Ver: {info.SwVer}",
                    "Read Info",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // XAML: Click="BtnPrintGpt_Click" olan butonun işleyicisi
        private async void BtnPrintGpt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Partition listesini al
                var parts = await _mtk.ListPartitionsAsync(CancellationToken.None);

                if (parts is null || parts.Count == 0)
                {
                    MessageBox.Show("Herhangi bir bölüm (partition) bulunamadı.", "GPT", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Basit bir özet hazırla
                var sb = new StringBuilder();
                sb.AppendLine($"Toplam {parts.Count} bölüm bulundu:");
                foreach (var p in parts)
                    sb.AppendLine($"{p.Name}  |  offset=0x{p.Offset:X}  size=0x{p.Length:X}");

                MessageBox.Show(sb.ToString(), "GPT / Partition List", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
