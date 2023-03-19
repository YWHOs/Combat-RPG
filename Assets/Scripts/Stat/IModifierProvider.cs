using System.Collections.Generic;

namespace RPG.Stat
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAddModifier(Stats _stat);
        IEnumerable<float> GetPercentModifier(Stats _stat);
    }

}
