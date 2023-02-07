using UnityEngine;

[System.Serializable]
public class PrefabFactor
{
    public GameObject prefab;
    public int[] xFactors;
    public int[] zFactors;
    public int maxInstances;
}

public class MapGenerator : MonoBehaviour
{
    public PrefabFactor[] prefabFactors;
    public float tileSize = 1f;
    public float spacing = 1f;
    public int prefabsInX = 10;
    public int prefabsInZ = 10;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        float halfMapWidth = (prefabsInX / 2) * (tileSize + spacing);
        float halfMapHeight = (prefabsInZ / 2) * (tileSize + spacing);

        for (int x = 0; x < prefabsInX; x++)
        {
            for (int z = 0; z < prefabsInZ; z++)
            {
                Vector3 tilePosition = new Vector3(x * (tileSize + spacing) - halfMapWidth + transform.position.x, 
                                                   0, 
                                                   z * (tileSize + spacing) - halfMapHeight + transform.position.z);
                int prefabCount = 0;
                GameObject selectedPrefab = null;
                for (int i = 0; i < prefabFactors.Length; i++)
                {
                    for (int j = 0; j < prefabFactors[i].xFactors.Length; j++)
                    {
                        if (x == prefabFactors[i].xFactors[j] && z == prefabFactors[i].zFactors[j] && prefabFactors[i].maxInstances > 0)
                        {
                            prefabCount++;
                            if (prefabCount == 1)
                            {
                                selectedPrefab = prefabFactors[i].prefab;
                            }
                            else
                            {
                                if (Random.Range(0, 2) == 1)
                                {
                                    selectedPrefab = prefabFactors[i].prefab;
                                }
                            }
                        }
                    }
                }
                if (selectedPrefab != null)
                {
                    GameObject tile = Instantiate(selectedPrefab, tilePosition, Quaternion.identity);
                    tile.transform.parent = transform;
                    for (int i = 0; i < prefabFactors.Length; i++)
                    {
                        if (prefabFactors[i].prefab == selectedPrefab)
                        {
                            prefabFactors[i].maxInstances--;
                        }
                    }
                }
            }
        }
    }
}

