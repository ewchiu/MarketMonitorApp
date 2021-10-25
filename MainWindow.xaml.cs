using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketMonitorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ticker_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tickerName.Text) && tickerName.Text == "MSFT")
            {
                SavedStocks.Items.Add(tickerName.Text);
                tickerName.Clear();

                // Generate fake data
                stockInfo1.Inlines.Add(new Bold(new Run("MSFT")));
                stockInfo1.Inlines.Add(new LineBreak());
                stockInfo1.Inlines.Add(new Run("Current Price: $138.40"));
                stockInfo1.Inlines.Add(new LineBreak());
                stockInfo1.Inlines.Add(new Run("52 Week High:  $138.40"));
                stockInfo1.Inlines.Add(new LineBreak());
                stockInfo1.Inlines.Add(new Run("52 Week Low:   $87.90"));
                stockInfo1.Inlines.Add(new LineBreak());
                stockInfo1.Inlines.Add(new Run("P/E Ratio:     30.57"));
            }
        }
    }
}
