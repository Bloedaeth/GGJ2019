﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //This script is for ANYTHING that has health

    public float maxHitPoints;
    [HideInInspector] public float armor;

    [SerializeField] float hitPoints;
    public float HitPoints
    {
        get
        {
            return hitPoints;
        }

        set
        {
            hitPoints = value;
            if (hitPoints > maxHitPoints)
            {
                hitPoints = maxHitPoints;
            }
            if (HitPoints <= 0)
            {
                Die();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        hitPoints = maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Heal(float magnitude)
    {
        HitPoints += magnitude;
    }

    public float Damage(float magnitude)
    {
        float modifiedMagnitude = magnitude - armor;
        LoseHitPoints(modifiedMagnitude);
        return modifiedMagnitude;
    }

    public float LoseHitPoints(float magnitude)
    {
        HitPoints -= magnitude;
        return magnitude;
    }

    public float GetHealthPercentage()
    {
        float healthPercentage;
        healthPercentage = HitPoints / maxHitPoints;
        return healthPercentage;
    }

    public void HealPercentage(float magnitude)
    {
        HitPoints += maxHitPoints * magnitude;
    }

    public void Die()
    {
        Death death = GetComponent<Death>();
        if (death!= null)
        {
            death.Die();
        }
    }

}
