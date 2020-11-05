using System;
using System.Collections.Generic;
using Example.core;
using pmapper = Pinomapper;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var mapper = new pmapper.Pinomapper();
            Console.WriteLine("Single object");
            var employeeInfo = new EmployeeInfo()
            {
                Name = "Sasha",
                Age = 35,
                CompanyName = "Sasha LTD",
                Position = "CEO",
                StuffSize = 500
            };
            var employeeInfos = new List<EmployeeInfo>()
            {
                new EmployeeInfo
                {
                    Name = "Sasha",
                    Age = 35,
                    CompanyName = "Sasha LTD",
                    Position = "CEO",
                    StuffSize = 500,
                    Computer = new Computer
                    {
                        Model = "IBM PC"
                    }
                },
                new EmployeeInfo
                {
                    Name = "Misha",
                    Age = 36,
                    CompanyName = "Misha LTD",
                    Position = "Tech",
                    StuffSize = 400,
                    Computer = new Computer
                    {
                        Model = "Apple"
                    }
                }
            };
            try
            {
                Console.WriteLine("Single object");
                var company = mapper.Map<Company>(employeeInfo);
                var employee = mapper.Map<Employee>(employeeInfo);

                Console.WriteLine("List of object");
                var employees = mapper.Map<List<Employee>>(employeeInfos);
                var companies = mapper.Map<List<Company>>(employeeInfos);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Pinomapper Example Error: {e.Message}");
            }
        }
    }
}
