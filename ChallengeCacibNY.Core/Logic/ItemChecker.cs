﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCacibNY.Core.Logic
{
    public class ItemChecker : IItemChecker
    {
        public bool IsOperator(string item)
        {
            return Constants.Operators.Contains(item);
        }

        public bool IsNumber(string item)
        {
            return double.TryParse(item, out _);
        }
    }
}
