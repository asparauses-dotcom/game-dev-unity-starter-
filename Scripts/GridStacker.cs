using UnityEngine;

/*
 * GridStacker
 * -----------
 * Spawns boxes in a simple grid formation.
 * The size of the grid is controlled by width and height.
 */

public class GridStacker : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject boxPrefab;

    [Header("Grid Size")]
    public int width = 5;
    public int height = 5;

    void Start()
    {
        if (boxPrefab == null)
        {
            Debug.LogError("Box Prefab is not assigned.");
            return;
        }

        Vector3 startPos = transform.position;

        BoxCollider col = boxPrefab.GetComponent<BoxCollider>();
        if (col == null)
        {
            Debug.LogError("Box Prefab needs a BoxCollider.");
            return;
        }

        Vector3 size = col.size;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 spawnPos = startPos + new Vector3(
                    x * size.x,
                    y * size.y,
                    0
                );

                Instantiate(boxPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
