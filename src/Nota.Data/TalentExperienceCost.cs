using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nota.Data
{
    public static class TalentExperienceCost
    {
        private readonly static Expression a = MathNet.Symbolics.Expression.FromIntegerFraction(11, 36);
        private readonly static Expression b = MathNet.Symbolics.Expression.FromIntegerFraction(25, 12);
        private readonly static Expression c = MathNet.Symbolics.Expression.FromIntegerFraction(-25, 18);

        public static int CalculateTotalCostForLevel(Compexety compexety, int level)
        {
            if (level < 1)
                throw new ArgumentOutOfRangeException(nameof(level), level, $"Level must be poseitive");
            var complexetyModifier = (int)compexety;

            return (int)Math.Ceiling(Evaluate.Evaluate(null, complexetyModifier * (a * level * level + b * level + c)).RealValue);
        }

        public static int CalculateIncreaseCostForLevel(Compexety compexety, int level)
        {
            if (level < 1)
                throw new ArgumentOutOfRangeException(nameof(level), level, $"Level must be poseitive");

            return CalculateTotalCostForLevel(compexety, level) - (level > 1 ? CalculateTotalCostForLevel(compexety, level - 1) : 0);
        }

        public static double CalculateLevelFromSpentExpirience(Compexety compexety, int exp)
        {
            if (exp == 0)
                return 0;

            var z = (int)compexety;
            var x = (Expression.Sqrt(z * (4 * a * (exp - c * z) + b * b * z)) - b * z) / (2 * a * z);

            return Math.Floor(Evaluate.Evaluate(null, x).RealValue);
        }

    }
}
