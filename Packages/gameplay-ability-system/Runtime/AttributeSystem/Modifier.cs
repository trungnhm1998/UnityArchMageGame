using System;

namespace GameplayAbilitySystem.AttributeSystem
{
    [Serializable]
    public struct Modifier
    {
        public float Additive;
        public float Multiplicative;
        public float Overriding;

        /// <summary>
        /// Will use the last modifier Override value
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Modifier operator +(Modifier a, Modifier b)
        {
            return new Modifier()
            {
                Additive = a.Additive + b.Additive,
                Multiplicative = a.Multiplicative + b.Multiplicative,
                Overriding = b.Overriding
            };
        }

        public override string ToString() => $"Additive: {Additive}, Multiplicative: {Multiplicative}, Overriding: {Overriding}";
    }
}