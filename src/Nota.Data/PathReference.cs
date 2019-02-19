using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Misc;
using Nota.Data.Generated.Pfad;

namespace Nota.Data
{
    public class PathReference : IReference
    {

        internal PathReference(PfadGruppenPfadePfad x, Data data, PathGroupReference parent)
        {
            this.origin = x;
            this.Data = data;
            this.parent = parent;
            this.Id = x.Id;
            this.Name = x.Name;
            this.Description = x.Beschreibung;
            this.Levels = x.Levels.Select(y => new LevelReference(y, data,this)).ToImmutableArray();
            //this.= x.NächstePfade;
        }

        private readonly PfadGruppenPfadePfad origin;
        private readonly PathGroupReference parent;

        public Data Data { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public LocalizedString Description { get; }
        internal ImmutableArray<LevelReference> Levels { get; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {
            foreach (IReference item in this.Levels)
                item.Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPath);

        }
    }
}