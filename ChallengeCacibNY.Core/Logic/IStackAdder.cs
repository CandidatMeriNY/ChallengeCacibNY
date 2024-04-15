using ChallengeCacibNY.Core.Models;

namespace ChallengeCacibNY.Core.Logic
{
    public interface IStackAdder
    {
        StackValue Add(StackValue stackValue, string newItem);
    }
}
