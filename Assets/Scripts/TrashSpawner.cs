using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public static TrashSpawner Instance { get; private set; }

    public GameObject[] TrashPrefabs;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        SpawnRandomTrash();
    }

    public void SpawnRandomTrash()
    {
        SpawnTrash(Random.Range(0, TrashPrefabs.Length));
    }

    public void SpawnTrash(int type)
    {
        Instantiate(TrashPrefabs[type], transform);
    }
}
