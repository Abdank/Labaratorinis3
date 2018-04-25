using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;
/// <summary>
/// Summary description for Studentas
/// </summary>
public class Studentas : IComparable<Studentas>, IEquatable<Studentas>
{
    /// <summary>
    /// konstruktorius
    /// </summary>
    /// <param name="vardas"> vardas ir pavardė</param>
    /// <param name="numeris"> telefono numeris</param>
    /// <param name="group"> grupes pavadinimas</param>
    /// <param name="gradecount"> pažymių kiekis</param>
    /// <param name="grades"> pažymių masyvas</param>
    /// <param name="reikalavimas"> minimalus pažimys stipendijai</param>
    public Studentas(string vardas, string numeris, string grupe, int pazymiuKiekis, int[] pazymiai, double reikalavimas)
    {
        Vardas = vardas;
        Numeris = numeris;
        Grupe = grupe;
        PazymiuKiekis = pazymiuKiekis;
        int[] tuscias = new int[PazymiuKiekis];
        Pazymiai = tuscias;
        Pazymiai = pazymiai;
        ArPirmunas = ArYraPirmunas();
        ArSkola = ArSkolingas();
        ArGausStipendija(reikalavimas);

    }
    /// <summary>
    /// Savybės
    /// </summary>
    public string Vardas { get; set; }
    public string Numeris { get; set; }
    public string Grupe { get; set; }
    public int PazymiuKiekis { get; set; }
    public int[] Pazymiai { get; set; }
    /// <summary>
    /// ar pirmūnas
    /// </summary>
    public bool ArPirmunas { get; set; }
    /// <summary>
    /// ar turi skolų
    /// </summary>
    public bool ArSkola { get; set; }
    /// <summary>
    /// gaunamos stipendijos dydis
    /// </summary>
    public double Stipendija { get; set; }
    /// <summary>
    /// ar gauna stipendija
    /// </summary>
    public bool ArStipendija { get; set; }
    /// <summary>
    /// Pažymių vidurkis
    /// </summary>
    public double Vidurkis { get; set; }
    /// <summary>
    /// Suranda ar studentas gaus stipendija
    /// </summary>
    /// <param name="reikalavimas"> minimalus pažimys stipendijai</param>
    /// <returns></returns>
    public bool ArGausStipendija(double reikalavimas)
    {
        double vidurkis = 0;
        for (int i = 0; i < PazymiuKiekis; i++)
            vidurkis = vidurkis + Pazymiai[i];
        vidurkis = vidurkis / (PazymiuKiekis);
        Vidurkis = Math.Round(vidurkis, 2);
        if (vidurkis > reikalavimas)
        {
            ArStipendija = true;
            return true;
        }
        else
        {
            ArStipendija = false;
            return false;
        }
    }
    /// <summary>
    /// Suranda ar pirmūnas
    /// </summary>
    /// <returns></returns>
    private bool ArYraPirmunas()
    {
        bool top = true;
        for (int i = 0; i < PazymiuKiekis; i++)
        {
            if (Pazymiai[i] < 9)
                top = false;
        }
        return top;
    }
    /// <summary>
    /// patikrina ar studentas turi skolų
    /// </summary>
    /// <returns></returns>
    private bool ArSkolingas()
    {
        for (int i = 0; i < PazymiuKiekis; i++)
        {
            if (Pazymiai[i] < 5)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// tikrina pagal stipendijos dydi ir vardą pavardė abėcėlės tvarka
    /// </summary>
    /// <param name="pirmas"> </param>
    /// <param name="antras"></param>
    /// <returns></returns>
    static public bool operator >(Studentas pirmas, Studentas antras)
    {
        return pirmas.CompareTo(antras) == 1;
    }
    static public bool operator <(Studentas pirmas,
    Studentas antras)
    {
        return pirmas.CompareTo(antras) == -1;
    }
    public override string ToString()
    {
        string eilute = string.Format("{0,-30};{1,-12};{2,-15};{3,-10};{4, 20}", Vardas, Numeris, Grupe, Vidurkis, Stipendija);
        return eilute;
    }
    /// <summary>
    /// Suskaičiuoja kokią stipendiją gaus studentas
    /// </summary>
    /// <param name="PinigaiTaskui"></param>
    public void StipendijosDydis(double PinigaiTaskui)
    {
        int taskai = 0;
        if (ArStipendija)
        {
            taskai = 10;
            if (ArPirmunas)
                taskai = 11;
        }
        Stipendija = Math.Floor(PinigaiTaskui * taskai * 100) / 100;
    }
    public int CompareTo(Studentas studentas)
    {
        if (studentas == null)
            return 1;
        if (studentas.Stipendija > Stipendija)
            return 1;
        else if (Stipendija > studentas.Stipendija)
            return -1;
        else
        {
            return String.Compare(Vardas, studentas.Vardas, StringComparison.CurrentCulture);
        }
    }
    public bool Equals(Studentas studentas)
    {
        if (studentas == null)
            return false;
        if (this.Vardas == studentas.Vardas)
            return true;
        return false;
         
    }

}