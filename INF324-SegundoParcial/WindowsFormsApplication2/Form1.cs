using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging; //adicionamos esto

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mostrar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(imagen.jpg)|*.jpg|*.jpeg|*.png";
            openFileDialog1.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Color c = new Color();
            int sR, sG, sB;
            sR = 0; sG = 0; sB = 0;

            for (int i = e.X; i < e.X + 10; i++)
                for (int j = e.Y; j < e.Y + 10; j++)
                {
                    c = bmp.GetPixel(i, j);
                    sR = sR + c.R;
                    sG = sG + c.G;
                    sB = sB + c.B;
                }
            sR = sR / 100;
            sG = sG / 100;
            sB = sB / 100;

            //textBox1.Text = c.R.ToString();
            //textBox2.Text = c.G.ToString();
            //textBox3.Text = c.B.ToString();

            textBox1.Text = sR.ToString();
            textBox2.Text = sG.ToString();
            textBox3.Text = sB.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            int t1 = Convert.ToInt32(textBox1.Text);
            int t2 = Convert.ToInt32(textBox2.Text);
            int t3 = Convert.ToInt32(textBox3.Text);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    if ( t1 == c.R && t2 == c.G && t3 == c.B)
                    //if (((221 <= c.R) && (c.R <= 255)) && ((50 <= c.G) && (c.G <= 132)) && ((0 <= c.B) && (c.B <= 18)))
                    {
                        bmp2.SetPixel(i, j, Color.Black);
                        //bmp2.SetPixel(i, j, Color.FromArgb(c.B, 0, 0));
                    }
                    else
                        bmp2.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));
                }
            }
            pictureBox2.Image = bmp2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OdbcConnection con = new OdbcConnection();
            OdbcCommand cmd = new OdbcCommand();
            con.ConnectionString = "DSN=prueba";

            cmd.CommandText = "INSERT INTO texturas (descripcion, cR, cG, cB) ";
            cmd.CommandText = cmd.CommandText + "VALUES('"+textBox4.Text+"',"+textBox1.Text+","+textBox2.Text+","+textBox3.Text+")";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            mostrar();
        }

        private void mostrar() {
            OdbcConnection con = new OdbcConnection();
            OdbcDataAdapter ada = new OdbcDataAdapter();
            con.ConnectionString = "DSN=prueba";
            ada.SelectCommand = new OdbcCommand();
            ada.SelectCommand.Connection = con;
            ada.SelectCommand.CommandText = "SELECT * FROM texturas";
            DataSet ds = new DataSet();
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                String des = selectedRow.Cells["descripcion"].Value.ToString();
                DialogResult result = MessageBox.Show("¿Està seguro de que quiere cambiar la textura a "+des+" ?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int r = Convert.ToInt32(selectedRow.Cells["cR"].Value);
                    int g = Convert.ToInt32(selectedRow.Cells["cG"].Value);
                    int b = Convert.ToInt32(selectedRow.Cells["cB"].Value);
                    Bitmap bmp = new Bitmap(pictureBox1.Image);
                    Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
                    Color c = new Color();
                    int sR, sG, sB;
                    int t1 = Convert.ToInt32(textBox1.Text);
                    int t2 = Convert.ToInt32(textBox2.Text);
                    int t3 = Convert.ToInt32(textBox3.Text);

                    for (int i = 0; i < bmp.Width - 10; i = i + 10)
                        for (int j = 0; j < bmp.Height - 10; j = j + 10)
                        {
                            sR = 0; sG = 0; sB = 0;
                            for (int ip = i; ip < i + 10; ip++)
                                for (int jp = j; jp < j + 10; jp++)
                                {
                                    c = bmp.GetPixel(ip, jp);
                                    sR = sR + c.R;
                                    sG = sG + c.G;
                                    sB = sB + c.B;
                                }
                            sR = sR / 100;
                            sG = sG / 100;
                            sB = sB / 100;

                            if (((t1 - 10 <= sR) && (sR <= t1 + 10)) && ((t2 - 10 <= sG) && (sG <= t2 + 10)) && ((t3 - 10 <= sB) && (sB <= t3 + 10)))
                            {
                                for (int ip = i; ip < i + 10; ip++)
                                    for (int jp = j; jp < j + 10; jp++)
                                    {
                                        bmp2.SetPixel(ip, jp, Color.FromArgb(r, g, b));
                                    }
                            }
                            else
                            {
                                for (int ip = i; ip < i + 10; ip++)
                                    for (int jp = j; jp < j + 10; jp++)
                                    {
                                        c = bmp.GetPixel(ip, jp);
                                        bmp2.SetPixel(ip, jp, Color.FromArgb(c.R, c.G, c.B));
                                    }
                            }
                        }
                    pictureBox2.Image = bmp2;
                }
                else
                {
                    MessageBox.Show("No se ha modificado la textura");
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "imagen(*.jpg)|*.jpg|imagen(*.png)|*.png|imagen(*.jpeg)|*.jpeg";
            openFileDialog1.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }

    }
}
