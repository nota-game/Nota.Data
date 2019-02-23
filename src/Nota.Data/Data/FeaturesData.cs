using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Nota.Data.References;

namespace Nota.Data
{
    internal class FeaturesData : INotifyPropertyChanged, IInitilizable
    {
        public FeaturesData(FeaturesReference reference, CharacterData data)
        {
            this.Reference = reference;
            this.Character = data;
        }

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

        private Expressions.ResultProbleme acquistionProblem;
        public Expressions.ResultProbleme AcquistionProblem
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

        public bool IsAcquired => this.NumberOfAcquisition > 0;

        public bool IsReplaced => this.replacingFeatures.Any(x => x.IsAcquired);

        private readonly List<FeaturesData> replacingFeatures = new List<FeaturesData>();

        public FeaturesReference Reference { get; }
        public CharacterData Character { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FirePropertyChanged([CallerMemberName]string proeprty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprty));
        }


        void IInitilizable.Initialize()
        {
            if (this.Reference.Replaces != null)
            {
                this.Character.Features[this.Reference.Replaces].replacingFeatures.Add(this);
                this.PropertyChanged += (sender, e) =>
                {
                    var replacedFeature = this.Character.Features[this.Reference.Replaces];
                    if (e.PropertyName == nameof(this.IsAcquired))
                        replacedFeature.FirePropertyChanged(nameof(this.IsReplaced));
                };
            }

            foreach (var item in this.Reference.Expression.CompetencyInvolved)
                this.Character.Competency[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(CompetencyData.IsActive))
                        this.FirePropertyChanged(nameof(this.AcquistionProblem));
                };

            foreach (var item in this.Reference.Expression.FeaturesInvolved)
                this.Character.Features[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(FeaturesData.IsAcquired))
                        this.FirePropertyChanged(nameof(this.AcquistionProblem));
                };

            foreach (var item in this.Reference.Expression.TalentInvolved)
                this.Character.Talent[item].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(TalentData.BaseLevel))
                        this.FirePropertyChanged(nameof(this.AcquistionProblem));
                };

            if (this.Reference.Expression.TagsInvolved.Any())
                this.Character.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(CharacterData.Tags))
                        this.FirePropertyChanged(nameof(this.AcquistionProblem));
                };
        }
    }
}