using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Video2scr
{
    public partial class Form1 : Form
    {
        Video2 v2 = new Video2();
        Black bl = new Black();
        bool fscr = true;
        bool porp = true;
        double tt1 = 0;
        TimeSpan t;
        //double volume = 50;
        bool v100 = false;





        public Form1()
        {
            InitializeComponent();
            v2.axVLCPlugin21.video.logo.file("logo.png");
            v2.axVLCPlugin21.video.logo.position = "bottom-right";
            v2.axVLCPlugin21.video.logo.disable();
            v2.axVLCPlugin21.volume = 50;
            listBox1.Items.Clear();
            bl.Show();
            ZaciemnianieStart();
            bl.Opacity = 0;
            bl.ShowInTaskbar = false;
            v2.ShowInTaskbar = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool exx = false;
            try
            {
                if (checkBox4.Checked == true)
                {
                    Screen[] screens = Screen.AllScreens;
                    Rectangle bounds = screens[1].Bounds;
                    v2.SetBounds(bounds.X, bounds.Y, v2.Width, v2.Height);
                    v2.axVLCPlugin21.video.fullscreen = true;
                }
            }
            catch
            {
                MessageBox.Show("Nie wykryto drugiego ekranu");
                exx = true;
            }

            try
            {
                if (exx == false)
                {
                    v2.Show();
                }

            }
            catch
            {
                MessageBox.Show("Okno odtwarzacza zostało nagle zamknięte - Uruchom ponownie program");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog vv = new OpenFileDialog();
                vv.ShowDialog();
                v2.axVLCPlugin21.playlist.add("file:///" + vv.FileName);
                listBox1.Items.Add(vv.SafeFileName);
            }
            catch
            {
                MessageBox.Show("Nieobsługiwany Format");

            }


        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                string dd = textBox3.Text;
                v2.axVLCPlugin21.playlist.add(dd);
                listBox1.Items.Add(dd);
            }
            catch
            {
                MessageBox.Show("Zły format wejściowy ");

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (porp == true)
            {
                timer1.Stop();
                button3.Text = "Wznów";
                v2.axVLCPlugin21.playlist.togglePause();

                porp = !porp;
            }
            else
            {
                timer1.Start();
                button3.Text = "Pauza";
                v2.axVLCPlugin21.playlist.togglePause();

                porp = !porp;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            v2.axVLCPlugin21.video.fullscreen = fscr;
            fscr = !fscr;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //label2.Text = v2.axVLCPlugin21.input.time.ToString(); To było by łatwiej napisać, ale za późno się skapłem, że to istnieje xD
            try
            {
                tt1 = Math.Round((v2.axVLCPlugin21.input.length / 100 * v2.axVLCPlugin21.input.position) / 10, 1);

                t = TimeSpan.FromSeconds(tt1);
                if (v2.axVLCPlugin21.playlist.isPlaying == true)
                {
                    if (checkBox1.Checked == true)
                    {
                        if (int.Parse(textBox2.Text) == (t.Hours * 3600 + t.Minutes * 60 + t.Seconds))
                        {
                            timer1.Stop();
                            v2.axVLCPlugin21.playlist.stop();
                        }
                    }
                }
                else
                {

                    timer1.Stop();
                }
            }
            catch
            {

            }
            try
            {
                // label2.Text = Math.Round(v2.axVLCPlugin21.input.length / 100 * v2.axVLCPlugin21.input.position, 1, MidpointRounding.AwayFromZero).ToString();
                /*timeM = (v2.axVLCPlugin21.input.length / 100 * v2.axVLCPlugin21.input.position);*/
                if (t.Hours == 0)
                {
                    label2.Text = t.Minutes.ToString() + ":" + t.Seconds.ToString();
                }
                else
                {
                    label2.Text = t.Hours.ToString() + "." + t.Minutes.ToString() + ":" + t.Seconds.ToString();
                }

            }
            catch
            {
                label2.Text = "xD";
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                label8.Text = v2.axVLCPlugin21.mediaDescription.title;
                button3.Text = "Pauza";

                if (listBox1.SelectedIndex != -1)
                {
                    v2.axVLCPlugin21.playlist.playItem(listBox1.SelectedIndex);
                    if (checkBox1.Checked)
                    {
                        v2.axVLCPlugin21.input.time = int.Parse(textBox1.Text) * 1000;
                    }
                    timer1.Start();
                }
            }
            catch
            {
                MessageBox.Show("Nie zapnomniałeś czegoś?");
            }
        }



        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            v2.axVLCPlugin21.video.logo.enable();
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false)
            {
                v2.axVLCPlugin21.video.logo.disable();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (v100 == false)
            {
                v2.axVLCPlugin21.volume = trackBar1.Value;
                label7.Text = v2.axVLCPlugin21.volume.ToString() + "%";
            }
            else
            {
                v2.axVLCPlugin21.volume = trackBar1.Value + 100;
                label7.Text = (v2.axVLCPlugin21.volume).ToString() + "%";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (v2.axVLCPlugin21.video.fullscreen == true)
                {
                    v2.axVLCPlugin21.video.fullscreen = false;
                }
                v2.Hide();
            }
            catch
            {
                MessageBox.Show("Piotrek znowu psujesz mój program, zrestartuj aplikacje aby kontynuować :)");
            }

        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                v100 = true;
                v2.axVLCPlugin21.volume += 100;
            }
            else
            {
                v100 = false;
                if (v2.axVLCPlugin21.volume > 100)
                {
                    v2.axVLCPlugin21.volume -= 100;
                }
            }
            trackBar1_Scroll(null, null);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                v2.axVLCPlugin21.input.position = ((double.Parse(textBox4.Text)) / (v2.axVLCPlugin21.input.length / 100)) * 10;
            }
            catch
            {
                MessageBox.Show("Zły format wejściowy");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            v2.axVLCPlugin21.input.position += (60 / (v2.axVLCPlugin21.input.length / 100)) * 10;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            v2.axVLCPlugin21.input.position += (10 / (v2.axVLCPlugin21.input.length / 100)) * 10;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            v2.axVLCPlugin21.input.position += (5 / (v2.axVLCPlugin21.input.length / 100)) * 10;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            v2.axVLCPlugin21.input.position += (1 / (v2.axVLCPlugin21.input.length / 100)) * 10;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                v2.axVLCPlugin21.playlist.items.remove(listBox1.SelectedIndex);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            v2.axVLCPlugin21.playlist.stop();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            v2.axVLCPlugin21.video.takeSnapshot();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            List<Label> d = new List<Label>();
            d.Add(label1);
            d.Add(label2);
            d.Add(label3);
            d.Add(label4);
            d.Add(label5);
            d.Add(label6);
            d.Add(label7);
            d.Add(label8);
            d.Add(label9);
            d.Add(label10);
            d.Add(label11);

            if (BackColor != Color.Black)
            {
                BackColor = Color.Black;

                foreach (Label a in d)
                {
                    a.ForeColor = Color.White;
                }
                checkBox1.ForeColor = Color.White;
                checkBox2.ForeColor = Color.White;
                checkBox3.ForeColor = Color.White;
                checkBox4.ForeColor = Color.White;


            }
            else
            {
                BackColor = DefaultBackColor;

                foreach (Label a in d)
                {
                    a.ForeColor = Color.Black;
                }
                checkBox1.ForeColor = Color.Black;
                checkBox2.ForeColor = Color.Black;
                checkBox3.ForeColor = Color.Black;
                checkBox4.ForeColor = Color.Black;
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            v2.axVLCPlugin21.input.position -= (5 / (v2.axVLCPlugin21.input.length / 100)) * 10;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            v2.axVLCPlugin21.input.position -= (60 / (v2.axVLCPlugin21.input.length / 100)) * 10;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button19_Click(object sender, EventArgs e)
        {

            try
            {
                if (bl.Opacity != 0)
                {
                    if(timer1.Enabled ==false)
                    {button6_Click(sender,e);}
                    
                    timer2.Interval = 30;
                    timer2.Start();
                }
            }
            catch
            {
                MessageBox.Show("Błędzik. POZDRO 33");
            }

            if(porp==true)
            {
                timer1.Start();
                button3.Text = "Pauza";
                v2.axVLCPlugin21.playlist.togglePause();

                porp = !porp;
            }

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            bl.TopMost = true;
            bl.Opacity = trackBar2.Value * 0.01; //cholerne double xd
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {

                bl.TopMost = true;
                if (checkBox5.Checked && v2.axVLCPlugin21.audio.volume < 50)
                {
                    v2.axVLCPlugin21.audio.volume += 1;
                }
                trackBar2.Value = int.Parse(Math.Round(bl.Opacity * 100).ToString());
                timer3.Stop();
                if (bl.Opacity > 0)
                {
                    bl.Opacity -= 0.01;
                }
                else
                {
                    if (v2.axVLCPlugin21.audio.volume > 50)
                    {

                    }
                    else
                        v2.axVLCPlugin21.audio.volume = 50;

                   
                    timer2.Stop();
                }
                if (v2.axVLCPlugin21.audio.volume > 0)
                {
                    trackBar1.Value = v2.axVLCPlugin21.audio.volume;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błędzik. POZDRO 1");
            }
        }

        public void ZaciemnianieStart()
        {
            try
            {
                Screen[] screens = Screen.AllScreens;
                Rectangle bounds = screens[1].Bounds;
                Rectangle resolution = screens[1].Bounds;
                bl.Width = resolution.Width;
                bl.Height = resolution.Height;
                bl.TopMost = true;
                bl.SetBounds(bounds.X, bounds.Y, bl.Width, bl.Height);
            }
            catch
            {
                MessageBox.Show("Uwaga przyciemnianie może nie działać");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                timer2.Stop();
                if (bl.Opacity != 1)
                {

                    timer3.Interval = 30;
                    timer3.Start();
                }

            }
            catch
            {
                MessageBox.Show("Błędzik. POZDRO 2");
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {

                bl.TopMost = true;
                if (checkBox5.Checked)
                {
                    v2.axVLCPlugin21.audio.volume -= 1;
                }
                trackBar2.Value = int.Parse(Math.Round(bl.Opacity * 100).ToString());
                if (bl.Opacity < 1)
                {
                    bl.Opacity += 0.01;
                }
                else
                {
                    if (porp == true)
                    {
                        timer1.Stop();
                        button3.Text = "Wznów";
                        v2.axVLCPlugin21.playlist.togglePause();

                        porp = !porp;
                    }
                    v2.axVLCPlugin21.audio.volume = 0;
                    timer3.Stop();
                }
                trackBar1.Value = v2.axVLCPlugin21.audio.volume;
            }
            catch
            {
                MessageBox.Show("Błędzik. POZDRO 3");
            }
        }
    }
}
