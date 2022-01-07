using ProgramTervezesiMintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTervezesiMintak.Entities
{
    public class Present : Toy
    {
        public SolidBrush BoxBrush { get; private set; }
        public SolidBrush RibbonBrush { get; private set; }

        public Present(Color boxColor, Color ribbonColor)
        {
            BoxBrush = new SolidBrush(boxColor);
            RibbonBrush = new SolidBrush(ribbonColor);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(BoxBrush, new Rectangle(0, 0, Width, Height));
            g.FillRectangle(RibbonBrush, new Rectangle(0, 0, Width / 3, Height / 3));
            g.FillRectangle(RibbonBrush, new Rectangle(Width * 2 / 3, Height * 2 / 3, Width, Height));
            g.FillRectangle(RibbonBrush, new Rectangle(Width * 2 / 3, 0, Width, Height / 3));
            g.FillRectangle(RibbonBrush, new Rectangle(0, Width * 2 / 3, Width / 3, Height));
        }
    }
}
