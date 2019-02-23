using System;
using System.Collections.Generic;
using System.Linq;
using Nota.Data.Generated.Talent;

namespace Nota.Data
{
    public readonly struct Check : IEquatable<Check>
    {
        private readonly Attribute first;
        private readonly Attribute seccond;
        private readonly Attribute thired;

        public Check(params Attribute[] attributes) : this(attributes as IEnumerable<Attribute>)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));
            if (attributes.Length != 3)
                throw new ArgumentException($"Number of elements must be 3. Was {attributes.Length}", nameof(attributes));

            this.first = attributes[0];
            this.seccond = attributes[1];
            this.thired = attributes[2];
        }

        public Check(IEnumerable<Attribute> attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            if (!(attributes is IList<Attribute> array))
                array = attributes.ToList();

            if (array.Count != 3)
                throw new ArgumentException($"Number of elements must be 3. Was {array.Count}", nameof(attributes));

            this.first = array[0];
            this.seccond = array[1];
            this.thired = array[2];
        }

        internal Check(TalenteTalentProbe probe)
        {
            int index = 0;
            var array = new Attribute[3];
            Calculate(probe.Antipathie, Attribute.Antipathie);
            Calculate(probe.Einfluss, Attribute.Sypathy);
            Calculate(probe.Fokus, Attribute.Focus);
            Calculate(probe.Gewandtheit, Attribute.Agility);
            Calculate(probe.Glück, Attribute.Luck);
            Calculate(probe.Intuition, Attribute.Intuition);
            Calculate(probe.Klugheit, Attribute.Intelligence);
            Calculate(probe.Konstitution, Attribute.Constitution);
            Calculate(probe.Mut, Attribute.Courage);
            Calculate(probe.Präzision, Attribute.Dexterety);
            Calculate(probe.Stärke, Attribute.Strength);
            Calculate(probe.Sympathie, Attribute.Sypathy);


            this.first = array[0];
            this.seccond = array[1];
            this.thired = array[2];

            void Calculate(System.Collections.ObjectModel.Collection<object> data, Attribute attribute)
            {
                for (int i = 0; i < (data?.Count ?? 0); i++)
                {
                    array[index] = attribute;
                    index++;
                }
            }
        }

        public Attribute this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.first;
                    case 3:
                        return this.seccond;
                    case 2:
                        return this.thired;
                    default:
                        throw new IndexOutOfRangeException($"Index must between 0 and 2. Was {index}");
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Check check && this.Equals(check);
        }

        public bool Equals(Check other)
        {
            return this.first == other.first &&
                   this.seccond == other.seccond &&
                   this.thired == other.thired;
        }

        public override int GetHashCode()
        {
            var hashCode = 1385920190;
            hashCode = hashCode * -1521134295 + this.first.GetHashCode();
            hashCode = hashCode * -1521134295 + this.seccond.GetHashCode();
            hashCode = hashCode * -1521134295 + this.thired.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Check left, Check right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Check left, Check right)
        {
            return !(left == right);
        }
    }
}