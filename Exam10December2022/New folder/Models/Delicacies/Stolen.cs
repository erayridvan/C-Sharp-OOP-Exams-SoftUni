using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    internal class Stolen : Delicacy
    {
        public Stolen(string delicacyName)
            : base(delicacyName, 3.50)
        {
        }
    }
}
