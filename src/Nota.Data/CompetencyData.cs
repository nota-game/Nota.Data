using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Nota.Data
{
    internal class CompetencyData : INotifyPropertyChanged, IInitilizable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int numberOfAcquisition;
        internal int NumberOfAcquisition
        {
            get => this.numberOfAcquisition; set
            {
                if (this.numberOfAcquisition != value)
                {
                    var oldValue = this.numberOfAcquisition;
                    this.numberOfAcquisition = value;
                    if ((oldValue == 0) ^ (value == 0))
                        this.FirePropertyChanged(nameof(this.IsAcquired));
                    this.FirePropertyChanged();
                }
            }
        }

        public bool IsAcquired => this.NumberOfAcquisition > 0;

        public bool IsReplaced => this.replacingCompetency.Any(x => x.IsAcquired);

        private readonly List<CompetencyData> replacingCompetency = new List<CompetencyData>();

        public DataAction<CompetencyData> Acquire { get; }
        public CompetencyReference Reference { get; }
        public CharacterData Character { get; }

        private Expressions.Result acquistionProblem;
        public Expressions.Result AcquistionProblem
        {
            get
            {
                if (this.acquistionProblem == null)
                {
                    this.acquistionProblem = this.Reference.Expression.Evaluate(this.Character);
                }
                return this.acquistionProblem;
            }
        }

        public CompetencyData(CompetencyReference reference, CharacterData character)
        {
            this.Reference = reference;
            this.Character = character;
            this.Character.PropertyChanged += this.Character_PropertyChanged;

            this.Acquire = new DataAction<CompetencyData>(character, this,
                execute: c => this.NumberOfAcquisition++,
                undoExecute: c => this.NumberOfAcquisition--,
                description: c => $"Sonderfertigkeit {c.Reference.Name} erworben",
                canExecute: c => !this.IsAcquired && c.Character.ExpirienceAvailable >= c.Reference.Cost && this.Reference.Expression.Evaluate(this.Character)
            );
        }

        void IInitilizable.Initialize()
        {
            if (this.Reference.Replaces != null)
            {
                this.Character.Competency[this.Reference.Replaces].replacingCompetency.Add(this);
                this.PropertyChanged += (sender, e) =>
                {
                    var replacedCompetency = this.Character.Competency[this.Reference.Replaces];
                    if (e.PropertyName == nameof(this.IsAcquired))
                        replacedCompetency.FirePropertyChanged(nameof(this.IsReplaced));
                };
            }

            foreach (var item in this.Reference.Expression.CompetencyInvolved)
                this.Character.Competency[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(CompetencyData.IsAcquired))
                    {
                        this.Acquire.FireCanExecuteChanged();
                        this.FirePropertyChanged(nameof(this.AcquistionProblem));
                    }
                };

            foreach (var item in this.Reference.Expression.FeaturesInvolved)
                this.Character.Features[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(CompetencyData.IsAcquired))
                    {
                        this.Acquire.FireCanExecuteChanged();
                        this.FirePropertyChanged(nameof(this.AcquistionProblem));
                    }
                };

            foreach (var item in this.Reference.Expression.TalentInvolved)
                this.Character.Talent[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(TalentData.BaseLevel))
                    {
                        this.Acquire.FireCanExecuteChanged();
                        this.FirePropertyChanged(nameof(this.AcquistionProblem));
                    }
                };

            if (this.Reference.Expression.TagsInvolved.Any())
                this.Character.PropertyChanged += (sender, e) =>
                 {
                     if (e.PropertyName == nameof(CharacterData.Tags))
                     {
                         this.Acquire.FireCanExecuteChanged();
                         this.FirePropertyChanged(nameof(this.AcquistionProblem));
                     }
                 };
        }

        private void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.Character.ExpirienceAvailable))
                this.Acquire.FireCanExecuteChanged();
        }

        private void FirePropertyChanged([CallerMemberName]string proeprty = null)
        {

            switch (proeprty)
            {
                case nameof(this.AcquistionProblem):
                    this.acquistionProblem = null;
                    _ = this.AcquistionProblem;
                    break;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprty));
        }

        internal Serelizer GetSerelizer() => new Serelizer() { Id = this.Reference.Id, NumberOfAcquisition = this.NumberOfAcquisition };

        [DataContract]
        internal class Serelizer
        {
            [DataMember]
            public string Id { get; set; }

            [DataMember]
            public int NumberOfAcquisition { get; set; }
        }

    }
}