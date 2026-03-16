using UnityEngine;

/*
 * PyramidSpawner
 * --------------
 * Creates a pyramid structure made of boxes.
 * Each level reduces the number of boxes by two.
 */

public class PyramidSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject boxPrefab;

    [Header("Pyramid Settings")]
    public int baseWidth = 7;   // Should ideally be odd
    public int height = 4;

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
            int rowWidth = baseWidth - (y * 2);

            if (rowWidth <= 0)
                break;

            int removed = baseWidth - rowWidth;
            float xOffset = (removed * size.x) / 2f;

            for (int x = 0; x < rowWidth; x++)
            {
                Vector3 spawnPos = startPos + new Vector3(
                    (x * size.x) + xOffset,
                    y * size.y,
                    0
                );

                Instantiate(boxPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
