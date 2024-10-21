using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerCollectible : Collectible
{
    [SerializeField] private int powerIncrease;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Player>().GainAttackPower(powerIncrease);
        // PlayEffect();
        Destroy(gameObject);
    }
}