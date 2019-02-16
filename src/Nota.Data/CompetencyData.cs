using System.ComponentModel;
using System.Runtime.CompilerServices;

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

        public bool IsReplaced => this.Reference.Replaces != null ? this.Character.Competency[this.Reference.Replaces].IsAcquired : false;

        public DataAction<CompetencyData> Acquire { get; }
        public CompetencyReference Reference { get; }
        public CharacterData Character { get; }

        public CompetencyData(CompetencyReference reference, CharacterData character)
        {
            this.Reference = reference;
            this.Character = character;
            this.Character.PropertyChanged += this.Character_PropertyChanged;

            this.Acquire = new DataAction<CompetencyData>(character, this,
                execute: c => this.NumberOfAcquisition++,
                undoExecute: c => this.NumberOfAcquisition--,
                description: c => $"Sonderfertigkeit {c.Reference.Name} erworben",
                canExecute: c => !this.IsAcquired && c.Character.ExpirienceAvailable >= c.Reference.Cost
            );
        }

        void IInitilizable.Initialize()
        {
            if (this.Reference.Replaces != null)
                this.Character.Competency[this.Reference.Replaces].PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(this.IsAcquired))
                        this.FirePropertyChanged(nameof(this.IsReplaced));
                };
        }

        private void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.Character.ExpirienceAvailable))
                this.Acquire.FireCanExecuteChanged();
        }

        private void FirePropertyChanged([CallerMemberName]string proeprty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proeprty));
        }

    }
}