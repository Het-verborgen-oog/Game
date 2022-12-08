using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ScoreManager
{
    public static int Score { get; private set; }
    public static void Add(int amount)
    {
        Score += amount;
        GameObject.Find("Score").GetComponent<TextMeshPro>().text = "Score: " + Score;
    }

    public static void Reset()
    {
        Score = 0;
    }
}
