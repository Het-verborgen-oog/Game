using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class Crate : MonoBehaviour, IScore, IInteractable
{
    [SerializeField]
    private ParticleSystem hitParticle;

    [Header("Crate Settings")]
    [Range(0, 45)]
    [SerializeField]
    private float ResetDelay = 5f;

    [SerializeField]
    private int score = 20;

    [SerializeField]
    private TextMeshProUGUI scoreField;

    private bool Triggered = false;
    private Animator animator;

    private const string IdleAnimation = "Idle";
    private const string CollideAnimation = "Collect";

    const string PLAYERTAG = "Player";

    // Public Properties
    public int Score { get { return score; }}

    // Monobehaviour Methods
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(IdleAnimation);
        scoreField = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYERTAG)) // Make sure this works properly with whoever is working on Dolfy
        {
            if (other.GetComponent<DolphinMovement>().idleBehaviour.IsIdle) return;
            Trigger();
        }
    }

    // Public Methods
    public void Trigger()
    {
        Triggered = true;

        ScoreManager.Add(Score);
        scoreField.text = "Score: " + ScoreManager.Score;
        animator.Play(CollideAnimation);
        SpawnParticles();
        GetComponent<AudioSource>().Play();
        StartCoroutine(PrepareReset());
    }

    public IEnumerator PrepareReset()
    {
        if (Triggered)
        {
            yield return new WaitForSeconds(ResetDelay);
            Triggered = false;
            Reset();
        }
    }

    public void Reset()
    {
        animator.Play(IdleAnimation);
    }

    // Private Methods
    private void SpawnParticles()
    {
        ParticleSystem system = Instantiate(hitParticle, gameObject.transform);
        system.Play();
        Destroy(system, ResetDelay);
    }
}
