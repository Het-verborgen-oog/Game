using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodrayGuidance : ParticleGuidance
{
    private float startAlpha;
    private const float SELECTEDALPHA = 200;

    private Color godrayColor;
    private void Start()
    {
        base.Start();
        godrayColor = particleSystem.main.startColor.color;
        startAlpha = godrayColor.a;
    }
    public override void HidePath()
    {
        SetAlphaOfGodray(startAlpha);
    }

    public override void ShowPath()
    {
        SetAlphaOfGodray(SELECTEDALPHA);
    }

    private void SetAlphaOfGodray(float alpha)
    {
        Color updatedColor = new Color(godrayColor.r, godrayColor.g, godrayColor.b, alpha);
        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = updatedColor;
    }
}
