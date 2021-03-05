/*
 * 1. Построить три класса (базовый и 2 потомка), описывающих некоторых работников
 * с почасовой оплатой (один из потомков) и фиксированной оплатой (второй потомок).
 *  а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной платы. 
 *     Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка»,
 *     для работников с фиксированной оплатой «среднемесячная заработная плата = фиксированная месячная оплата».
 *  б) Создать на базе абстрактного класса массив сотрудников и заполнить его.
 *  в) *Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort().
 *  г) *Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
 */

using System;

namespace HomeTask02
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseEmployee[] employees = new BaseEmployee[]
            {
                new HourlyEmployee("Jack", 12.0m),
                new HourlyEmployee("John", 25.0m),
                new HourlyEmployee("Sue", 30.0m),
                new SalaryEmployee("Frank", 3000m),
                new SalaryEmployee("Mary", 5000m),
                new SalaryEmployee("Jeremy", 2500m)
            };
            Array.Sort(employees);
            Console.WriteLine("Employees\nSorted by natural order");
            foreach (var person in employees)
                Console.WriteLine(person);

            Array.Sort(employees, new EmployeeCompareByByWages());

            EmployeeCollection collection = new EmployeeCollection(employees);
            Console.WriteLine("\nSorted by wages");
            foreach (var person in collection)
                Console.WriteLine(person);
            Console.ReadKey();
        }
    }
}


