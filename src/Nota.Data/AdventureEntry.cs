using System;
using System.Runtime.Serialization;

namespace Nota.Data
{
    public readonly struct AdventureEntry
    {
        public AdventureEntry(string title, int gainedExp, string description)
        {
            this.GainedExp = gainedExp;
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Description = description ?? "";
        }

        public int GainedExp { get; }
        public string Title { get; }
        public string Description { get; }

        internal Serelizer GetSerelizer() => new Serelizer() { GainedExp = this.GainedExp, Title = this.Title, Description = this.Description };


        [DataContract]
        internal class Serelizer
        {
            [DataMember]
            public int GainedExp { get; set; }
            [DataMember]
            public string Title { get; set; }
            [DataMember]
            public string Description { get; set; }


        }
    }
}