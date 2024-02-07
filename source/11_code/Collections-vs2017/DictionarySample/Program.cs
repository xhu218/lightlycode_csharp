﻿using System;
using System.Collections.Generic;
using static System.Console;

namespace Wrox.ProCSharp.Collections
{
    class Program
    {
        static void Main()
        {
            var idTony = new EmployeeId("C3755");
            var tony = new Employee(idTony, "Tony Stewart", 379025.00m);

            var idCarl = new EmployeeId("F3547");
            var carl = new Employee(idCarl, "Carl Edwards", 403466.00m);

            var idKevin = new EmployeeId("C3386");
            var kevin = new Employee(idKevin, "Kevin Harwick", 415261.00m);

            var idMatt = new EmployeeId("F3323");
            var matt = new Employee(idMatt, "Matt Kenseth", 1589390.00m);

            var idBrad = new EmployeeId("D3234");
            var brad = new Employee(idBrad, "Brad Keselowski", 322295.00m);

            var employees = new Dictionary<EmployeeId, Employee>(31)
            {
                [idTony] = tony,
                [idCarl] = carl,
                [idKevin] = kevin,
                [idMatt] = matt,
                [idBrad] = brad
            };

            foreach (var employee in employees.Values)
            {
                WriteLine(employee);
            }

            while (true)
            {
                Write("Enter employee id (X to exit)> ");
                var userInput = ReadLine();
                userInput = userInput.ToUpper();
                if (userInput == "X") break;

                EmployeeId id;
                try
                {
                    id = new EmployeeId(userInput);


                    Employee employee;
                    if (!employees.TryGetValue(id, out employee))
                    {
                        WriteLine("Employee with id {0} does not exist", id);
                    }
                    else
                    {
                        WriteLine(employee);
                    }
                }
                catch (EmployeeIdException ex)
                {
                    WriteLine(ex.Message);
                }
            }

        }
    }
}
