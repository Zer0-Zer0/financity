using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class DungeonPart : MonoBehaviour
{
    [SerializeField] GameObject[] exits; // References to all the exits of the dungeon part
    [SerializeField] SpawnableBlocks spawnableBlocks; // Scriptable object containing spawnable dungeon parts
    [SerializeField] Collider blockSize; // Trigger collider encapsulating the entire DungeonPart
    [SerializeField] bool _isRoot = false;

    private List<DungeonPart> _adjacentBlocks = new List<DungeonPart>(); // References to all connected dungeon parts
    private DungeonPart _parent; // Parent of this DungeonPart, may be null
    private GameObject _exit; // Exit of this DungeonPart, may be null
    private int _spawnCount = 0; // Counter to limit spawning
    private static int _index = 0;
    private int _hardIndex; // The location of the object in relation to the scriptable object

    public DungeonPart Parent
    {
        get => _parent;
        set
        {
            _parent = value;
            Init();
        }
    }
    public GameObject Exit { get => _exit; set => _exit = value; }
    public int SpawnCount { get => _spawnCount; set => _spawnCount = value; }
    public int HardIndex { get => _hardIndex; set => _hardIndex = value; }

    private void OnEnable()
    {
        if (_isRoot)
            Init();
    }

    public void Init()
    {
        PutIndexOnName();
        CheckParent();
        CheckForCollisions();
        CheckExitsAndSpawn();
    }

    private void PutIndexOnName()
    {
        gameObject.name = $"{_index} - {gameObject.name}";
        _index++;
    }

    private void CheckParent()
    {
        if (Parent == null && !_isRoot)
        {
            Debug.Log($"{gameObject.name}: has no parent, destroying");
            StartCoroutine(DestroyDungeonPart());
        }
    }

    private void CheckForCollisions()
    {
        Collider[] colliders = Physics.OverlapBox(blockSize.bounds.center, blockSize.bounds.extents, Quaternion.identity);
        Debug.Log($"{gameObject.name}: Detected {colliders.Length} colliders.");

        foreach (Collider collider in colliders)
            HandleCollision(collider);
    }

    private void HandleCollision(Collider collider)
    {
        DungeonPart otherPart = collider.GetComponent<DungeonPart>();
        if (otherPart != null && otherPart != this && otherPart != Parent)
        {
            Debug.LogWarning($"{gameObject.name}: Collision detected with {otherPart.name}. Initiating destruction of this DungeonPart.");
            StartCoroutine(DestroyDungeonPart());
        }
    }

    private void CheckExitsAndSpawn()
    {
        foreach (GameObject exit in exits)
            if (!IsExitAlreadyConnected(exit))
                StartCoroutine(SpawnRandomDungeonPart(exit));
    }

    private bool IsExitAlreadyConnected(GameObject exit)
    {
        return _adjacentBlocks.Any(adjacent => adjacent != null && adjacent.Exit == exit);
    }

    private List<int> _triedIndexes = new List<int>();
    public IEnumerator SpawnRandomDungeonPart(GameObject place)
    {
        yield return null;

        if (SpawnCount >= 10)
            SpawnWall(place);
        else if (spawnableBlocks != null && spawnableBlocks.dungeonParts.Count > 0)
            SpawnDungeonPart(place);
        else
            Debug.LogWarning("No spawnable dungeon parts available.");
    }

    private void SpawnWall(GameObject place)
    {
        DungeonPart newPart = Instantiate(spawnableBlocks.wall, place.transform.position, place.transform.rotation);
        newPart.Parent = this; // Set the parent of the new DungeonPart
        newPart.Exit = place; // Set the exit of the new DungeonPart
        Debug.LogWarning("Spawn limit reached.");
    }

    private void SpawnDungeonPart(GameObject place)
    {
        if (place == null)
        {
            Debug.LogError($"{gameObject.name}: No place defined");
            return;
        }

        _triedIndexes.Add(HardIndex);
        int randomIndex = GetRandomIndex();

        if (randomIndex == -1)
        {
            Instantiate(spawnableBlocks.wall, place.transform.position, place.transform.rotation);
            Debug.Log("Tried every option, wall it is");
        }
        else
        {
            DungeonPart newPart = Instantiate(spawnableBlocks.dungeonParts[randomIndex], place.transform.position, place.transform.rotation);
            InitializeNewPart(newPart, place, randomIndex);
        }
    }

    private int GetRandomIndex()
    {
        List<int> indexRange = Range(spawnableBlocks.dungeonParts);
        bool containsAll = indexRange.All(i => _triedIndexes.Contains(i));

        if (containsAll) return -1; // Indicates all options have been tried

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnableBlocks.dungeonParts.Count - 1);
        } while (_triedIndexes.Contains(randomIndex));

        return randomIndex;
    }

    private void InitializeNewPart(DungeonPart newPart, GameObject place, int randomIndex)
    {
        newPart.Parent = this; // Set the parent of the new DungeonPart
        newPart.Exit = place; // Set the exit of the new DungeonPart
        newPart.HardIndex = randomIndex;
        newPart.SpawnCount = _spawnCount + 1;

        if (newPart != null)
        {
            _adjacentBlocks.Add(newPart);
            _triedIndexes.Clear();
            Debug.Log($"Spawned new DungeonPart: {newPart.name} at {place}");
        }
    }

    private IEnumerator DestroyDungeonPart()
    {
        if (Parent != null)
            yield return Parent.SpawnRandomDungeonPart(Exit); // Spawn a new part in the parent
        Destroy(gameObject); // Destroy this DungeonPart
    }

    private List<int> Range(List<DungeonPart> list)
    {
        return Enumerable.Range(0, list.Count).ToList();
    }
}

// ScriptableObject to hold spawnable dungeon parts
[CreateAssetMenu(fileName = "SpawnableBlocks", menuName = "ScriptableObjects/SpawnableBlocks")]
public class SpawnableBlocks : ScriptableObject
{
    public List<DungeonPart> dungeonParts; // Array of spawnable dungeon parts
    public DungeonPart wall; // A wall to cap the ends
}