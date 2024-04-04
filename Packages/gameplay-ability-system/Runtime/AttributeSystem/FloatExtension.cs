﻿using UnityEngine;

namespace GameplayAbilitySystem.AttributeSystem
{
    public static class FloatExtension
    {
        public static bool NearlyEqual(this float a, float b, float epsilon = 0.001f)
        {
            return Mathf.Abs(a - b) < epsilon;
        }
    }
}