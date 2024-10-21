using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCollectible : Collectible
{
    [SerializeField] private int turnIncrease;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Player>().AddExtraTurn(turnIncrease);
        // PlayEffect();
        Destroy(gameObject);
    }
}