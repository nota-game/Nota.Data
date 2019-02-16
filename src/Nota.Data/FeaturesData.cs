using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

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
        }
    }
}