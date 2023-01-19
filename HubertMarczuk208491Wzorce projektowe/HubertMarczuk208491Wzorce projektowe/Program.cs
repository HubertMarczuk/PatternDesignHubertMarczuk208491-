using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubertMarczuk208491Wzorce_projektowe
{
    public abstract class Kampania
    {
        protected int kwota_wydana;
        protected int liczba_klientow;
        public Kampania(int kwota_wydana, int liczba_klientow)
        {
            this.kwota_wydana = kwota_wydana;
            this.liczba_klientow = liczba_klientow;
        }
        public int KwotaWydana
        {
            get
            {
                return kwota_wydana;
            }
        }
        public int LiczbaKlientów
        {
            get
            {
                return liczba_klientow;
            }
        }
        public abstract void Akceptuj(Visitor visitor);
    }
    public class Influencer : Kampania
    {
        string nazwa_blogu;
        public Influencer(int kwota_wydana, int liczba_klientow, string nazwa_blogu) : base(kwota_wydana, liczba_klientow)
        {
            this.nazwa_blogu = nazwa_blogu;
        }
        public string NazwaBlogu
        {
            get
            {
                return nazwa_blogu;
            }
        }
        public override void Akceptuj(Visitor visitor)
        {
            visitor.VisitInfluencer(this);
        }
    }
    public class Native : Kampania
    {
        int liczba_bilbordow;
        public Native(int kwota_wydana, int liczba_klientow, int liczba_bilbordow) : base(kwota_wydana, liczba_klientow)
        {
            this.liczba_bilbordow = liczba_bilbordow;
        }
        public int LiczbaBilbordow
        {
            get
            {
                return liczba_bilbordow;
            }
        }
        public override void Akceptuj(Visitor visitor)
        {
            visitor.VisitNative(this);
        }
    }
    public class Internet : Kampania
    {
        string nazwa_strony;
        public Internet(int kwota_wydana, int liczba_klientow, string nazwa_strony) : base(kwota_wydana, liczba_klientow)
        {
            this.nazwa_strony = nazwa_strony;
        }
        public override void Akceptuj(Visitor visitor)
        {
            visitor.VisitInternet(this);
        }
        public string NazwaStrony
        {
            get
            {
                return nazwa_strony;
            }
        }
    }
    public abstract class Visitor
    {
        public abstract void VisitInfluencer(Influencer influencer);
        public abstract void VisitNative(Native native);
        public abstract void VisitInternet(Internet internet);
    }
    public class RaportVisitor : Visitor
    {
        public override void VisitInfluencer(Influencer influencer)
        {
            Console.Write("Informacja o kampanii Influencer,\n" +
                          "wydano " + influencer.KwotaWydana + "PLN,\n" +
                          "pozyskano "+ influencer.LiczbaKlientów + " klientów,\n" +
                          "nazwa blogu " + influencer.NazwaBlogu + ".");
            Console.WriteLine();
        }

        public override void VisitInternet(Internet internet)
        {
            Console.Write("Informacja o kampanii Internet,\n" +
                          "wydano " + internet.KwotaWydana + "PLN,\n" +
                          "pozyskano "+ internet.KwotaWydana + " klientów,\n" +
                          "nazwa strony " + internet.NazwaStrony + ".");
            Console.WriteLine();
        }

        public override void VisitNative(Native native)
        {
            Console.Write("Informacja o kampanii Native,\n" +
                          "wydano " + native.KwotaWydana + "PLN,\n" +
                          "pozyskano " + native.LiczbaKlientów + " klientów,\n" +
                          "liczba bilbordów " + native.LiczbaBilbordow + ".");
            Console.WriteLine();
        }
    }
    public class ValidatorVisitor : Visitor
    {
        public override void VisitInfluencer(Influencer influencer)
        {
            if (influencer.KwotaWydana != 0 && influencer.LiczbaKlientów != 0 && influencer.NazwaBlogu != "")
            {
                Console.WriteLine("Dane kampanii są poprawne!");
            }
            else
            {
                Console.WriteLine("Dane kampanii są niepoprawne!");
            }
        }
        public override void VisitInternet(Internet internet)
        {
            if(internet.KwotaWydana != 0 && internet.LiczbaKlientów != 0 && internet.NazwaStrony != "")
            {
                Console.WriteLine("Dane kampanii są poprawne!");
            }
            else
            {
                Console.WriteLine("Dane kampanii są niepoprawne!");
            }
        }
        public override void VisitNative(Native native)
        {
            if (native.KwotaWydana != 0 && native.LiczbaKlientów != 0 && native.LiczbaBilbordow != 0)
            {
                Console.WriteLine("Dane kampanii są poprawne!");
            }
            else
            {
                Console.WriteLine("Dane kampanii są niepoprawne!");
            }
        }
    }
    public class Klient
    {
        public static void ClientCode()
        {
            Console.WriteLine();
            Console.WriteLine("Podaj rodzaj kampanii (Influencer/Native/Internet lub \"Q\" żeby zamknąć program): ");
            string nazwa = Console.ReadLine();
            int kwota_wydana;
            int liczba_klientow;
            RaportVisitor rv = new RaportVisitor();
            ValidatorVisitor vv = new ValidatorVisitor();
            if (nazwa == "Q")
            {
                Console.WriteLine("Program zostanie zamknięty! Naciśnij dowolny przycisk");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else if (nazwa == "Influencer")
            {
                Console.WriteLine("Podaj wydaną kwotę: ");
                kwota_wydana = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj liczbę klientów: ");
                liczba_klientow = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj nazwę blogu: ");
                string nazwa_blogu = Console.ReadLine();
                Influencer influencer = new Influencer(kwota_wydana, liczba_klientow, nazwa_blogu);
                Console.WriteLine();
                influencer.Akceptuj(rv);
                Console.WriteLine();
                influencer.Akceptuj(vv);
            }
            else if (nazwa == "Native")
            {
                Console.WriteLine("Podaj wydaną kwotę: ");
                kwota_wydana = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj liczbę klientów: ");
                liczba_klientow = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj liczbę bilbordów: ");
                int liczba_bilbordow = Convert.ToInt32(Console.ReadLine());
                Native native = new Native(kwota_wydana, liczba_klientow, liczba_bilbordow);
                Console.WriteLine();
                native.Akceptuj(rv);
                Console.WriteLine();
                native.Akceptuj(vv);

            }
            else if (nazwa == "Internet")
            {
                Console.WriteLine("Podaj wydaną kwotę: ");
                kwota_wydana = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj liczbę klientów: ");
                liczba_klientow = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj nazwę strony: ");
                string nazwa_strony = Console.ReadLine();
                Internet internet = new Internet(kwota_wydana, liczba_klientow, nazwa_strony);
                Console.WriteLine();
                internet.Akceptuj(rv);
                Console.WriteLine();
                internet.Akceptuj(vv);
            }
            else
            {
                Console.WriteLine("Niepoprawna nazwa Kampanii!");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Klient.ClientCode();
            }
        }
    }
}
