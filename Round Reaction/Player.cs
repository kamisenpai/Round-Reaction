using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Round_Reaction
{
    public class Player

    {
        PointF loc;
        int size;
        Color intC;
        public Player()
        {
            size = 10;
            loc = new PointF();
            intC = Color.FromArgb(100, 30, 110);
        }

        public void draw()
        {
            PointF tempPoint1,tempPoint2;
            loc.X = Engine.mc.X-size; loc.Y = Engine.mc.Y-size;
            tempPoint1 = loc;
            tempPoint2 = loc;
            tempPoint1.X = loc.X - size +2;
            tempPoint1.Y = loc.Y - size /4 -2;
            //tempPoint2.X = loc.X + size / 4;
            //tempPoint2.Y = loc.Y + size / 4;
            SolidBrush sb = new SolidBrush(intC);
            Engine.g.FillEllipse(sb, loc.X - size, loc.Y - size, 2 * size, 2 * size);
            Engine.g.DrawLine(Pens.Black, loc.X- size/2, loc.Y - size / 2, loc.X + size / 2, loc.Y + size / 2);
            Engine.g.DrawLine(Pens.Black, loc.X + size/2, loc.Y - size/2, loc.X - size / 2, loc.Y + size/2);
            Engine.g.DrawRectangle(Pens.Black,tempPoint1.X,tempPoint1.Y , 2*size-3, size-2);
            Engine.g.FillRectangle(Brushes.Blue, tempPoint1.X, tempPoint1.Y, 2 * size - 2, size - 1);
            Engine.g.DrawEllipse(Pens.Black, loc.X - size, loc.Y - size, 2 * size, 2 * size);
        }
        public PointF GetXY()
        {
            return loc;
        }
        public int getSize()
        {
            return size;
        }

    }
}
