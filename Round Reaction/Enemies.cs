using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Round_Reaction
{
    public class Enemies
    {
        float alfa;
        float speed;
        float angleX, angleY;
        float uConst = 0.0f;
        PointF loc;
        
        //stabilirea parametrilor: locatia, unghiul, viteza, marimea si culoarea punctului
        public Enemies()
        {
          
            loc = new PointF();
            //alfa ia valoare random intre 0 si 2pi
            alfa = (float)Engine.rnd.NextDouble() * (2.0f * (float)Math.PI);
            speed = (float)Engine.rnd.NextDouble() * 3.0f + 1.0f;

            loc.X = Engine.rnd.Next(20, Engine.px-20);
            loc.Y = Engine.rnd.Next(20, Engine.py-20);


            byte tR = (byte)Engine.rnd.Next(256);
            byte tG = (byte)Engine.rnd.Next(256);
            byte tB = (byte)Engine.rnd.Next(256);

            //ia culoarea random
            intC = Color.FromArgb(tR, tG, tB);
            size = Engine.rnd.Next(7, 12);
        }

        Color intC;
        int size;
        
        //va desena la fiecare tick
        public void draw()
        {
            SolidBrush sb = new SolidBrush(intC);
            Engine.g.FillEllipse(sb, loc.X - size, loc.Y - size, 2 * size, 2 * size);
            Engine.g.DrawEllipse(Pens.Black, loc.X - size, loc.Y - size, 2 * size, 2 * size);
        }

        //cat de repede se va misca la fiecare tick
        public void move()
        {
            // daca alfa e mai mare de un anumit unghi, atunci modifica unghiul in functie de asta
            // (float)Math.PI/2.0f
            // (float)Math.PI
            // 3.0f * (float)Math.PI/2.0f 
            // 2.0f * (float)Math.PI
            
            //daca totul e in regula, calculeaza ca in mod normal
            if (loc.X > 0 + size && loc.X < Engine.px - size && loc.Y > 0 + size && loc.Y < Engine.py - size)
            {
                Refresh();
            }
            else
            {
                //daca punctul se loveste de peretele stang, schimba in dreapta
                if (loc.X < 0 + size)
                {
                    loc.X = loc.X + speed + 2;
                    speed = -speed;
                    alfa = dRnd(3.0f * (float)Math.PI / 2.0f + uConst, (float)Math.PI / 2.0f - uConst);
                    Refresh();
                }
                else
                //daca se loveste de peretele drept, schimba in stanga
                if (loc.X > Engine.px - size)
                {
                    loc.X = loc.X - speed - 2;
                    speed = -speed;
                    alfa = dRnd((float)Math.PI / 2.0f + uConst, 3.0f * (float)Math.PI / 2.0f - uConst);
                    Refresh();
                }
                //daca se loveste de partea de sus, schimba in jos
                if (loc.Y < 0 + size)
                {
                    loc.Y = loc.Y + speed + 2;
                    speed = -speed;
                    alfa = dRnd((float)Math.PI + uConst, 2.0f * (float)Math.PI - uConst);
                    Refresh();
                }
                //daca se loveste de partea de jos schimba in sus
                else
                if (loc.Y > Engine.py - size)
                {
                    loc.Y = loc.Y - speed - 2;
                    speed = -speed;
                    alfa = dRnd(0.0f + uConst, (float)Math.PI - uConst);
                    Refresh();
                }
            } 
        }
        //calculeaza coordonatele noi
        public void Refresh()
        {
            angleX = (float)Math.Cos(alfa);
            angleY = (float)Math.Sin(alfa);
            loc.X = loc.X + speed * angleX;
            loc.Y = loc.Y + speed * angleY;
        }

        //functie random pentru a simplifica scrierea codului
        public float dRnd(float minimum, float maximum)
        {
            Random random = new Random();
            return (float)random.NextDouble() * (maximum - minimum) + minimum;
        }

        //obtine coordonatele curente ale inamicului
        public PointF GetXY()
        {

            return loc;
        }

        //obtine marimea curenta a inamicului
        public int getSize()
        {
            return size;
        }

    }

}
