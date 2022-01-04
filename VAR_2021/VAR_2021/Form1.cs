using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAR_2021.Entities;

namespace VAR_2021
{
    public partial class Form1 : Form
    {

        PortfolioEntities context = new PortfolioEntities();
        List<Tick> ticks;
        List<PortfolioItem> Portfolio = new List<PortfolioItem>();
        public Form1()
        {
            InitializeComponent();
            ticks = context.Ticks.ToList();
            dataGridView1.DataSource = ticks;

            CreatePortfolio();
        }

        private void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 }); 
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
