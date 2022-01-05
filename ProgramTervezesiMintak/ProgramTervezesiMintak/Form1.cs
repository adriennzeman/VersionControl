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
        private List<Ball> _balls = new List<Ball>();
        private BallFactory _ballFactory;
        public BallFactory Factory
        {
            get { return _ballFactory; } 
            set { _ballFactory = value; } 
        }
        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            Ball b = Factory.CreateNew();
            _balls.Add(b);
            b.Left = -b.Width;
            mainPanel.Controls.Add(b);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var b in _balls)
            {
                b.MoveToy();
                if (b.Left > maxPosition)
                    maxPosition = b.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _balls[0];
                mainPanel.Controls.Remove(oldestBall);
                _balls.Remove(oldestBall);
            }
        }
    }
}
