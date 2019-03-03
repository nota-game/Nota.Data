using Nota.Data.Generated.Lebewesen;

namespace Nota.Data
{
    public class AttributeModification
    {

        internal AttributeModification(EigenschaftsMods eigenschaften)
        {
            if (eigenschaften == null)
                return;
            this.Sympathy = eigenschaften.Sympathie?.Mod;
            this.Strength = eigenschaften.Stärke?.Mod;
            this.Courage = eigenschaften.Mut?.Mod;
            this.Constitution = eigenschaften.Konstitution?.Mod;
            this.Intelegence = eigenschaften.Klugheit?.Mod;
            this.Intuition = eigenschaften.Intuition?.Mod;
            this.Luck = eigenschaften.Glück?.Mod;
            this.Agility = eigenschaften.Gewandtheit?.Mod;
            this.Dexterety = eigenschaften.Feinmotorik?.Mod;
            this.Antipathy = eigenschaften.Antipathie?.Mod;
        }

        public int? Sympathy { get; }
        public int? Strength { get; }
        public int? Courage { get; }
        public int? Constitution { get; }
        public int? Intelegence { get; }
        public int? Intuition { get; }
        public int? Luck { get; }
        public int? Agility { get; }
        public int? Dexterety { get; }
        public int? Antipathy { get; }

    }
}