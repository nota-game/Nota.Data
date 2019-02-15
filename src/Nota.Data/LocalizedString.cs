using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Nota.Data
{
    public class LocalizedString : IComparable<LocalizedString>, IComparable
    {
        private readonly Dictionary<CultureInfo, string> localizedValues;

        internal LocalizedString(System.Collections.ObjectModel.Collection<Nota.Data.Generated.Misc.LokalisierungenLokalisirung> localisations)
        {
            this.localizedValues = localisations.ToDictionary(x => new System.Globalization.CultureInfo(x.Sparche), x => x.Value);
            this.localizedValues.Add(CultureInfo.InvariantCulture, localisations.First().Value);
        }
        public override string ToString()
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentUICulture;

            while (!this.localizedValues.ContainsKey(currentCulture))
                currentCulture = currentCulture.Parent;

            return this.localizedValues[currentCulture];
        }

        int IComparable<LocalizedString>.CompareTo(LocalizedString other) => this.ToString().CompareTo(other?.ToString());

        int IComparable.CompareTo(object obj)
        {
            if (obj is LocalizedString l)
                return (this as IComparable<LocalizedString>).CompareTo(l);
            throw new ArgumentException($"Types does not match {obj?.GetType().FullName ?? "<null>"}");
        }

        public static implicit operator LocalizedString(System.Collections.ObjectModel.Collection<Generated.Misc.LokalisierungenLokalisirung> localisations) => new LocalizedString(localisations);

    }
}