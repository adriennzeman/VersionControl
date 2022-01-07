using MnbCurrencyReader.Entities;
using MnbCurrencyReader.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace MnbCurrencyReader
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            ValutaLekerdezes(); 
            dgwRates.DataSource = Rates;
            cbCurrency.DataSource = Currencies;
            RefreshData();
        }

        private void ValutaLekerdezes()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody();
            var response = mnbService.GetCurrencies(request);
            var result = response.GetCurrenciesResult;
            Console.WriteLine(result);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            foreach(XmlElement element in xml.DocumentElement.ChildNodes[0])
            {
                Currencies.Add(element.InnerText);
            }
        }

        private void RefreshData()
        {
            Rates.Clear();
            var result = ArfolyamLekerdezes();
            ArfolyamFeldolgozas(result);
            ChartFeltoltes();
        }

        private void ChartFeltoltes()
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void ArfolyamFeldolgozas(string result)
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);
                rate.Date = DateTime.Parse(element.GetAttribute("date"));
                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null) continue;
                rate.Currency = childElement.GetAttribute("curr");
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText.Replace(',','.')); //Tizedes vesszot pontra cserelem
                if (unit != 0) rate.Value = value / unit;
            }
        }

        private string ArfolyamLekerdezes()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = cbCurrency.SelectedItem.ToString(),
                startDate = dtpStart.Value.ToString("yyyy-MM-dd"),
                endDate = dtpEnd.Value.ToString("yyyy-MM-dd")
            };
            var response= mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
            Console.WriteLine(result);
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void cbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
