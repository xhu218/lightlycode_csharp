using System;

class Program
{
    static void Main(string[] args)
    {
        int a = 0,b=0,c=0;
        Console.WriteLine(String.Format("a = {0},b = {1} ,c={2}",a,b,c));
        
        testa test = new testa();
        test.setname("wfg");
        test.show();

        Console.WriteLine("Hello World");
    }
}