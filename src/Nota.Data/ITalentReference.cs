using System.Collections.Generic;

namespace Nota.Data
{
    internal interface IReference
    {
        void Initilize(Dictionary<string, TalentReference> talentLookup, Dictionary<string, CompetencyReference> directoryCompetency, Dictionary<string, FeaturesReference> directoryFeatures, Dictionary<string, TagReference> directoryTags);
    }
}