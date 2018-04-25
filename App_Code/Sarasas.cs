using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentasSarasas
/// </summary>
public class Sarasas<tipas> : IEnumerable<tipas> where tipas : IComparable<tipas>, IEquatable<tipas>
{
    /// <summary>
    /// vieno elemento klasė
    /// </summary>
    private sealed class Mazgas<tipas>
    {
        public tipas Duom { get; set; }
        public Mazgas<tipas> Kitas { get; set; }
        public Mazgas<tipas> Buves { get; set; }
        public Mazgas(tipas a, Mazgas<tipas> kitas, Mazgas<tipas> buves)
        {
            Duom = a;
            Kitas = kitas;
            Buves = buves;
        }
    }
    private Mazgas<tipas> pr; // sąrašo pradžią
    private Mazgas<tipas> pb; // sąrašo pabaiga
    private Mazgas<tipas> d; // sąrašo sąsaja
    public double Fondas { get; private set; } // Stipendijos fondas
    public double Reikalavimai { get; private set; }  // Minimalus Pažimys stipendijai

    /// <summary>
    /// Įveda į studentų sąrašą // Stipendijos fondą ir Minimalų Pažimį stipendijai
    /// </summary>
    /// <param name="fondas"> Stipendijos fondas</param>
    /// <param name="reikalavimai"> Minimalus Pažimys stipendijai</param>
    public void PirmaEilute(double fondas, double reikalavimai)
    {
        Fondas = fondas;
        Reikalavimai = reikalavimai;
    }
    /// <summary>
    /// konstruktorius
    /// </summary>
    public Sarasas()
    {
        this.pr = null;
        this.pb = null;
        this.d = null;
    }
    public void DetiDuomenisT(tipas naujas)
    {
        var dd = new Mazgas<tipas>(naujas, null, pb);
        if (pr != null)
        {
            pb.Kitas = dd;
            pb = dd;
        }
        else
        {
            pr = dd;
            pb = dd;
        }

    }
    /** Sąsajai priskiriama sąrašo pradžia */
    public void Pradžia()
    { d = pr; }
    /** Sąsajai priskiriamas tolesnis sąrašo elementas */
    public void Kitas()
    { d = d.Kitas; }
    /** Grąžina true, jeigu sąrašas netuščias */
    public bool Yra()
    { return d != null; }
    /** Grąžina sąrašo sąsajos elemento reikšmę */
    public tipas ImtiDuomenis()
    { return d.Duom; }
    /// <summary>
    /// rikiavimas pagal stipendijos dydį ir vardą pavardę
    /// </summary>
    public void Rikiuoti()
    {
        for (Mazgas<Studentas> d1 = pr as Mazgas<Studentas>; d1 != null; d1 = d1.Kitas)
        {
            Mazgas<Studentas> minv = d1 as Mazgas<Studentas>;
            for (Mazgas<Studentas> d2 = d1.Kitas; d2 != null; d2 = d2.Kitas)
                if (d2.Duom < minv.Duom)
                    minv = d2;
                    // Informacinių dalių sukeitimas vietomis
                    Studentas St = d1.Duom;
                    d1.Duom = minv.Duom;
                    minv.Duom = St;
        }
    }
    /// <summary>
    /// Šalina studentus kurie negaus stipendijos
    /// </summary>
    public void SalintiStudentus()
    {
        for (Mazgas<Studentas> d1 = pr as Mazgas<Studentas>; d1 != null; /*d1 = d1.Kitas*/)
        {
            d1.Duom.StipendijosDydis(PinigaiTaskui);
            if (d1.Kitas != null)
                if (d1.Kitas.Duom.ArSkola || !d1.Kitas.Duom.ArStipendija)
                {
                    d1.Kitas = d1.Kitas.Kitas;
                }
                else
                    d1 = d1.Kitas;
            else
                d1 = d1.Kitas;
        }
        //if (pr != null)
        //    if (pr.Duom.ArSkola || !pr.Duom.ArStipendija)
        //        pr = pr.Kitas;
    }

    public double PinigaiTaskui { get; private set; } // 10% stipendijos
    /// <summary>
    /// Suskaičiuoja kiek yra 10% stipendijos
    /// </summary>
    public void SkaiciuotiTaskoVerte(int Taskai)
    {
        PinigaiTaskui = Fondas / Taskai;
    }
    public IEnumerator<tipas> GetEnumerator()
    {
        for (Mazgas<tipas> dd = pr; dd != null; dd = dd.Kitas)
        {
            yield return dd.Duom;
        }
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}