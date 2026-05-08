using UnityEngine;

namespace KenRampage.Addons.SOAP.Demo
{
    /// <summary>
    /// Spawns a single instance of a prefab at a time, destroying any existing instance before spawning a new one.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Extensions/Single Prefab Spawner")]
public class SinglePrefabSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnLocation;

    private GameObject currentInstance;

    public void DestroyCurrentPrefab()
    {
        if (currentInstance == null)
        {
            return;
        }

        Destroy(currentInstance);
        currentInstance = null;
    }

    public void SpawnPrefab(GameObject prefab)
    {
        if (spawnLocation == null)
        {
            Debug.LogWarning("Spawn location is not assigned.", this);
            return;
        }

        if (prefab == null)
        {
            Debug.LogWarning("Prefab is null.", this);
            return;
        }

        DestroyCurrentPrefab();

        currentInstance = Instantiate(prefab, spawnLocation.position, spawnLocation.rotation);
    }
}
}