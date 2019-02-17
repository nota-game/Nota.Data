using System.Collections.Generic;
using System.Collections.ObjectModel;

using Nota.Data.Generated.Misc;

namespace Nota.Data
{
    public class GenusReference:IReference
    {

        public GenusReference(Generated.Lebewesen.OrganismenGattung x, Data data)
        {
            this.Data = data;
            this.origin = x;

            this.Id = x.Id;
            this.Name = x.Name;
            this.Description = x.Beschreibung;

        }

        public Data Data { get; }

        private readonly Generated.Lebewesen.OrganismenGattung origin;

        public string Id { get; }
        public LocalizedString Name { get; }
        public LocalizedString Description { get; }

        void IReference.Initilize(Dictionary<string, TalentReference> talentLookup, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags)
        {
        }
    }
}