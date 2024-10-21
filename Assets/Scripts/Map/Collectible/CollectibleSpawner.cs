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
        InvokeRepeating("CheckAndSpawnCollectibles", 0.5f, 5f);
    }

    private void CheckAndSpawnCollectibles()
    {
        int currentCollectibles = FindObjectsOfType<Collectible>().Length;

        Debug.Log(currentCollectibles);

        Debug.Log(_initialCollectiblesValue);

        if (currentCollectibles <= ((_initialCollectiblesValue / 10))) // 10% of the initial value
        {
            SpawnCollectibles();
        }
    }

    private void SpawnCollectibles()
    {
        int tileLayer = LayerMask.NameToLayer("Tile");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        float boardOffsetX = -(columns / 2f) * tileSize + tileSize / 2f;
        float boardOffsetZ = -(rows / 2f) * tileSize + tileSize / 2f;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == tileLayer)
            {
                int row = Mathf.RoundToInt((obj.transform.position.z - boardOffsetZ) / tileSize);
                int column = Mathf.RoundToInt((obj.transform.position.x - boardOffsetX) / tileSize);

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
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.5f);
        foreach (var hitCollider in hitColliders)
        {
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