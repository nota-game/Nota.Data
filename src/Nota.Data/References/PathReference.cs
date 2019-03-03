using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Nota.Data.Generated.Misc;
using Nota.Data.Generated.Pfad;

namespace Nota.Data.References
{
    public class PathReference : IReference, IEquatable<PathReference>
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
            
        }

        private readonly PfadGruppenPfadePfad origin;
        private readonly PathGroupReference parent;

        public Data Data { get; }
        public string Id { get; }
        public LocalizedString Name { get; }
        public LocalizedString Description { get; }
        internal ImmutableArray<LevelReference> Levels { get; }
        public ImmutableHashSet<PathReference> DefaultPathes { get; private set; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, OrganismReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPathGroup, Dictionary<string, PathReference> directoryPath)
        {
            foreach (IReference item in this.Levels)
                item.Initilize(directoryTalent, directoryCompetency, directoryFeatures, directoryTags, directoryGenus, directoryBeing, directoryPathGroup, directoryPath);

            this.DefaultPathes = this.origin.NächstePfade.Select(x => directoryPath[x.Id]).ToImmutableHashSet();

        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PathReference);
        }

        public bool Equals(PathReference other)
        {
            return other != null &&
                   this.Id == other.Id;
        }

        public static bool operator ==(PathReference left, PathReference right)
        {
            return EqualityComparer<PathReference>.Default.Equals(left, right);
        }

        public static bool operator !=(PathReference left, PathReference right)
        {
            return !(left == right);
        }
    }
}