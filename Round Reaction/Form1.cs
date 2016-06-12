using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Round_Reaction
{

//d^2 = (x2-x1)^2 + (y2-y1)^2
//  x1= centru player x2= centru Enemy d= diametrul
//if (r1+r2)^2 <= d^2 you have a collision.
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int timp = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            StartGame();
        }
        private void StartGame()
        {
            Engine.px = pb_game.Width; Engine.py = pb_game.Height;
            Engine.initDraw();
            Engine.initPoints();
            Engine.initDP();
            Engine.initPlayer();
            pb_game.Image = Engine.bmp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool ok = false;
            //gaseste pozitia curenta a cursorului
            PointF Location = this.PointToClient(Cursor.Position);
            txtX.Text = (Engine.mc.X - pb_game.Location.X).ToString(); txtY.Text = (Engine.mc.Y - pb_game.Location.Y).ToString();
            Engine.Move();
            Engine.g.Clear(Color.White);

            //daca locatia cursorului e in imagine jocul continua
            if (Location.X >= pb_game.Location.X && Location.Y >= pb_game.Location.Y && Location.X <= pb_game.Location.X + pb_game.Width && Location.Y <= pb_game.Location.Y + pb_game.Height)
            {
                Engine.mc.X = Location.X; Engine.mc.Y = Location.Y;
                Engine.initPlayerDraw();
            }
            else
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                Cursor.Show();
                MessageBox.Show(" Ai iesit din spatiul de joc.\n Game Over \n Ai rezistat "+ timp +" secunde. \n Felicitari :)","GAME OVER", MessageBoxButtons.OK, MessageBoxIcon.Information,MessageBoxDefaultButton.Button1);
                timp = 0; txtTime.Text = "";
                Engine.RefreshGame();
                StartGame();
                lblGame.Visible = true;
                lblGame2.Visible = true;
            }
            txtCount.Text = Engine.Collision().ToString();
           
            //redeseneaza punctele
            Engine.initDP(); 

            //scrie in lista cu coordonatele inamicilor
            listEnemy.Items.Clear();
            if (listEnemy.Enabled == true)
            {
                for (int i = 0; i < Engine.army; i++)
                {
                    int nr = i + 1;
                    listEnemy.Items.Add("Inamicul " + nr + " Coord X " + (int)Engine.enemyPoint[i].X + " Coord Y " + (int)Engine.enemyPoint[i].Y);
                }
            }
            else listEnemy.Visible = false;
            //foloseste functia de coliziune pentru a afla daca s-a intamplat una sau nu
            ok = Engine.Collision();
            if( ok == true)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                Cursor.Show();
                MessageBox.Show(" Inamicii te-au prins.\n Game Over\n Ai rezistat " + timp + " secunde. \n Felicitari :)", "GAME OVER", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                txtTime.Text = "";
                timp = 0;
                Engine.RefreshGame();
                StartGame();              
                lblGame.Visible = true;
                lblGame2.Visible = true;
            }
            //refresh la picture box
            pb_game.Image = Engine.bmp;
        }

            //if (timer1.Enabled)
            //{
            //    Cursor.Show();
            //    btn_start.Enabled = true;
            //    lblGame.Visible = true;
            //    lblGame2.Visible = true;
            //    btn_start.Visible = true;
            //    timer1.Enabled = false;
            //    timer2.Enabled = false;       
            //    timp = 0; txtTime.Text = "";
            //    btn_start.Text = "START";
            //}

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            timp = timp + 1;
            txtTime.Text = timp.ToString();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                if (!timer1.Enabled)             
                {
                    Cursor.Hide();
                    //btn_start.Enabled = false;
                    lblGame.Visible = false;
                    lblGame2.Visible = false;
                    timer1.Enabled = true;
                    timer2.Enabled = true;
                }
            }
        }
    }
}
