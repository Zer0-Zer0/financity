using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class DungeonPart : MonoBehaviour
{
    [SerializeField] private GameObject[] exits; // References to all the exits of the dungeon part
    [SerializeField] private SpawnableBlocks spawnableBlocks; // Scriptable object containing spawnable dungeon parts
    [SerializeField] private Collider blockSize; // Trigger collider encapsulating the entire DungeonPart
    [SerializeField] private bool isRoot = false;

    private List<DungeonPart> adjacentBlocks = new List<DungeonPart>(); // References to all connected dungeon parts
    private DungeonPart parent; // Parent of this DungeonPart, may be null
    private GameObject exit; // Exit of this DungeonPart, may be null
    private int spawnCount = 0; // Counter to limit spawning
    private static int index = 0;
    private int hardIndex; // The location of the object in relation to the scriptable object
    private List<int> triedIndexes = new List<int>();

    public DungeonPart Parent { get => parent; set => parent = value; }
    public GameObject Exit { get => exit; set => exit = value; }
    public int SpawnCount { get => spawnCount; set => spawnCount = value; }
    public int HardIndex { get => hardIndex; set => hardIndex = value; }

    private void OnEnable()
    {
        if (isRoot)
            Initialize();
    }

    private void Initialize()
    {
        SetIndexOnName();
        ValidateParent();
        CheckForCollisions();
        CheckExitsAndSpawn();
    }

    private void SetIndexOnName()
    {
        gameObject.name = $"{index} - {gameObject.name}";
        index++;
    }

    private void ValidateParent()
    {
        if (Parent == null && !isRoot)
        {
            Debug.Log($"{gameObject.name}: has no parent, destroying");
            StartCoroutine(DestroyDungeonPart());
        }
    }

    private void CheckForCollisions()
    {
        if (blockSize == null) return;

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
        return adjacentBlocks.Any(adjacent => adjacent != null && adjacent.Exit == exit);
    }

    public IEnumerator SpawnRandomDungeonPart(GameObject place)
    {
        yield return null;

        if (SpawnCount >= 10)
        {
            SpawnWall(place);
            Debug.LogWarning("Spawn limit reached.");
        }
        else if (spawnableBlocks != null && spawnableBlocks.dungeonParts.Count > 0)
        {
            SpawnDungeonPart(place);
        }
        else
        {
            Debug.LogWarning("No spawnable dungeon parts available.");
        }
    }

    private void SpawnWall(GameObject place)
    {
        DungeonPart newPart = Instantiate(spawnableBlocks.wall, place.transform.position, place.transform.rotation);
        InitializeNewPart(newPart, place, -1);
    }

    private void SpawnDungeonPart(GameObject place)
    {
        if (place == null)
        {
            Debug.LogError($"{gameObject.name}: No place defined");
            return;
        }

        triedIndexes.Add(HardIndex);
        int randomIndex = GetRandomIndex();

        if (randomIndex == -1)
        {
            SpawnWall(place);
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
        if (triedIndexes.Count >= spawnableBlocks.dungeonParts.Count)
            return -1; // Indicates all options have been tried

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnableBlocks.dungeonParts.Count);
        } while (triedIndexes.Contains(randomIndex));

        return randomIndex;
    }

    private void InitializeNewPart(DungeonPart newPart, GameObject place, int randomIndex)
    {
        newPart.Parent = this; // Set the parent of the new DungeonPart
        newPart.transform.parent = this.transform;
        newPart.Exit = place; // Set the exit of the new DungeonPart
        newPart.HardIndex = randomIndex;
        newPart.SpawnCount = spawnCount + 1;
        newPart.Initialize();

        if (newPart != null)
        {
            adjacentBlocks.Add(newPart);
            triedIndexes.Clear();
            Debug.Log($"Spawned new DungeonPart: {newPart.name} at {place}");
        }
    }

    private IEnumerator DestroyDungeonPart()
    {
        if (Parent != null)
            yield return Parent.SpawnRandomDungeonPart(Exit); // Spawn a new part in the parent
        Destroy(gameObject); // Destroy this DungeonPart
    }

    public override string ToString()
    {
        string parentName = Parent != null ? Parent.gameObject.name : "None";
        string exitsInfo = exits != null ? string.Join(", ", exits.Select(exit => exit.name)) : "No exits";
        string adjacentCount = adjacentBlocks.Count.ToString();

        return $"DungeonPart: {gameObject.name}, Parent: {parentName}, Exits: [{exitsInfo}], Adjacent Count: {adjacentCount}, Spawn Count: {SpawnCount}, Hard Index: {HardIndex}";
    }
}
