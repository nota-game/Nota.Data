using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Nota.Data
{
    public class CharacterData : INotifyPropertyChanged
    {
        private readonly Data data;
        private readonly Dictionary<TalentReference, TalentData> talent;


        private string name;
        public string Name
        {
            get => this.name; set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.FirePropertyChanged();
                }
            }
        }


        internal void AddToUndo(DataActionReturn data) => this.undoStack.Add(data);

        private readonly ObservableCollection<DataActionReturn> undoStack = new ObservableCollection<DataActionReturn>();
        public ReadOnlyObservableCollection<DataActionReturn> UndoStack { get; }

        public void Undo(DataActionReturn dataAction)
        {
            if (dataAction.Origin != this)
                throw new ArgumentException("Undo was from wrong CharacterData");
            if (dataAction.hasUndone)
                return;
            DataActionReturn data;
            do
            {
                data = this.undoStack[this.undoStack.Count - 1];
                data.Undo();
                this.undoStack.RemoveAt(this.undoStack.Count - 1);
            }
            while (data != dataAction);

        }

        private int? totalExpirienceSpent;
        public int TotalExpirienceSpent
        {
            get
            {
                if (this.totalExpirienceSpent == null)
                {
                    this.totalExpirienceSpent =
                        this.Talent.Select(x => x.ExpirienceSpent).Sum()
                        + this.Competency.Where(x => x.IsAcquired).Select(x => x.Reference.Cost).Sum();
                    ;
                    this.FirePropertyChanged(nameof(this.ExpirienceAvailable));
                }
                return this.totalExpirienceSpent.Value;
            }
        }

        private int? totalExpirience;

        public int TotalExpirience
        {
            get
            {
                if (this.totalExpirience == null)
                {
                    this.totalExpirience = this.AdventureEntries.Sum(x => x.GainedExp);
                    this.FirePropertyChanged(nameof(this.ExpirienceAvailable));
                }
                return this.totalExpirience.Value;
            }
        }


        private ImmutableHashSet<TagReference> tags;

        public ImmutableHashSet<TagReference> Tags
        {
            get
            {
                if (this.tags == null)
                {
                    this.tags = ImmutableHashSet.Create(
                        this.Competency.SelectMany(x => x.Reference.Tags)
                        .Concat(this.Features.SelectMany(x => x.Reference.Tags)
                        ).ToArray());
                }
                return this.tags;
            }
        }

        public ReadOnlyObservableCollection<AdventureEntry> AdventureEntries { get; }
        internal readonly ObservableCollection<AdventureEntry> adventureEntries = new ObservableCollection<AdventureEntry>();
        private readonly Dictionary<CompetencyReference, CompetencyData> competency;
        private readonly Dictionary<FeaturesReference, FeaturesData> features;

        internal IndexAccessor<FeaturesReference, FeaturesData> Features { get; }
        internal IndexAccessor<CompetencyReference, CompetencyData> Competency { get; }
        public DataAction<CharacterData, AdventureEntry> AddEvent { get; }


        public int ExpirienceAvailable => this.TotalExpirience - this.TotalExpirienceSpent;


        internal CharacterData(Guid id, Data data)
        {
            this.Id = id;
            this.data = data;
            this.UndoStack = new ReadOnlyObservableCollection<DataActionReturn>(this.undoStack);

            this.AdventureEntries = new ReadOnlyObservableCollection<AdventureEntry>(this.adventureEntries);

            this.AddEvent = new DataAction<CharacterData, AdventureEntry>(this, this, (c, e) =>
            {
                this.adventureEntries.Add(e);
            }, (c, e) =>
            {
                this.adventureEntries.Remove(e);
            }, (c, e) =>
            {
                return $"Abenteuer eintrag  \"{e.Title}\" hinzugefügt ({e.GainedExp} AP)";
            });


            this.talent = new Dictionary<TalentReference, TalentData>();
            this.Talent = new IndexAccessor<TalentReference, TalentData>(this.talent);
            foreach (var reference in data.Talents)
            {
                var value = new TalentData(reference, this);
                value.PropertyChanged += this.OnTalentChanging;
                this.talent.Add(reference, value);
            }


            this.competency = new Dictionary<CompetencyReference, CompetencyData>();
            this.Competency = new IndexAccessor<CompetencyReference, CompetencyData>(this.competency);
            foreach (var reference in data.Competency)
            {
                var value = new CompetencyData(reference, this);
                value.PropertyChanged += this.OnCompetencyChanging;
                this.competency.Add(reference, value);
            }

            this.features = new Dictionary<FeaturesReference, FeaturesData>();
            this.Features = new IndexAccessor<FeaturesReference, FeaturesData>(this.features);
            foreach (var reference in data.Features)
            {
                var value = new FeaturesData(reference, this);
                value.PropertyChanged += this.OnFeaturesChanging;
                this.features.Add(reference, value);
            }


            foreach (var item in this.Competency
                .Concat<IInitilizable>(this.Talent)
                .Concat(this.Features))
                item.Initialize();


            this.adventureEntries.CollectionChanged += (sender, e) =>
            {
                this.FirePropertyChanged(nameof(this.TotalExpirience));
            };
        }

        private void OnFeaturesChanging(object sender, PropertyChangedEventArgs e)
        {
            var feature = sender as FeaturesData;
            if (e.PropertyName == nameof(FeaturesData.IsAcquired))
            {
                if (feature.Reference.Tags.Length > 0)
                    this.FirePropertyChanged(nameof(this.Tags));
            }
        }

        private void OnCompetencyChanging(object sender, PropertyChangedEventArgs e)
        {
            var competency = sender as CompetencyData;
            if (e.PropertyName == nameof(CompetencyData.IsAcquired))
            {
                this.FirePropertyChanged(nameof(this.TotalExpirienceSpent));
                if (competency.Reference.Tags.Length > 0)
                    this.FirePropertyChanged(nameof(this.Tags));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnTalentChanging(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == nameof(TalentData.BaseLevel))
            //    TalentChanging?.Invoke((TalentData)sender);
            if (e.PropertyName == nameof(TalentData.ExpirienceSpent))
                this.FirePropertyChanged(nameof(this.TotalExpirienceSpent));
        }

        public IndexAccessor<TalentReference, TalentData> Talent { get; }
        public Guid Id { get; }

        private void FirePropertyChanged([CallerMemberName]string proeprty = null)
        {
            // When a cached property changes, we want to get its new value to guarantee that other Property Changes will fireie that are triggerd on recalculating the cached values..
            switch (proeprty)
            {
                case nameof(this.TotalExpirienceSpent):
                    this.totalExpirienceSpent = null;
                    _ = this.TotalExpirienceSpent;
                    break;

                case nameof(this.TotalExpirience):
                    this.totalExpirience = null;
                    _ = this.TotalExpirience;
                    break;

                case nameof(this.Tags):
                    this.tags = null;
                    _ = this.Tags;
                    break;

                default:
                    break;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprty));
        }

        internal Serelizer GetSerelizer()
        {

            return new Serelizer()
            {
                Id = this.Id,
                Name = this.Name,
                Talents = this.Talent.Select(x => x.GetSerelizer()).Where(x => x.SpentExperience > 0).ToArray(),
                Competency = this.Competency.Select(x => x.GetSerelizer()).Where(x => x.NumberOfAcquisition > 0).ToArray(),
                AdventureEntrys = this.AdventureEntries.Select(x => x.GetSerelizer()).ToArray()
            };
        }

        [DataContract]
        internal class Serelizer
        {
            [DataMember]
            public Guid Id { get; set; }
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public TalentData.Serelizer[] Talents { get; set; }

            [DataMember]
            public CompetencyData.Serelizer[] Competency { get; set; }

            [DataMember]
            public AdventureEntry.Serelizer[] AdventureEntrys { get; set; }
        }
    }
}