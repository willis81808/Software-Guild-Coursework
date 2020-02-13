using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class State
    {
        public string StateAbbreviation;
        public string StateName;
        public decimal TaxRate;

        public State()
        {
            // do nothing
        }
        public State(string abbreviation, string name, decimal taxRate)
        {
            StateAbbreviation = abbreviation;
            StateName = name;
            TaxRate = taxRate;
        }
    }
}
