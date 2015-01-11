﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /*
     * machines can be part of input - they dont need separate field - e.g.
     *      cow -> cow + milk
     */
    public class Technology
    {
        public Technology(string name, int labourNeeded,
            IDictionary<Commodity, int> input, IDictionary<Commodity, int> output)
        {
            Name = name;
            LabourNeeded = labourNeeded;
            Input = input;
            Output = output;
        }

        public string Name { get; private set; }
        public int LabourNeeded { get; private set; }
        public IDictionary<Commodity, int> Input { get; private set; }
        public IDictionary<Commodity, int> Output { get; private set; }

        public void Produce(Company company, int times)
        {
            Console.WriteLine("Company {0} uses {1} technology {2} times",
                company.Name, Name, times);
            UseResources(company, times);
            GiveOutput(company, times);
        }

        private void GiveOutput(Company company, int times)
        {
            foreach (var item in Output)
            {
                int commodityProduced = item.Value * times;

                if (!company.commodities.ContainsKey(item.Key))
                {
                    company.commodities.Add(item.Key, 0);
                }
                company.commodities[item.Key] += commodityProduced;
            }
        }

        private void UseResources(Company company, int times)
        {
            UseCommodities(company, times);
            UseEmployees(company, times);
        }

        private void UseEmployees(Company company, int times)
        {
            int employeesNeeded = times * LabourNeeded;
            if (company.FreeEmployees.Count < employeesNeeded)
                throw new ApplicationException();

            company.UseEmployees(
                company.Employees.Take(employeesNeeded).Select(x => x.Key).ToList());
        }

        private void UseCommodities(Company company, int times)
        {
            foreach (var item in Input)
            {
                int commodityNeeded = item.Value * times;

                if (!company.commodities.ContainsKey(item.Key))
                    throw new ApplicationException();
                if (company.commodities[item.Key] < commodityNeeded)
                    throw new ApplicationException();

                company.commodities[item.Key] -= commodityNeeded;
            }
        }
    }
}
