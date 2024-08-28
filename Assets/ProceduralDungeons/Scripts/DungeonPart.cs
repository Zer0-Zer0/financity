using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

/// <summary>
/// Represents a part of a dungeon, which can spawn other dungeon parts and manage connections between them.
/// </summary>
[System.Serializable]
public class DungeonPart : MonoBehaviour
{
    [SerializeField] private GameObject[] exits; // References to all the exits of the dungeon part
    [SerializeField] private SpawnableBlocks spawnableBlocks; // Scriptable object containing spawnable dungeon parts
    [SerializeField] private Collider blockSize; // Trigger collider encapsulating the entire DungeonPart
    [SerializeField] private bool isRoot = false; // Indicates if this is the root dungeon part

    private List<DungeonPart> adjacentBlocks = new List<DungeonPart>(); // References to all connected dungeon parts
    private DungeonPart parent; // Parent of this DungeonPart, may be null
    private GameObject exit; // Exit of this DungeonPart, may be null
    private int spawnCount = 0; // Counter to limit spawning
    private int hardIndex; // The location of the object in relation to the scriptable object
    private List<int> triedIndexes = new List<int>(); // List of indexes that have been tried for spawning
    private bool isBeingDestroyed = false;
    private bool isSpawning = false; // Flag to manage coroutine execution

    public DungeonPart Parent { get => parent; set => parent = value; }
    public GameObject Exit { get => exit; set => exit = value; }
    public int SpawnCount { get => spawnCount; set => spawnCount = value; }
    public int HardIndex { get => hardIndex; set => hardIndex = value; }
    public bool IsBeingDestroyed { get => isBeingDestroyed; private set => isBeingDestroyed = value; }

    private void OnEnable()
    {
        if (IsBeingDestroyed) return;
        if (isRoot) StartCoroutine(Initialize());
    }

    public IEnumerator Initialize()
    {
        if (IsBeingDestroyed) yield break;

        SetIndexOnName();
        yield return ValidateParent();
        yield return CheckForCollisions();
        yield return CheckExitsAndSpawn();
    }

    private void SetIndexOnName()
    {
        if (IsBeingDestroyed) return;
        gameObject.name = $"{gameObject.name} - {System.Guid.NewGuid()}";
    }

    private IEnumerator ValidateParent()
    {
        if (IsBeingDestroyed) yield break;

        if (Parent == null && !isRoot)
        {
            Debug.Log($"{gameObject.name}: has no parent, destroying");
            yield return DestroyDungeonPart();
        }
    }

    private IEnumerator CheckForCollisions()
    {
        if (IsBeingDestroyed || blockSize == null) yield break;

        Collider[] colliders = Physics.OverlapBox(blockSize.bounds.center, blockSize.bounds.extents, Quaternion.identity);
        foreach (Collider collider in colliders)
            yield return HandleCollision(collider);
    }

    private IEnumerator HandleCollision(Collider collider)
    {
        if (IsBeingDestroyed) yield break;
        DungeonPart otherPart = collider.GetComponent<DungeonPart>();
        if (otherPart != null && otherPart != this && otherPart != Parent)
        if (otherPart.IsBeingDestroyed) yield break;
        {
            Debug.LogWarning($"{gameObject.name}: Collision detected with {otherPart.name}. Initiating destruction of this DungeonPart.");
            yield return DestroyDungeonPart();
        }
    }

    private IEnumerator CheckExitsAndSpawn()
    {
        if (IsBeingDestroyed) yield break;

        foreach (GameObject exit in exits)
        {
            if (exit == null)
            {
                Debug.LogWarning($"{gameObject.name}: Exit is null, skipping.");
                continue;
            }
            if (!IsExitAlreadyConnected(exit))
                yield return SpawnRandomDungeonPart(exit);
        }
    }

    private bool IsExitAlreadyConnected(GameObject exit)
    {
        return adjacentBlocks.Any(adjacent => adjacent != null && adjacent.Exit == exit);
    }

    private IEnumerator SpawnRandomDungeonPart(GameObject place)
    {
        if (IsBeingDestroyed || isSpawning) yield break;
        isSpawning = true;

        try
        {
            if (SpawnCount >= 12)
            {
                yield return SpawnWall(place);
                Debug.Log("Spawn limit reached.");
            }
            else if (spawnableBlocks != null && spawnableBlocks.dungeonParts.Count > 0)
                yield return SpawnDungeonPart(place);
            else
                Debug.LogWarning("No spawnable dungeon parts available.");
        }
        finally
        {
            isSpawning = false;
        }
    }

    private IEnumerator SpawnWall(GameObject place)
    {
        if (IsBeingDestroyed) yield break;

        DungeonPart newPart = Instantiate(spawnableBlocks.wall, place.transform.position, place.transform.rotation);
        yield return InitializeNewPart(newPart, place, -1);
    }

    public IEnumerator SpawnDungeonPart(GameObject place)
    {
        if (IsBeingDestroyed) yield break;

        if (place == null)
        {
            Debug.LogError($"{gameObject.name}: No place defined");
            yield break;
        }
        yield return null;

        if (!isRoot && !triedIndexes.Contains(HardIndex)) triedIndexes.Add(HardIndex);
        DungeonPart newPart;
        int randomIndex = -1;

        do
        {
            if (!triedIndexes.Contains(randomIndex))
                triedIndexes.Add(randomIndex);
            randomIndex = GetRandomIndex();
            if (randomIndex == -1)
            {
                yield return SpawnWall(place);
                Debug.Log("Tried every option, wall it is");
                yield break;
            }

            newPart = Instantiate(spawnableBlocks.dungeonParts[randomIndex], place.transform.position, place.transform.rotation);
            yield return InitializeNewPart(newPart, place, randomIndex);
        } while (newPart == null);
        if (newPart != null) adjacentBlocks.Add(newPart);
    }

    private int GetRandomIndex()
    {
        if (triedIndexes.Count >= spawnableBlocks.dungeonParts.Count)
            return -1; // Indicates all options have been tried

        int randomIndex;
        do
            randomIndex = Random.Range(0, spawnableBlocks.dungeonParts.Count);
        while (triedIndexes.Contains(randomIndex));

        return randomIndex;
    }

    private IEnumerator InitializeNewPart(DungeonPart newPart, GameObject place, int randomIndex)
    {
        newPart.Parent = this;
        newPart.transform.parent = transform;
        newPart.Exit = place;
        newPart.HardIndex = randomIndex;
        newPart.SpawnCount = SpawnCount + 1;
        StartCoroutine(newPart.Initialize());
        yield return null;
    }

    private IEnumerator DestroyDungeonPart()
    {
        if (IsBeingDestroyed) yield break;
        IsBeingDestroyed = true;

        if (Parent != null)
            yield return Parent.SpawnDungeonPart(Exit);

        triedIndexes.Clear();
        Destroy(gameObject);
    }

    public override string ToString()
    {
        string parentName = Parent != null ? Parent.gameObject.name : "None";
        string exitsInfo = exits != null ? string.Join(", ", exits.Select(exit => exit.name)) : "No exits";
        string adjacentCount = adjacentBlocks.Count.ToString();

        return $"DungeonPart: {gameObject.name}, Parent: {parentName}, Exits: [{exitsInfo}], Adjacent Count: {adjacentCount}, Spawn Count: {SpawnCount}, Hard Index: {HardIndex}";
    }
}
