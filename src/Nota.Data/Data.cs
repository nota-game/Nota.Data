using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Nota.Data.Generated.Talent;

namespace Nota.Data
{
    public class Data
    {
        private Data(IReadOnlyList<TalentReference> talents)
        {
            this.Talents = talents;
        }

        public static Data Load(System.IO.Stream stream)
        {
            var serializer = new XmlSerializer(typeof(Generated.Core.Daten));
            var data = serializer.Deserialize(stream) as Generated.Core.Daten;
            var talentList = new List<TalentReference>();

            var output = new Data(talentList.AsReadOnly());
            var directory = new Dictionary<string, TalentReference>();
            foreach (var item in data.Talente)
            {
                var item1 = new TalentReference(item);
                talentList.Add(item1);
                directory.Add(item1.Name, item1);
            }
            foreach (var item in output.Talents)
                item.InitilizeDerivation(directory);


            return output;

        }

        public IReadOnlyList<TalentReference> Talents { get; }

    }
}
