using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// ScriptableObject to hold spawnable dungeon parts
[CreateAssetMenu(fileName = "SpawnableBlocks_", menuName = "ScriptableObjects/SpawnableBlocks")]
public class SpawnableBlocks : ScriptableObject
{
    public List<DungeonPart> dungeonParts; // Array of spawnable dungeon parts
    public DungeonPart wall; // A wall to cap the ends
}