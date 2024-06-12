using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

[Serializable]
[XmlInclude(typeof(koło))]
[XmlInclude(typeof(Prostokąt))]
public abstract class kształty
{
    [Description("Pozycja x")]
    public int x {  get; set; }
    [Description("Pozycja y")]
    public int y { get; set; }
    [Description("Kolor")]
    public Color color { get; set; }
    [Description("Szerokosc")]
    public int szerokosc { get; set; }
    [Description("Dlygosc")]
    public int dlugosc { get; set; }
    [Description("Predkosc x")]
    public int Vx { get; set; }
    [Description("Predkosc y")]
    public int Vy { get; set; }


   [JsonConstructor]
    public kształty() { }


    public abstract void Rysuj(Graphics g);
    public abstract void Update(int BorderX, int BorderY);

    //potrzebne do json; inaczej jest pusty plik
  /*  [XmlElement("x")]
    public int X { get { return x; } }
    [XmlElement("y")]
    public int Y { get { return y; } }
    [XmlElement("width")]
    public int width { get { return szerokosc; } }
    [XmlElement("height")]
    public int height { get { return dlugosc; } }
    [XmlElement("vx")]
    public int VX { get { return Vx; } }
    [XmlElement("vy")]
    public int VY { get { return Vy; } }
    [XmlElement("color")]
    public Color kolor { get { return color; } }
  */
    

}
