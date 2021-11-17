using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommissionCalculator
{
    public class CommissionItem
    {
        static short availableID = 1;

        public short ID { get; }
        public float Price { get; set; }
        public bool Replacement { get; set; }
        public bool OutOfDepartment { get; set; }
        public bool Return { get; set; }
        public float ReplacementPrice { get; private set; }
        public float Commission { get; private set; }

        readonly float under10 = .06f;
        readonly float between10100 = .03f;
        readonly float above100 = .015f;
        readonly float OOD = .01f;

        public CommissionItem(float _price, bool _ood, bool _replacement, bool _return)
        {
            Price = _price;
            OutOfDepartment = _ood;
            Replacement = _replacement;
            Return = _return;
            ID = availableID;
            availableID++;

            Commission = CommissionReturn();
            ReplacementPrice = (Replacement) ? ReplacementPriceReturn() : 0;

            Price = (!Return) ? Price : -Price;
        }

        private float CommissionReturn()
        {
            float returnCommission;

            if (!OutOfDepartment)
            {
                if (Price < 10) { returnCommission = Price * under10; }
                else if (Price < 100) { returnCommission = Price * between10100; }
                else { returnCommission = Price * above100; }
            }
            else { returnCommission = Price * OOD; }

            return (!Return) ? returnCommission : -returnCommission;
        }

        private float ReplacementPriceReturn()
        {
            //There's definietly a better way to do this,
            //but this'll have to do.
            float returnFloat;

            if (Price < 10) { returnFloat = 1; }
            else if (Price < 20) { returnFloat = 2; }
            else if (Price < 25) { returnFloat = 2.5f; }
            else if (Price < 50) { returnFloat = 5; }
            else if (Price < 70) { returnFloat = 7; }
            else if (Price < 100) { returnFloat = 10; }
            else if (Price < 200) { returnFloat = 20; }
            else if (Price < 300) { returnFloat = 30; }
            else if (Price < 400) { returnFloat = 50; }
            else { returnFloat = 70; }

            return (!Return) ? returnFloat : -returnFloat;
        }
    }
}
