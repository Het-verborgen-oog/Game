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
    public int Score { get { return score; }} 

    const string PlayerTag = "Player";
    private MeshRenderer MeshRenderer;

    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        DefaultMaterial = MeshRenderer.material;
        scoreField = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            Trigger();
        }
    }

    private void SpawnParticles()
    {
        ParticleSystem system = Instantiate(HitParticles, gameObject.transform);
        system.Play();
        Destroy(system, ResetDelay);
    }
    public void Trigger()
    {
        ScoreManager.Add(Score);
        SwitchToClearedMaterial();
        GetComponent<AudioSource>().Play();
        SpawnParticles();
        StartCoroutine(PrepareReset());
        scoreField.text = "Score: " + ScoreManager.Score;

    }

    private void SwitchToDefaultMaterial()
    {
        MeshRenderer.material = DefaultMaterial;
    }

    private void SwitchToClearedMaterial()
    {
        MeshRenderer.material = ClearedMaterial;
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
}
