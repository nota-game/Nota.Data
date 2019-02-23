using System;
using System.Collections.Generic;
using Nota.Data.References;

namespace Nota.Data
{
    public abstract class DerivationCollection : AbstractDerivation
    {
        public IList<AbstractDerivation> Derivations { get; } = new List<AbstractDerivation>();

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