using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ScoreManager
{
    // Public Properties
    public static int Score { get; private set; }
    
    // Public Methods
    public static void Add(int amount)
    {
        Score += amount;
    }

    public static void Reset()
    {
        Score = 0;
    }
}
