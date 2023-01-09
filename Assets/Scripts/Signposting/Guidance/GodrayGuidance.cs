using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodrayGuidance : ParticleGuidance
{
    private float startAlpha;
    private const float SELECTEDALPHA = 250;
    private const float STARTALPHA = 50;

    private Color godrayColor;

    [Header("Colors")]
    [SerializeField]
    private Color showColor;
    [SerializeField]
    private Color hiddenColor;
    private void Start()
    {
        base.Start();
        godrayColor = particleSystem.main.startColor.color;
        hiddenColor = godrayColor;
    }
    public override void HidePath()
    {
        SetAlphaOfGodray(hiddenColor);
        Debug.Log(this.gameObject.name + "is hiding");
    }

    public override void ShowPath()
    {
        SetAlphaOfGodray(showColor);
        Debug.Log(this.gameObject.name + "is showing");
    }

    private void SetAlphaOfGodray(Color col)
    {
        Color updatedColor = col;
        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = updatedColor;
    }
}
