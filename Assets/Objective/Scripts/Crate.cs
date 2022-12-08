﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class Crate : MonoBehaviour, IScore, IInteractable
{
    [SerializeField]
    ParticleSystem HitParticle;

    [Header("Crate Settings")]
    [Range(0, 45)]
    [SerializeField]
    float ResetDelay = 5f;
    [SerializeField]
    int score = 20;

    [SerializeField]
    TextMeshProUGUI scoreField;

    public int Score { get { return score; }}

    private bool Triggered = false;
    private Animator animator;

    private const string IdleAnimation = "Idle";
    private const string CollideAnimation = "Collect";
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(IdleAnimation);
        scoreField = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure this works properly with whoever is working on Dolfy
        {
            Trigger();
        }
    }

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

    private void SpawnParticles()
    {
        ParticleSystem system = Instantiate(HitParticle, gameObject.transform);
        system.Play();
        Destroy(system, ResetDelay);
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
}
