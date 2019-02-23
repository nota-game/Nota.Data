using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Misc;
using Nota.Data.Generated.Pfad;

namespace Nota.Data.References
{
    public class PathGroupReference : IReference
    {
        public PathGroupReference(PfadGruppenPfade x, Data data)
        {
            this.origin = x;
            this.Data = data;

            this.Id = x.Id;
            this.Description = x.Beschreibung;
            this.Name = x.Name;
            this.MultiSelect = x.Mehrfachauswahl;
            this.Pathes = x.Pfad.Select(y => new PathReference(y, data,this)).ToImmutableArray();

        }

        private readonly PfadGruppenPfade origin;

        public Data Data { get; }
        public string Id { get; }
        public LocalizedString Description { get; }
        public LocalizedString Name { get; }
        public bool MultiSelect { get; }
        internal ImmutableArray<PathReference> Pathes { get; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            foreach (IReference item in this.Pathes)
                item.Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath);
        }

    }
}
