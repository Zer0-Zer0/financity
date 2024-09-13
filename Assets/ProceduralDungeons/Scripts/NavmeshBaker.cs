using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

/// <summary>
/// Singleton class responsible for baking the NavMesh in the scene.
/// </summary>
public class NavmeshBaker : MonoBehaviour
{
    private static NavmeshBaker _instance;

    /// <summary>
    /// Gets the singleton instance of the NavmeshBaker.
    /// </summary>
    public static NavmeshBaker Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<NavmeshBaker>();
            return _instance;
        }
    }

    [SerializeField] NavMeshSurface navMeshSurface;
    [SerializeField] GameEvent OnNavmeshBaked;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError($"{gameObject.name}: two or more instances of NavmeshBaker found");
            return;
        }

        _instance = this;
    }

    private Coroutine _bakeCoroutine;

    public void BakeAfterTime(float time = 3f)
    {
        if (_bakeCoroutine != null)
            StopCoroutine(_bakeCoroutine);

        _bakeCoroutine = StartCoroutine(BakeNavMeshAfterDelay(time));
    }

    private IEnumerator BakeNavMeshAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        BakeNavMesh();
        OnNavmeshBaked.Raise(this, null);
    }

    private void BakeNavMesh()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("Baking NavMesh...");
        }
        else
        {
            Debug.LogError("NavMeshSurface is not assigned.");
        }
    }
}
