using System;
using System.Collections.Generic;
using System.Text;

namespace Nota.Data
{
    public class TalentData
    {
        public TalentData(TalentReference reference)
        {
            this.Reference = reference;
        }

        public TalentReference Reference { get; }
    }
}
