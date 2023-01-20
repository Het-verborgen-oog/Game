using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodrayGuidance : ParticleGuidance
{
    

    [Header("Colors")]
    [SerializeField]
    private Color showColor;

    [SerializeField]
    private Color hiddenColor;


    private float startAlpha;
    private const float SELECTEDALPHA = 250;
    private const float STARTALPHA = 50;

    private Color godrayColor;

    // Monobehaviour Methods
    private void Start()
    {
        base.Start();
        godrayColor = particleSystem.main.startColor.color;
        hiddenColor = godrayColor;
    }

    // Public Methods
    public override void HidePath()
    {
        SetAlphaOfGodray(hiddenColor);
    }

    public override void ShowPath()
    {
        SetAlphaOfGodray(showColor);
    }

    // Private Methods
    private void SetAlphaOfGodray(Color col)
    {
        Color updatedColor = col;
        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = updatedColor;
    }
}
