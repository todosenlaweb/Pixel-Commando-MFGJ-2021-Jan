﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBag : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletHitEffect;
    public float blockProbability;
    AudioManager Audiomanager;
    private void Awake() {
        Audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            float result = Random.value;
            if (result < blockProbability)
            {
                GetComponent<Animator>().SetTrigger("hit");
                Audiomanager.PlaySound("HitSandbag");
                BulletStopper.HitEffect(bulletHitEffect,collision);
                Destroy(collision.gameObject);
            }

        }
    }
}
