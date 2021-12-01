using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Net.Http;
using Newtonsoft.Json;
using StockMarket;


namespace MarketMonitorApp
{
    public partial class MainWindow : Window
    {
        // Stock Properties to display
        public static double open { get; set; }
        public static double high { get; set; }
        public static double low { get; set; }
        public static double close { get; set; }
        public static long volume { get; set; }
        public string lastTicker { get; set; }

        // constants for messages/dialogs
        private const string infoMsg = "Retrieving info...";
        private const string invalidMsg = "The ticker was invalid.\nPlease enter a valid ticker.";
        private const string clearText = "";

        // main HttpClient
        public static HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            // display data labels
            stockInfo1.Inlines.Add(new Bold(new Run($"Ticker:")));
            stockInfo1.Inlines.Add(new LineBreak());
            stockInfo1.Inlines.Add(new Run("Open"));
            stockInfo1.Inlines.Add(new LineBreak());
            stockInfo1.Inlines.Add(new Run("Current/Close"));
            stockInfo1.Inlines.Add(new LineBreak());
            stockInfo1.Inlines.Add(new Run("Low"));
            stockInfo1.Inlines.Add(new LineBreak());
            stockInfo1.Inlines.Add(new Run("High"));
            stockInfo1.Inlines.Add(new LineBreak());
            stockInfo1.Inlines.Add(new Run("Volume"));

            MessageBox.Show(
                "Enter a stock into the field and click submit to check the price." +
                "\nClick Refresh to get the price of the last stock you checked." +
                "\nClick a stock in the History panel to view its price."
                );
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Retrieves user input and retrieves and displays stock price
            stockInfo2.Text = infoMsg;
            string ticker = tickerName.Text;
            string tickerUpper = ticker.ToUpper();
            tickerName.Clear();
                
            await RetrievePrice(ticker);

            if (open == 0)
            {
                stockInfo2.Text = invalidMsg;
            }
            else
            {
                SavedStocks.Items.Add(tickerUpper);
                lastTicker = tickerUpper;

                // display prices/data
                stockInfo2.Text = clearText;
                stockInfo2.Inlines.Add(new Bold(new Run($"{ticker}")));
                stockInfo2.Inlines.Add(new LineBreak());
                stockInfo2.Inlines.Add(new Run($"${open}"));
                stockInfo2.Inlines.Add(new LineBreak());
                stockInfo2.Inlines.Add(new Run($"${close}"));
                stockInfo2.Inlines.Add(new LineBreak());
                stockInfo2.Inlines.Add(new Run($"${low}"));
                stockInfo2.Inlines.Add(new LineBreak());
                stockInfo2.Inlines.Add(new Run($"${high}"));
                stockInfo2.Inlines.Add(new LineBreak());
                stockInfo2.Inlines.Add(new Run($" {volume}"));
            }
        }

        public static async Task RetrievePrice(string ticker)
        {
            // Sends request to microservice and saves the stock prices in the response object
            string baseUrl = "https://xvfo29na9j.execute-api.us-west-2.amazonaws.com/PROD?ticker=";

            HttpResponseMessage response = await client.GetAsync($"{baseUrl}{ticker}");
            var res = await response.Content.ReadAsStringAsync();
            List<Stock> stockInfo = JsonConvert.DeserializeObject<List<Stock>>(res);

            // check if we received a valid response from microservice
            if (stockInfo.Count == 0)
            {
                open = 0;
            }
            else
            {
                open = RoundPrice(stockInfo[0].Open);
                high = RoundPrice(stockInfo[0].High);
                low = RoundPrice(stockInfo[0].Low);
                close = RoundPrice(stockInfo[0].Close);
                volume = stockInfo[0].Volume;
            }
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            // Handles refresh button functionality
            if (lastTicker is null)
            {
                stockInfo2.Text = invalidMsg;
                return;
            }
            
            stockInfo2.Text = infoMsg;
            await RetrievePrice(lastTicker);

            // display prices/data
            stockInfo2.Text = clearText;
            stockInfo2.Inlines.Add(new Bold(new Run($"{lastTicker}")));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"${open}"));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"${close}"));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"${low}"));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"${high}"));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"{volume}"));
        }

        private async void HistoryListBox_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Retrieves and displays stock price when a ticker in the history section is selected
            var lbi = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;

            if (lbi is null)
            {
                return;
            }

            stockInfo2.Text = infoMsg;
            string ticker = lbi.Content.ToString();
            await RetrievePrice(ticker);
            lastTicker = ticker;

            // display prices/data
            stockInfo2.Text = clearText;
            stockInfo2.Inlines.Add(new Bold(new Run($"{ticker}")));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"${open}"));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"${close}"));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"${low}"));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($"${high}"));
            stockInfo2.Inlines.Add(new LineBreak());
            stockInfo2.Inlines.Add(new Run($" {volume}"));
        }

        public static double RoundPrice(double price)
        {
            return Math.Round(price, 2, MidpointRounding.AwayFromZero);
        }
    }
}
