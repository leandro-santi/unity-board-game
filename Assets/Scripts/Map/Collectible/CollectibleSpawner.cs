using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] collectiblePrefabs;
    [SerializeField] private int rows = 16;
    [SerializeField] private int columns = 16;
    [SerializeField] private float tileSize = 1f;
    private int _counterCollectibles;
    private int _initialCollectiblesValue = 0;

    private void Start()
    {
        // This invokes repeating aims to handler the spawn of the collectibles
        // and also the check of 10% collectibles remaining
        InvokeRepeating("CheckAndSpawnCollectibles", 0.5f, 5f);
    }

    private void CheckAndSpawnCollectibles()
    {
        int currentCollectibles = FindObjectsOfType<Collectible>().Length;

        if (currentCollectibles <= ((_initialCollectiblesValue / 10))) // 10% of the initial value
        {
            SpawnCollectibles();
        }
    }

    private void SpawnCollectibles()
    {
        int tileLayer = LayerMask.NameToLayer("Tile");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == tileLayer)
            {
                if (!IsCollectiblePresent(obj.transform.position))
                {
                    GameObject selectedPrefab = GetRandomCollectiblePrefab();
                    Vector3 spawnPosition = obj.transform.position + new Vector3(0, 0.75f, 0);

                    Instantiate(selectedPrefab, spawnPosition, Quaternion.identity, obj.transform);

                    _initialCollectiblesValue++;
                }
            }
        }
    }

    private bool IsCollectiblePresent(Vector3 position)
    {
        // This aims controll the possibility to spawn in a occupied place
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f);
        foreach (var hitCollider in hitColliders)
        {
            // If it has something colliding as a Collectible or Player
            if (hitCollider.GetComponent<Collectible>() != null || hitCollider.GetComponent<Player>() != null)
            {
                return true;
            }
        }

        return false;
    }

    private GameObject GetRandomCollectiblePrefab()
    {
        int randomIndex = Random.Range(0, collectiblePrefabs.Length);
        return collectiblePrefabs[randomIndex];
    }
}