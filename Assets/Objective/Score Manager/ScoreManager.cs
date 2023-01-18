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
        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: " + Score.ToString();
    }

    public static void Reset()
    {
        Score = 0;
        GameObject.Find("Score").GetComponent<TextMeshProUGUI>().text = "Score: " + Score.ToString();
    }
}
