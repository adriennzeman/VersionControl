using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramTervezesiMintak.Abstractions
{
    public abstract class Toy: Label
    {
        public Toy()
        {
            AutoSize = false;
            Width = Height = 50;
            Paint += Toy_Paint;
            Click += Toy_Click;

        }

        private void Toy_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetTypeName());
        }

        private void Toy_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected abstract void DrawImage(Graphics g);

        protected abstract string GetTypeName();

        public virtual void MoveToy()
        {
            Left += 1;
        }
    }
}
