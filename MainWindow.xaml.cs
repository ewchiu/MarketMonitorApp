using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using StockMarket;


namespace MarketMonitorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Stock Properties to display
        public static double open { get; set; }
        public static double high { get; set; }
        public static double low { get; set; }
        public static double close { get; set; }
        public static long volume { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ticker_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tickerName.Text))
            {
                string ticker = tickerName.Text;
                SavedStocks.Items.Add(ticker);
                tickerName.Clear();
                stockInfo1.Text = "";

                stockInfo1.Inlines.Add(new Bold(new Run($"Ticker: {ticker}")));
                stockInfo1.Inlines.Add(new LineBreak());

                await RetrievePrice(ticker);

                // display stock data
                stockInfo1.Inlines.Add(new Run($"Open: ${open}"));
                stockInfo1.Inlines.Add(new LineBreak());
                stockInfo1.Inlines.Add(new Run($"Current/Close: ${close}"));
                stockInfo1.Inlines.Add(new LineBreak());
                stockInfo1.Inlines.Add(new Run($"Low: ${low}"));
                stockInfo1.Inlines.Add(new LineBreak());
                stockInfo1.Inlines.Add(new Run($"High: ${high}"));
                stockInfo1.Inlines.Add(new LineBreak());
                stockInfo1.Inlines.Add(new Run($"Volume: {volume}"));
            }
            else
            {
                stockInfo1.Inlines.Add(new Run("The ticker you entered was invalid"));
            }
        }

        public static async Task RetrievePrice(string ticker)
        {
            HttpClient client = new HttpClient();
            string baseUrl = "https://xvfo29na9j.execute-api.us-west-2.amazonaws.com/PROD?ticker=";

            HttpResponseMessage response = await client.GetAsync($"{baseUrl}{ticker}");
            var res = await response.Content.ReadAsStringAsync();
            List<Stock> stockInfo = JsonConvert.DeserializeObject<List<Stock>>(res);

            open = stockInfo[0].Open;
            high = stockInfo[0].High;
            low = stockInfo[0].Low;
            close = stockInfo[0].Close;
            volume = stockInfo[0].Volume;

        }
    }
}
