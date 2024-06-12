using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

[Serializable]

public class koło : kształty
{
   [JsonConstructor]
    public koło()
    {


        Random random = new Random();

        x = random.Next(0, 700);
        y = random.Next(0, 300);

        szerokosc = random.Next(0, 50); 
        dlugosc = szerokosc;
        color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));

        Vx = random.Next(-50, 50);
        Vy = random.Next(-50,50);

    }

    public override void Rysuj(Graphics g)
    {
        Pen p = new Pen(color);

        g.DrawEllipse(p, x, y, szerokosc, dlugosc);
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

