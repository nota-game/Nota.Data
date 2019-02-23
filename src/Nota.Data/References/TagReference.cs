using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nota.Data.Generated.Misc;

namespace Nota.Data.References
{
    public class TagReference : IReference
    {

        internal TagReference(Tag x, Data data)
        {
            this.Id = x.Id;
            this.Name = x.Name;
            this.Description = x.Beschreibung;
            this.Data = data;
        }

        public string Id { get; }
        public LocalizedString Name { get; }
        public LocalizedString Description { get; }
        public Data Data { get; }

        void IReference.Initilize(Dictionary<string, TalentReference> directoryTalent, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags, Dictionary<string, GenusReference> directoryGenus, Dictionary<string, BeingReference> directoryBeing, Dictionary<string, PathGroupReference> directoryPath)
        {

        }
    }
}