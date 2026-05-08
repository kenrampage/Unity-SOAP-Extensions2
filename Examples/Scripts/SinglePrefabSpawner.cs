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
    [Tooltip("If true, the spawned instance will be parented to the spawn location (required for UI prefabs).")]
    [SerializeField] private bool _reparentToSpawnLocation = false;

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
            //Debug.LogWarning("Spawn location is not assigned.", this);
            return;
        }

        if (prefab == null)
        {
            //Debug.LogWarning("Prefab is null.", this);
            DestroyCurrentPrefab();
            return;
        }

        DestroyCurrentPrefab();

        //Debug.Log($"Spawning prefab: {prefab.name}", this);
        if (_reparentToSpawnLocation)
        {
            currentInstance = Instantiate(prefab, spawnLocation);
            currentInstance.transform.localPosition = Vector3.zero;
            currentInstance.transform.localRotation = Quaternion.identity;
        }
        else
        {
            currentInstance = Instantiate(prefab, spawnLocation.position, spawnLocation.rotation);
        }
    }
}
}