using System;

namespace ConsoleApp8
{
    class PierwszyWyjatek : System.Exception
    {
        public PierwszyWyjatek() : base() { }
        public PierwszyWyjatek(string message) : base(message) { }
        public PierwszyWyjatek(string message, System.Exception inner) : base(message, inner) { }
    }
    class DrugiWyjatek : System.Exception
    {
        public DrugiWyjatek() : base() { }
        public DrugiWyjatek(string message) : base(message) { }
        public DrugiWyjatek(string message, System.Exception inner) : base(message, inner) { }
    }
    class TrzeciWyjatek : System.Exception
    {
        public TrzeciWyjatek() : base() { }
        public TrzeciWyjatek(string message) : base(message) { }
        public TrzeciWyjatek(string message, System.Exception inner) : base(message, inner) { }
    }

    class Program
    {

        static void LosujWyjatek()
        {
            try
            {
                Random rand = new Random();
                int losowa = rand.Next(3);

                if (losowa == 0) throw new PierwszyWyjatek();
                if (losowa == 1) throw new DrugiWyjatek();
                if (losowa == 2) throw new TrzeciWyjatek();
            }
            catch (PierwszyWyjatek e1)
            {
                Console.WriteLine(e1.GetType() + e1.StackTrace);
            }
            catch (DrugiWyjatek e2)
            {
                Console.WriteLine(e2.GetType() + e2.StackTrace);
            }
            catch (TrzeciWyjatek e3)
            {
                Console.WriteLine(e3.GetType() + e3.StackTrace);
            }
        }

        static void WyswietlDlugosc(string napis)
        {
            try
            {
                try
                {
                    Console.WriteLine(napis.Length);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.StackTrace);
                    throw new Exception(e.Message, e.InnerException);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }
        static void Main(string[] args)
        {
            WyswietlDlugosc(null);
            LosujWyjatek();


            SomeClass someClassObj = new SomeClass();
            try
            {
                someClassObj.CanThrowException();
                someClassObj.CanThrowException();
                someClassObj.CanThrowException();
                someClassObj.CanThrowException();
                someClassObj.CanThrowException();
            }
            catch (Exception e)
            {
                string wyjatek = e.StackTrace;
                Console.WriteLine(wyjatek);
                Console.Write(wyjatek[wyjatek.Length - 2]);
                Console.Write(wyjatek[wyjatek.Length - 1]);
            }


            DoSkopiowania PierwszyObiekt = new DoSkopiowania("tak",null);
            DoSkopiowania DrugiObiekt = new DoSkopiowania();

            DoSkopiowania.Kopiarka(PierwszyObiekt, DrugiObiekt);

            Console.WriteLine();
            Console.WriteLine(DrugiObiekt.ToString());

            DoSkopiowania TrzeciObiekt = new DoSkopiowania("tak", "ok");
            DoSkopiowania CzwartyObiekt = new DoSkopiowania();
            CzwartyObiekt = (DoSkopiowania) TrzeciObiekt.Clone();

            Console.WriteLine();
            Console.WriteLine(CzwartyObiekt.ToString());
        }
    }
}

public class SomeClass
{ 
    public void CanThrowException() 
    { 
        if (new Random().Next(5) == 0) throw new Exception(); 
    } 
}

class DoSkopiowania : ICloneable 
{
    string pierwsza;
    string druga;


    public DoSkopiowania() {}
    public DoSkopiowania(string pierwsza, string druga)
    {
        this.pierwsza = pierwsza;
        this.druga = druga;
    }

    public static void Kopiarka(DoSkopiowania kopiowana, DoSkopiowania cel)
    {
        try
        {
            if(kopiowana.pierwsza == null || kopiowana.druga == null) throw new ArgumentNullException();
            cel.pierwsza = kopiowana.pierwsza;
            cel.druga = kopiowana.druga;
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine();
            Console.WriteLine(e.StackTrace);

        }
    }

    public object Clone()
    {
        try
        {
            var clone = (DoSkopiowania)this.MemberwiseClone();
            //HandleCloned(clone);
            if (clone.pierwsza == null || clone.druga == null) throw new ArgumentNullException();
            return clone;
            
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine();
            Console.WriteLine(e.StackTrace);

        }
        return -1;
    }

    public override string ToString()
    {
        return (pierwsza+" "+druga);
    }
}
