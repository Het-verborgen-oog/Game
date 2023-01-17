using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct DifferentPotValues
{
    public float MinValue;
    public float TurnoverValue;
    public float MaxValue;

    
    public DifferentPotValues(float _minValue, float _turnoverValue, float _maxValue)
    {
        MinValue = _minValue;
        TurnoverValue = _turnoverValue;
        MaxValue = _maxValue;
    }
}
