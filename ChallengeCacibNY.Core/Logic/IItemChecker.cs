using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCacibNY.Core.Logic
{
    public interface IItemChecker
    {
        bool IsOperator(string item);
        bool IsNumber(string item);
    }
}
