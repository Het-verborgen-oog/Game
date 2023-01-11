using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ring : MonoBehaviour, IScore,IInteractable
{
    [SerializeField]
    ParticleSystem HitParticles;

    [SerializeField]
    Material DefaultMaterial;

    [SerializeField]
    Material ClearedMaterial;

    [SerializeField]
    [Range(0,45)]
    float ResetDelay = 5f;

    [SerializeField]
    int score = 5;

    [SerializeField]
    TextMeshProUGUI scoreField;

    const string PLAYERTAG = "Player";
    private MeshRenderer MeshRenderer;

    // Public Properties
    public int Score { get { return score; }} 

    // Monobehaviour Methods
    void Start()
    {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        DefaultMaterial = MeshRenderer.material;
        scoreField = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYERTAG))
        {
            if (other.GetComponent<DolphinMovement>().idleBehaviour.IsIdle) return;
            Trigger();
        }
    }

    // Public Methods
    public void Trigger()
    {
        ScoreManager.Add(Score);
        SwitchToClearedMaterial();
        GetComponent<AudioSource>().Play();
        if(HitParticles != null) SpawnParticles();
        StartCoroutine(PrepareReset());
        scoreField.text = "Score: " + ScoreManager.Score;
    }

    public IEnumerator PrepareReset()
    {
        yield return new WaitForSeconds(ResetDelay);
        Reset();
    }

    public void Reset()
    {
        SwitchToDefaultMaterial();
    }

    // Private Methods
    private void SpawnParticles()
    {
        ParticleSystem system = Instantiate(HitParticles, gameObject.transform);
        system.Play();
        Destroy(system, ResetDelay);
    }

    private void SwitchToDefaultMaterial()
    {
        MeshRenderer.material = DefaultMaterial;
    }

    private void SwitchToClearedMaterial()
    {
        MeshRenderer.material = ClearedMaterial;
    }
}
