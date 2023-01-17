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
        particleSystem.Stop();
        SetAlphaOfGodray(hiddenColor);
    }

    public override void ShowPath()
    {
        particleSystem.Play();
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
