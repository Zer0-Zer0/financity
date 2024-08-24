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

    /// <summary>
    /// Gets or sets the parent DungeonPart.
    /// </summary>
    public DungeonPart Parent { get => parent; set => parent = value; }

    /// <summary>
    /// Gets or sets the exit GameObject of this DungeonPart.
    /// </summary>
    public GameObject Exit { get => exit; set => exit = value; }

    /// <summary>
    /// Gets or sets the spawn count of this DungeonPart.
    /// </summary>
    public int SpawnCount { get => spawnCount; set => spawnCount = value; }

    /// <summary>
    /// Gets or sets the hard index of this DungeonPart.
    /// </summary>
    public int HardIndex { get => hardIndex; set => hardIndex = value; }

    private void OnEnable()
    {
        if (isBeingDestroyed) return;
        if (isRoot) Initialize();
    }

    /// <summary>
    /// Initializes the DungeonPart by setting its index, validating its parent, checking for collisions, and checking exits to spawn new parts.
    /// </summary>
    private void Initialize()
    {
        SetIndexOnName();
        ValidateParent();
        CheckForCollisions();
        CheckExitsAndSpawn();
    }

    /// <summary>
    /// Sets the index of the DungeonPart in its name for identification.
    /// </summary>
    private void SetIndexOnName()
    {
        gameObject.name = $"{gameObject.name} - {System.Guid.NewGuid()}";
    }

    /// <summary>
    /// Validates that the DungeonPart has a parent if it is not a root part.
    /// </summary>
    private void ValidateParent()
    {
        if (Parent == null && !isRoot)
        {
            Debug.Log($"{gameObject.name}: has no parent, destroying");
            StartCoroutine(DestroyDungeonPart());
        }
    }

    /// <summary>
    /// Checks for collisions with other colliders in the vicinity of the DungeonPart.
    /// </summary>
    private void CheckForCollisions()
    {
        if (isBeingDestroyed || blockSize == null) return;

        Collider[] colliders = Physics.OverlapBox(blockSize.bounds.center, blockSize.bounds.extents, Quaternion.identity);
        Debug.Log($"{gameObject.name}: Detected {colliders.Length} colliders.");

        foreach (Collider collider in colliders)
            HandleCollision(collider);
    }

    /// <summary>
    /// Handles a collision with another collider, potentially destroying this DungeonPart if it collides with another DungeonPart.
    /// </summary>
    /// <param name="collider">The collider that this DungeonPart collided with.</param>
    private void HandleCollision(Collider collider)
    {
        if (isBeingDestroyed) return;
        DungeonPart otherPart = collider.GetComponent<DungeonPart>();
        if (otherPart != null && otherPart != this && otherPart != Parent)
        {
            Debug.LogWarning($"{gameObject.name}: Collision detected with {otherPart.name}. Initiating destruction of this DungeonPart.");
            StartCoroutine(DestroyDungeonPart());
        }
    }

    /// <summary>
    /// Checks the exits of this DungeonPart and spawns new parts if they are not already connected.
    /// </summary>
    private void CheckExitsAndSpawn()
    {
        if (isBeingDestroyed) return;

        foreach (GameObject exit in exits)
        {
            if (exit == null)
            {
                Debug.LogWarning($"{gameObject.name}: ERROR Exit is null, skipping.");
                break;
            }
            if (!IsExitAlreadyConnected(exit))
                StartCoroutine(SpawnRandomDungeonPart(exit));
        }
    }

    /// <summary>
    /// Checks if the specified exit is already connected to an adjacent DungeonPart.
    /// </summary>
    /// <param name="exit">The exit GameObject to check.</param>
    /// <returns>True if the exit is already connected; otherwise, false.</returns>
    private bool IsExitAlreadyConnected(GameObject exit)
    {
        return adjacentBlocks.Any(adjacent => adjacent != null && adjacent.Exit == exit);
    }

    /// <summary>
    /// Spawns a random dungeon part at the specified location.
    /// </summary>
    /// <param name="place">The location where the new dungeon part should be spawned.</param>
    /// <returns>An enumerator for coroutine execution.</returns>
    public IEnumerator SpawnRandomDungeonPart(GameObject place)
    {
        if (isBeingDestroyed) yield break;
        yield return null;

        if (SpawnCount >= 4)
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

    /// <summary>
    /// Spawns a wall at the specified location.
    /// </summary>
    /// <param name="place">The location where the wall should be spawned.</param>
    private void SpawnWall(GameObject place)
    {
        if (isBeingDestroyed) return;
        DungeonPart newPart = Instantiate(spawnableBlocks.wall, place.transform.position, place.transform.rotation);
        InitializeNewPart(newPart, place, -1);
    }

    /// <summary>
    /// Spawns a dungeon part at the specified location.
    /// </summary>
    /// <param name="place">The location where the dungeon part should be spawned.</param>
    private void SpawnDungeonPart(GameObject place)
    {
        if (isBeingDestroyed) return;
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
            return; // Exit the method after spawning the wall
        }
        else
        {
            DungeonPart newPart = Instantiate(spawnableBlocks.dungeonParts[randomIndex], place.transform.position, place.transform.rotation);
            InitializeNewPart(newPart, place, randomIndex);
        }
    }

    /// <summary>
    /// Gets a random index for spawning a dungeon part that has not been tried yet.
    /// </summary>
    /// <returns>A random index or -1 if all options have been tried.</returns>
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

    /// <summary>
    /// Initializes a newly spawned DungeonPart.
    /// </summary>
    /// <param name="newPart">The new DungeonPart to initialize.</param>
    /// <param name="place">The location where the new DungeonPart was spawned.</param>
    /// <param name="randomIndex">The index of the spawned DungeonPart in the spawnable blocks.</param>
    private void InitializeNewPart(DungeonPart newPart, GameObject place, int randomIndex)
    {
        if (isBeingDestroyed) return;
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

    /// <summary>
    /// Destroys this DungeonPart and potentially spawns a new part in its parent.
    /// </summary>
    /// <returns>An enumerator for coroutine execution.</returns>
    private IEnumerator DestroyDungeonPart()
    {
        if (isBeingDestroyed) yield break;
        isBeingDestroyed = true;

        if (Parent != null)
            yield return Parent.SpawnRandomDungeonPart(Exit);
        triedIndexes.Clear();
        Destroy(gameObject);
    }

    /// <summary>
    /// Returns a string representation of the DungeonPart, including its name, parent, exits, adjacent count, spawn count, and hard index.
    /// </summary>
    /// <returns>A string representation of the DungeonPart.</returns>
    public override string ToString()
    {
        string parentName = Parent != null ? Parent.gameObject.name : "None";
        string exitsInfo = exits != null ? string.Join(", ", exits.Select(exit => exit.name)) : "No exits";
        string adjacentCount = adjacentBlocks.Count.ToString();

        return $"DungeonPart: {gameObject.name}, Parent: {parentName}, Exits: [{exitsInfo}], Adjacent Count: {adjacentCount}, Spawn Count: {SpawnCount}, Hard Index: {HardIndex}";
    }
}