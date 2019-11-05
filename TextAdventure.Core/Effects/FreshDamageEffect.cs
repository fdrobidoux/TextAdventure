using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Effects;

namespace TextAdventure.Core.Effects
{
    public class FreshDamageEffect : CellEffectBase
    {
        public FreshDamageEffect() : base()
        {

        }

        #region "CellEffectBase Abstract methods"
        public override ICellEffect Clone()
        {
            throw new NotImplementedException();
        }

        public override bool UpdateCell(Cell cell)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
