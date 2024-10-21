using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : Collectible
{
    [SerializeField] private int healthIncrease;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Player>().Heal(healthIncrease);
        // PlayEffect();
        Destroy(gameObject);
    }
}