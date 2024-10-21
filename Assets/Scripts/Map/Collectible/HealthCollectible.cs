using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : Collectible
{
    [SerializeField] private int healthIncrease;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            other.gameObject.GetComponent<Player>().Heal(healthIncrease);

            // PlayEffect();

            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }
}