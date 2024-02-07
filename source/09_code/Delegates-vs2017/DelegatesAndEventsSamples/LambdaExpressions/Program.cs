﻿using System;
using System.Collections.Generic;
using static System.Console;

namespace Wrox.ProCSharp.Delegates
{
  class Program
  {
    static void Main()
    {
      // SimpleDemos();

      // Closure1();
      ClosureForEach();
    }


    static void SimpleDemos()
    {
      Func<string, string> oneParam = s => $"change uppercase {s.ToUpper()}";
      WriteLine(oneParam("test"));

      Func<double, double, double> twoParams = (x, y) => x * y;
      WriteLine(twoParams(3, 2));

      Func<double, double, double> twoParamsWithTypes = (double x, double y) => x * y;
      WriteLine(twoParamsWithTypes(4, 2));

      Func<double, double> operations = x => x * 2;
      operations += x => x * x;

      ProcessAndDisplayNumber(operations, 2.0);
      ProcessAndDisplayNumber(operations, 7.94);
      ProcessAndDisplayNumber(operations, 1.414);
      WriteLine();
    }

    static void ProcessAndDisplayNumber(Func<double, double> action, double value)
    {
      double result = action(value);
      WriteLine($"Value is {value}, result of operation is {result}");

    }

    static void Closure1()
    {
      int someVal = 5;
      Func<int, int> f = x => x + someVal;

      someVal = 7;

      Console.WriteLine(f(3));
    }


    static void ClosureForEach()
    {
      var values = new List<int>() { 10, 20, 30 };
      var funcs = new List<Func<int>>();
      foreach (var val in values)
      {
        funcs.Add(() => val);
      }
      foreach (var f in funcs)
      {
        WriteLine((f()));
      }

    }
  }
}
