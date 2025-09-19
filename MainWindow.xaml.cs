using System;
using System.Threading;
using System.Windows;
using MtkClient.Library.Abstractions;
using MtkClient.Library.Services;

namespace CoreFlashToolPro
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource? _cts;
        private readonly IMtkService _mtk;

        public MainWindow()
        {
            InitializeComponent();
            var exe = System.IO.Path.Combine(AppContext.BaseDirectory, "mtk_old.exe");
            _mtk = new MtkService(exe);
        }

        // İPTAL butonu için handler
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            _cts?.Cancel();
            // LogTextBox varsa şöyle yazabilirsin:
            // LogTextBox.AppendText("İşlem iptal istendi." + Environment.NewLine);
        }

        // Örnek: uzun süren bir iş başlatırken _cts oluştur
        private async void BtnReadInfo_Click(object sender, RoutedEventArgs e)
        {
            _cts = new CancellationTokenSource();
            try
            {
                var info = await _mtk.ReadInfoAsync(_cts.Token);
                // info’yu UI’ya yaz…
            }
            catch (OperationCanceledException)
            {
                // iptal edildi
            }
            finally
            {
                _cts.Dispose();
                _cts = null;
            }
        }
    }
}
