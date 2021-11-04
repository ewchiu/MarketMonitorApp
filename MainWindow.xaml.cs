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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using QuickType;


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
            if (!string.IsNullOrWhiteSpace(tickerName.Text))
            {
                string ticker = tickerName.Text;
                SavedStocks.Items.Add(ticker);
                tickerName.Clear();

                stockInfo1.Inlines.Add(new Bold(new Run(ticker)));
                stockInfo1.Inlines.Add(new LineBreak());

                RetrievePrice(ticker);

                // Generate fake data
                //stockInfo1.Inlines.Add(new Bold(new Run("MSFT")));
                //stockInfo1.Inlines.Add(new LineBreak());
                //stockInfo1.Inlines.Add(new Run("Current Price: $138.40"));
                //stockInfo1.Inlines.Add(new LineBreak());
                //stockInfo1.Inlines.Add(new Run("52 Week High:  $138.40"));
                //stockInfo1.Inlines.Add(new LineBreak());
                //stockInfo1.Inlines.Add(new Run("52 Week Low:   $87.90"));
                //stockInfo1.Inlines.Add(new LineBreak());
                //stockInfo1.Inlines.Add(new Run("P/E Ratio:     30.57"));
            }
        }

        public static async Task RetrievePrice(string ticker)
        {
            HttpClient client = new HttpClient();
            string baseUrl = "https://xvfo29na9j.execute-api.us-west-2.amazonaws.com/PROD?ticker=";

            HttpResponseMessage response = await client.GetAsync($"{baseUrl}{ticker}");
            var json = response.Content.ReadAsStringAsync().Result;
            var userStock = Stock.FromJson(json);

            Console.WriteLine(userStock.GetType());
        }
    }
}
