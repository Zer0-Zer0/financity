using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonPart : MonoBehaviour
{
    [SerializeField]
    private GameObject[] exits; // References to all the exits of the dungeon part
    [SerializeField]
    private SpawnableBlocks spawnableBlocks; // Scriptable object containing spawnable dungeon parts
    [SerializeField]
    private Collider blockSize; // Trigger collider encapsulating the entire DungeonPart

    private List<DungeonPart> _adjacentBlocks = new List<DungeonPart>(); // References to all connected dungeon parts
    private DungeonPart _parent; // Parent of this DungeonPart, may be null
    private GameObject _exit; // Exit of this DungeonPart, may be null
    private int _spawnCount = 0; // Counter to limit spawning

    public DungeonPart Parent { get => _parent; set => _parent = value; }
    public GameObject Exit { get => _exit; set => _exit = value; }
    public int SpawnCount { get => _spawnCount; set => _spawnCount = value; }

    private void OnEnable()
    {
        CheckForCollisions();
        CheckExitsAndSpawn();
    }

    private void CheckForCollisions()
    {
        Collider[] colliders = Physics.OverlapBox(blockSize.bounds.center, blockSize.bounds.extents, Quaternion.identity);
        Debug.Log($"Detected {colliders.Length} colliders.");
        foreach (Collider collider in colliders)
        {
            DungeonPart otherPart = collider.GetComponent<DungeonPart>();
            if (otherPart != null && otherPart != this)
            {
                Debug.LogWarning($"Collision detected with {otherPart.name}. Initiating destruction of this DungeonPart.");
                StartCoroutine(DestroyDungeonPart());
                return;
            }
        }
        Debug.Log("No collisions detected.");
    }

    private void CheckExitsAndSpawn()
    {
        foreach (GameObject exit in exits)
        {
            if (_adjacentBlocks != null)
                foreach (DungeonPart adjacent in _adjacentBlocks)
                    if (adjacent != null && adjacent.Exit == exit)
                        continue;

            StartCoroutine(SpawnRandomDungeonPart(exit));
        }
    }

    public IEnumerator SpawnRandomDungeonPart(GameObject place)
    {
        yield return null;
        if (SpawnCount >= 10)
        {
            Debug.LogWarning("Spawn limit reached.");
        }
        else
        {
            if (spawnableBlocks != null && spawnableBlocks.dungeonParts.Length > 0)
            {
                int randomIndex = Random.Range(0, spawnableBlocks.dungeonParts.Length);
                DungeonPart newPart = Instantiate(spawnableBlocks.dungeonParts[randomIndex], place.transform.position, place.transform.rotation);
                newPart.Parent = this; // Set the parent of the new DungeonPart
                newPart.Exit = place; // Set the exit of the new DungeonPart
                newPart.SpawnCount = _spawnCount + 1;
                if (newPart != null)
                    _adjacentBlocks.Add(newPart);
                Debug.Log($"Spawned new DungeonPart: {newPart.name} at {place}");
            }
            else
                Debug.LogWarning("No spawnable dungeon parts available.");
        }
    }

    private IEnumerator DestroyDungeonPart()
    {
        if (Parent != null)
            yield return Parent.SpawnRandomDungeonPart(Exit); // Spawn a new part in the parent
        Destroy(this); // Destroy this DungeonPart
    }
}

// ScriptableObject to hold spawnable dungeon parts
[CreateAssetMenu(fileName = "SpawnableBlocks", menuName = "ScriptableObjects/SpawnableBlocks")]
public class SpawnableBlocks : ScriptableObject
{
    public DungeonPart[] dungeonParts; // Array of spawnable dungeon parts
}
