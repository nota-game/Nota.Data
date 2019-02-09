using System;
using System.Collections.Generic;

namespace Nota.Data
{
    public abstract class DerivationCollection : AbstractDerivation
    {
        public IList<AbstractDerivation> Derivations { get; }

    }
    public class DerivationAll : DerivationCollection
    {

    }

    public class DerivationMax : DerivationCollection
    {
        public int Count { get; internal set; }

    }

    public abstract class AbstractDerivation
    {

    }
    public class Derivation : AbstractDerivation
    {
        public Derivation(TalentReference talent, int count)
        {
            this.Talent = talent ?? throw new ArgumentNullException(nameof(talent));
            this.Count = count;
        }

        public TalentReference Talent { get; }
        public int Count { get; }
        
    }
}