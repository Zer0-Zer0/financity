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

    private DungeonPart[] _adjacentBlocks; // References to all connected dungeon parts
    private DungeonPart _parent; // Parent of this DungeonPart, may be null
    private GameObject _exit; // Exit of this DungeonPart, may be null

    public DungeonPart Parent { get => _parent; set => _parent = value; }
    public GameObject Exit { get => _exit; set => _exit = value; }

    private void OnEnable()
    {
        CheckForCollisions();
        CheckExitsAndSpawn();
    }

    private void CheckForCollisions()
    {
        // Check if this DungeonPart is colliding with other DungeonParts
        Collider[] colliders = Physics.OverlapBox(blockSize.bounds.center, blockSize.bounds.extents, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            DungeonPart otherPart = collider.GetComponent<DungeonPart>();
            if (otherPart != null && otherPart != this)
            {
                Debug.LogWarning($"Collision detected with {otherPart.name}. Destroying this DungeonPart.");
                Destroy(gameObject); // Destroy this DungeonPart
                Parent?.SpawnRandomDungeonPart(Exit); // Notify parent to create another part
                return;
            }
        }
    }

    private void CheckExitsAndSpawn()
    {
        foreach (GameObject exit in exits)
        {
            // Check if the exit has an adjacent block connected
            bool hasAdjacentBlock = false;
            if (_adjacentBlocks != null)
            {
                foreach (DungeonPart adjacent in _adjacentBlocks)
                {
                    if (adjacent != null && adjacent.Exit == exit)
                    {
                        hasAdjacentBlock = true;
                        break;
                    }
                }
            }

            if (!hasAdjacentBlock)
            {
                // Instantiate a random DungeonPart from the SpawnableBlocks list
                SpawnRandomDungeonPart(exit);
            }
        }
    }

    public void SpawnRandomDungeonPart(GameObject place)
    {
        if (spawnableBlocks != null && spawnableBlocks.dungeonParts.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnableBlocks.dungeonParts.Length);
            DungeonPart newPart = Instantiate(spawnableBlocks.dungeonParts[randomIndex], place.transform.position, Quaternion.identity);
            newPart.Parent = this; // Set the parent of the new DungeonPart
            newPart.Exit = place; // Set the exit of the new DungeonPart
            Debug.Log($"Spawned new DungeonPart: {newPart.name} at {place}");
        }
        else
        {
            Debug.LogWarning("No spawnable dungeon parts available.");
        }
    }
}

// ScriptableObject to hold spawnable dungeon parts
[CreateAssetMenu(fileName = "SpawnableBlocks", menuName = "ScriptableObjects/SpawnableBlocks")]
public class SpawnableBlocks : ScriptableObject
{
    public DungeonPart[] dungeonParts; // Array of spawnable dungeon parts
}