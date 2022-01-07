using ProgramTervezesiMintak.Abstractions;
using ProgramTervezesiMintak.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramTervezesiMintak
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();
        private IToyFactory _toyFactory;
        private Toy _nextToy;
        public IToyFactory Factory
        {
            get { return _toyFactory; } 
            set { _toyFactory = value;
                DisplayNext();
            } 
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = lblNext.Top + lblNext.Height + 20;
            _nextToy.Left = lblNext.Left;
            Controls.Add(_nextToy);
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Toy b = Factory.CreateNew();
            _toys.Add(b);
            b.Left = -b.Width;
            mainPanel.Controls.Add(b);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var b in _toys)
            {
                b.MoveToy();
                if (b.Left > maxPosition)
                    maxPosition = b.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestToy = _toys[0];
                mainPanel.Controls.Remove(oldestToy);
                _toys.Remove(oldestToy);
            }
        }

        private void btnSelectCar_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void btnSelectBall_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory() { BallColor = btnBallColor.BackColor };
        }

        private void btnBallColor_Click(object sender, EventArgs e)
        {
            Button kattintott = (Button)sender;
            ColorDialog cd = new ColorDialog();
            cd.Color = kattintott.BackColor;
            if (cd.ShowDialog() != DialogResult.OK) return;
            kattintott.BackColor = cd.Color;

        }

        private void btnSelectPresent_Click(object sender, EventArgs e)
        {
            Factory = new PresentFactory()
            {
                BoxColor = btnBoxColor.BackColor,
                RibbonColor = btnRibbonColor.BackColor
            };
        }
    }
}
