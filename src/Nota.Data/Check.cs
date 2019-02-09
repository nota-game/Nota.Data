﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nota.Data.Generated.Talent;

namespace Nota.Data
{
    public struct Check
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

            first = attributes[0];
            seccond = attributes[1];
            thired = attributes[2];
        }

        public Check(IEnumerable<Attribute> attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            if (!(attributes is IList<Attribute> array))
                array = attributes.ToList();

            if (array.Count != 3)
                throw new ArgumentException($"Number of elements must be 3. Was {array.Count}", nameof(attributes));

            first = array[0];
            seccond = array[1];
            thired = array[2];
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


            first = array[0];
            seccond = array[1];
            thired = array[2];

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
                        return first;
                    case 3:
                        return seccond;
                    case 2:
                        return thired;
                    default:
                        throw new IndexOutOfRangeException($"Index must between 0 and 2. Was {index}");
                }
            }
        }
    }
}