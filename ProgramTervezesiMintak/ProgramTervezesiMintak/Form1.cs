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
        private ToyFactory _toyFactory;
        public ToyFactory Factory
        {
            get { return _toyFactory; } 
            set { _toyFactory = value; } 
        }
        public Form1()
        {
            InitializeComponent();
            Factory = new ToyFactory();
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
    }
}
