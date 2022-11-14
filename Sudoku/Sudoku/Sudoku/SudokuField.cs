using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    class SudokuField : Button
    {
        private int _value;


        public int Value
        {
            get { return _value; }
            set { 
                _value = value;
                if (_value < 0) _value = 9;
                if (_value > 9) _value = 0;
                if (_value == 0) _value = "";
                else Text = _value.ToString();
            }
        }

        public SudokuField()
        {
            Height = 30;
            Width = Height;
            BackColor = Color.White;
            Value = 0;
            MouseDown += SudokuField_MouseDown;
        }

        private void SudokuField_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) Value++;
            if (e.Button == MouseButtons.Right) Value--;

        }
    }
}
