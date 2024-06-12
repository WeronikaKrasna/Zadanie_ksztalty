using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;


namespace Zadanie_ksztalty
{
    public partial class Form1 : Form
    {
        List<kszta速y> figury = new List<kszta速y>();
        int ilosc_kol;
        int ilosc_prostokat;
        Graphics g;
        ManualResetEvent evt = new ManualResetEvent(true);


        public Form1()
        {
            ilosc_kol = 100;
          ilosc_prostokat = 100;

            for (int i = 0; i < ilosc_kol; i++)
            {
                kszta速y k = new ko這();
                figury.Add(k);

            }

            for (int i = ilosc_kol; i < ilosc_prostokat + ilosc_kol; i++)
            {
                kszta速y k = new Prostok靖();
                figury.Add(k);

            }



            InitializeComponent();

            List<Thread>watki = new List<Thread>();
            for(int i=0;i<figury.Count;i++)
            {
               Thread thread = new Thread(() => Updatefigur());
                watki.Add(thread);
                watki[i].Start();
            }
            
            

            timer1.Start();
        }

        void Updatefigur()
        {
          while (true)
            {
                evt.WaitOne();
                Thread.Sleep(100);
                int i = 0;
                kszta速y[] k = new kszta速y[100];
                foreach(kszta速y x in figury)
                {
                   if(x.x <= pictureBox1.Width-10 || x.y<= pictureBox1.Height-10)
                    {
                        k[i] = x;
                        i++;
                    }
                }
                //zrob zeby kilka kulek sie zatrzymalo w miejscu kliknieca albo dzies obok
                foreach(kszta速y p in k)
                {
                    lock (p)
                    {
                        p.Update(pictureBox1.Width, pictureBox1.Height);
                    }
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            for (int i = 0; i < ilosc_prostokat; i++)
            {
                figury[i].Rysuj(g);

            }
            for (int i = ilosc_kol; i < ilosc_kol + ilosc_prostokat; i++)
            {
                figury[i].Rysuj(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
         /*  for (int i = 0; i < figury.Count; i++)
            {

                if (figury[i].x < 0)
                {
                    figury[i].Vx = -figury[i].Vx;
                }

                if (figury[i].x + figury[i].dlugosc > pictureBox1.Width)
                {

                    figury[i].Vx = -figury[i].Vx;
                }

                figury[i].x += figury[i].Vx;


                if (figury[i].y < 0)
                {
                    figury[i].Vy = -figury[i].Vy;

                }

                if (figury[i].y + figury[i].szerokosc > pictureBox1.Height)
                {

                    figury[i].Vy = -figury[i].Vy;
                }

                figury[i].y += figury[i].Vy;
          
            */
                Refresh();
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            if (evt.WaitOne(0))
                evt.Reset();
            else
                evt.Set();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //tworzenie okan dialogowego
            DialogResult res = saveFileDialog1.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                //   string f = saveFileDialog1.FileName;
                //  MessageBox.Show(f);  //wyrzuca na ekran 


                /* Stream file = new FileStream("test", FileMode.CreateNew);
                 IFormatter fr = new BinaryFormatter();
                 fr.Serialize(file, figury);
                 file.Close();
                */
                // figury=(List<kszta速y>)fr.Deserialize(file);


                //JSON
                //json zapisuje tylko wlasnosci, zapisuje tylko get set

                /*  StreamWriter f = new StreamWriter("test_json_pelny");
                    string xx = JsonSerializer.Serialize(figury);
                    f.Write(xx);
                    f.Close();
                 */
               

              //  string jsonString = File.ReadAllText("test_json_pelny");
              //  figury = JsonSerializer.Deserialize<List<kszta速y>>(jsonString);

                //XML

               /* XmlSerializer serializer = new XmlSerializer(typeof(List<kszta速y>));
               
                using (StreamWriter writer = new StreamWriter("xmll"))
                {   
                    serializer.Serialize(writer, figury);
                }
               */
              
               /* using (StreamReader reader = new StreamReader("xmll"))
                {
                    figury = (List<kszta速y>)serializer.Deserialize(reader)!;
                }
               */
                
                SaveLoad zapis = new SaveLoad();
                StreamWriter sm = new StreamWriter("zapis_saveload.txt");
                foreach( kszta速y k in figury)
                {
                    zapis.Save(sm, k);
                }

               sm.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            
        }
    }
}
