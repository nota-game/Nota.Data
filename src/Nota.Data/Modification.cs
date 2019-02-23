using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Nota.Data.Generated.Lebewesen;
using Nota.Data.References;

namespace Nota.Data
{
    public class ModificationReference : IReference
    {
        private readonly Mods origin;

        public AttributeModification AttributeModification { get; }

        internal ModificationReference(Mods mods)
        {
            this.origin = mods;
            this.AttributeModification = new AttributeModification(this.origin.Eigenschaften);
        }

        public ImmutableArray<FeaturesReference> Features { get; private set; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            if (this.origin.BesonderheitenSpecified)
                this.Features = this.origin.Besonderheiten.Select(x => directoryFeatures[x.Id]).ToImmutableArray();
            else
                this.Features = ImmutableArray.Create<FeaturesReference>();

        }
    }
}