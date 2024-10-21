using UnityEngine;

public class TurnCollectible : Collectible
{
    [SerializeField] private int turnIncrease;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            other.gameObject.GetComponent<Player>().AddExtraTurn(turnIncrease);

            // PlayEffect(); // PlayEffect(); -> Future effects implementation

            gameObject.SetActive(false);
            Destroy(gameObject, 0.5f);
        }
    }
}