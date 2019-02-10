using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Nota.Data
{
    public class CharacterData : INotifyPropertyChanged
    {
        private Data data;
        private readonly Dictionary<TalentReference, TalentData> talent;


        private int? totalExpirienceSpent;
        public int TotalExpirienceSpent
        {
            get
            {
                if (this.totalExpirienceSpent == null)
                {
                    this.totalExpirienceSpent = this.Talent.Select(x => x.ExpirienceSpent).Sum();
                    FirePropertyChanged();
                    FirePropertyChanged(nameof(ExpirienceAvailable));
                }
                return this.totalExpirienceSpent.Value;
            }
            set => this.totalExpirienceSpent = value;
        }

        private int totalExpirience;

        public int TotalExpirience
        {
            get => totalExpirience;
            set
            {
                if (totalExpirience != value)
                {
                    totalExpirience = value;
                    FirePropertyChanged();
                    FirePropertyChanged(nameof(ExpirienceAvailable));
                }

            }
        }

        public int ExpirienceAvailable => TotalExpirience - TotalExpirienceSpent;

        internal CharacterData(Data data)
        {
            this.data = data;
            this.talent = new Dictionary<TalentReference, TalentData>();
            this.Talent = new IndexAccessor<TalentReference, TalentData>(this.talent);
        }

        public TalentData AddTallent(TalentReference reference)
        {
            var value = new TalentData(reference, this);

            value.PropertyChanged += OnTalentChanging;
            this.talent.Add(reference, value);
            TalentChanging?.Invoke(value);
            this.TalentChanged?.Invoke(this, (value, CollectionChangedKind.Add));
            return value;
        }

        internal event Action<TalentData> TalentChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<(TalentData talent, CollectionChangedKind kind)> TalentChanged;

        public enum CollectionChangedKind
        {
            Add,
            Remove
        }

        private void OnTalentChanging(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TalentData.BaseLevel))
                TalentChanging?.Invoke((TalentData)sender);
            if (e.PropertyName == nameof(TalentData.ExpirienceSpent))
            {
                totalExpirienceSpent = null;

            }
        }

        public IndexAccessor<TalentReference, TalentData> Talent { get; }


        private void FirePropertyChanged([CallerMemberName]string proeprty = null)
        {
            // When a cached property changes, we want to get its new value to guarantee that other Property Changes will fireie that are triggerd on recalculating the cached values..
            switch (proeprty)
            {
                case nameof(this.TotalExpirienceSpent):
                    this.totalExpirienceSpent = null;
                    _ = this.TotalExpirienceSpent;
                    break;

                default:
                    break;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprty));
        }

    }

    public struct IndexAccessor<TKey, TValue> : IEnumerable<TValue>
    {
        private readonly IDictionary<TKey, TValue> dictionary;

        public IndexAccessor(IDictionary<TKey, TValue> dictionary)
        {
            this.dictionary = dictionary;
        }

        public TValue this[TKey index]
        {
            get
            {
                if (this.dictionary.ContainsKey(index))
                    return this.dictionary[index];
                return default;
            }
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (var item in dictionary.Values)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}