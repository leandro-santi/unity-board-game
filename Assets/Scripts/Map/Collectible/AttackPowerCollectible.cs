using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPowerCollectible : Collectible
{
    [SerializeField] private int powerIncrease;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.GainAttackPower(powerIncrease);

            // PlayEffect();

            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }
}