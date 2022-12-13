using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkIndicator : MonoBehaviour, IIdleAction
{
    [SerializeField]
    private List<GameObject> indicators;

    [SerializeField]
    private float minAlpha = 10f;

    [SerializeField]
    private float blinkTime = 1f;

    private float defaultAlpha;
    private int shiftDirection = -1;
    private List<Renderer> indicatorRenderers;

    void Start()
    {
        indicatorRenderers = new();

        if (indicators != null && indicators.Count > 0)
        {
            defaultAlpha = indicators[0].GetComponent<Renderer>().material.color.a;

            foreach (GameObject indicator in indicators)
            {
                indicatorRenderers.Add(indicator.GetComponent<Renderer>());
            }          
        }

        enabled = false;
    }

    public void SwitchIdleState(bool isIdle)
    {
        this.enabled = isIdle;
    }

    private void OnDisable() {
        foreach(GameObject indicator in indicators)
        {
            indicator.SetActive(false);
        }
    }

    private void OnEnable() {
        foreach(GameObject indicator in indicators)
        {
            indicator.SetActive(true);
        }
    }

    void Update()
    {
        if (indicators == null || indicators.Count == 0)
        {
            this.enabled = false;
        }

        float alphaShift = Time.deltaTime * ((defaultAlpha - minAlpha) / blinkTime) * shiftDirection;
        Color currentColor = indicatorRenderers[0].material.color;

        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a + alphaShift);

        foreach(Renderer renderer in indicatorRenderers)
        {
            renderer.material.color = newColor;
        }
        
        if (indicatorRenderers[0].material.color.a <= minAlpha)
        {
            shiftDirection = 1;
        }
        if (indicatorRenderers[0].material.color.a >= defaultAlpha)
        {
            shiftDirection = -1;
        }
        
    }
}
