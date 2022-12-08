using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<TextMeshPro>().text = "Score: " + ScoreManager.Score;       
    }
}
