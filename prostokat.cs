using System;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;

[Serializable]
//[JsonExtensionData]
public class Prostokąt : kształty
{
    [JsonConstructor]
    public Prostokąt()
    {

        Random random = new Random();

        x = random.Next(0, 700);
        y = random.Next(0, 300);

        szerokosc = random.Next(0, 100);
        dlugosc = random.Next(0, 125);

        color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));

        Vx = random.Next(-50, 50);
        Vy = random.Next(-50, 50);
    }
    //serializacja i deserializacja dla json i xml
    //deserializacja ----> wczytanie z pliku i pokazanie za dziala
    //get; set potrzeben do json
    /* public int X { get { return x; } }
     public int Y { get { return y; } }
     public int width { get { return szerokosc; } }
     public int height { get { return dlugosc; } }
     public int VX { get { return Vx; } }
     public int VY { get { return Vy; } }
     public Color kolor { get { return color; } }
    */
    public override void Rysuj(Graphics g)
    {
        Pen p = new Pen(color);

        g.DrawLine(p, x, y, x + dlugosc, y);
        g.DrawLine(p, x, y, x, y + szerokosc);
        g.DrawLine(p, x, y + szerokosc, x + dlugosc, y + szerokosc);
        g.DrawLine(p, x + dlugosc, y, x + dlugosc, y + szerokosc);

    }

    public override void Update(int BorderX, int BorderY)
    {
        {

            if (x < 0)
            {
                Vx = -Vx;
            }

            if (x + dlugosc > BorderX)
            {

                Vx = -Vx;
            }

            x += Vx;


            if (y < 0)
            {
                Vy = -Vy;
            }

            if (y + szerokosc > BorderY)
            {

                Vy = -Vy;
            }

            y += Vy;
        }
    }
}
