using UnityEngine;

/*
 * ProceduralBoxSpawner
 * --------------------
 * Randomly scatters box structures across a surface.
 * Structures can be either grid stacks or pyramids.
 */

public class ProceduralBoxSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject boxPrefab;

    [Header("Surface Area")]
    public Collider surfaceCollider;

    [Header("Scatter Settings")]
    public int structureCount = 5;

    [Header("Structure Height Limits")]
    public int minHeight = 2;
    public int maxHeight = 6;

    void Start()
    {
        ScatterStructures();
    }

    void ScatterStructures()
    {
        if (boxPrefab == null || surfaceCollider == null)
        {
            Debug.LogError("Missing prefab or surface collider.");
            return;
        }

        BoxCollider boxCol = boxPrefab.GetComponent<BoxCollider>();
        Vector3 boxSize = boxCol.size;

        Bounds bounds = surfaceCollider.bounds;

        for (int i = 0; i < structureCount; i++)
        {
            bool spawnPyramid = Random.value > 0.5f;

            int height = Random.Range(minHeight, maxHeight + 1);

            int maxBase = Mathf.FloorToInt(bounds.size.x / boxSize.x);

            if (maxBase % 2 == 0)
                maxBase--;

            int baseWidth = spawnPyramid
                ? Random.Range(1, maxBase + 1) | 1
                : Random.Range(1, maxBase + 1);

            Vector3 startPos = bounds.center;

            startPos.x += Random.Range(-bounds.extents.x * 0.4f, bounds.extents.x * 0.4f);
            startPos.z += Random.Range(-bounds.extents.z, bounds.extents.z);
            startPos.y = bounds.max.y + boxSize.y * 0.5f;

            SpawnStructure(startPos, baseWidth, height, spawnPyramid, boxSize);
        }
    }

    void SpawnStructure(Vector3 startPos, int baseWidth, int height, bool pyramid, Vector3 boxSize)
    {
        for (int y = 0; y < height; y++)
        {
            int rowWidth = pyramid ? baseWidth - (y * 2) : baseWidth;

            if (rowWidth <= 0)
                break;

            float offset = -(rowWidth - 1) * boxSize.x * 0.5f;

            for (int x = 0; x < rowWidth; x++)
            {
                Vector3 spawnPos = startPos;

                spawnPos.x += offset + x * boxSize.x;
                spawnPos.y += y * boxSize.y;

                Instantiate(boxPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
