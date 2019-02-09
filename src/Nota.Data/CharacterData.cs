using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Nota.Data
{
    public class CharacterData
    {
        private Data data;
        private readonly Dictionary<TalentReference, TalentData> talent;

        internal CharacterData(Data data)
        {
            this.data = data;
            this.talent = new Dictionary<TalentReference, TalentData>();
            this.Talent = new ReadOnlyDictionary<TalentReference, TalentData>(this.talent);
        }

        public TalentData AddTallent(TalentReference reference)
        {
            var value = new TalentData(reference, this);

            value.PropertyChanged += OnTalentChanging;
            this.talent.Add(reference, value);
            TalentChanging?.Invoke(value);
            return value;
        }

        internal event Action<TalentData> TalentChanging;

        private void OnTalentChanging(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TalentData.BaseLevel))
                TalentChanging?.Invoke((TalentData)sender);
        }

        public IReadOnlyDictionary<TalentReference, TalentData> Talent { get; }

    }
}