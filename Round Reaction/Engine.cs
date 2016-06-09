using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace Round_Reaction
{
    public static class Engine
    {
        public static Bitmap bmp;
        public static Graphics g;
        public static int px, py, army=40;

        public static Random rnd = new Random();
        public static PointF mc;
        public static List<Enemies> myDots = new List<Enemies>();
        public static List<Player> myP = new List<Player>();
        public static List<PointF> enemyPoint = new List<PointF>();
        public static bool ok = false;
        public static float d, r1, r2;

        //initializarea listei de puncte(obiecte)
        public static void initPoints()
        {
            for (int i = 0; i < army; i++)
                myDots.Add(new Enemies());
        }

        //initializare jucator
        public static void initPlayer()
        {
            myP.Add(new Player());
        }

        //desenare pe ecran jucator
        public static void initPlayerDraw()
        {
            foreach (Player p in myP)
                p.draw();
        }

        // initializarea desenului in picturebox
        public static void initDraw()
        {
            bmp = new Bitmap(px, py);
            g = Graphics.FromImage(bmp);

            g.Clear(Color.White);
        }

        //deseneaza punctele
        public static void initDP()
        {
            foreach (Enemies e in myDots)
            {
                e.draw();
            }
        }

        //misca inamicii
        public static void Move()
        {
            foreach (Enemies e in myDots)
            {
                e.move();
            }
        }

        //obtine coordonatele inamicilor intr-o lista
        public static void getEnemy()
        {
            enemyPoint.Clear();
            foreach (Enemies e in myDots)
            {
                enemyPoint.Add(e.GetXY());
            }
        }
       
        //d^2 = (x2-x1)^2 + (y2-y1)^2
        //  x1= centru player x2= centru Enemy d= diametrul
        //if (r1+r2)^2 <= d^2 you have a collision.

        //functia pentru coliziune intre jucator si inamici
        public static bool Collision()
        {
            // int colCount=0;
            foreach (Enemies e in myDots)
            {
                foreach (Player p in myP)
                { 
                    r1 = e.getSize();
                    r2 = p.getSize();
                    //formula pentru distanta intre cei 2 inamici
                    d = distEuclid(e.GetXY().X, e.GetXY().Y, p.GetXY().X, p.GetXY().Y);
                    getEnemy();
                    // daca razele se intersecteaza e coliziune
                    if (((r1 + r2) * (r1 + r2)) >= d)
                        ok = true;
                }
            }
            return ok;
        }

        //ecuatia pentru masurarea distantei dintre 2 puncte
        public static float distEuclid(float x1, float y1, float x2, float y2)
        {
            return (x2-x1)*(x2-x1) + (y2- y1)*(y2-y1);
        }
        
        public static void RefreshGame()
        {
            ok = false;
            d = 0;
            myDots.Clear();
            myP.Clear();
            enemyPoint.Clear();
            g.Clear(Color.White);
        }
    }
}
