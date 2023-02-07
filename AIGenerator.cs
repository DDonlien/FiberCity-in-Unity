using UnityEngine;

public class AIGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct AIType
    {
        public GameObject prefab;
        public int minCount;
        public int maxCount;
        public float factor;
    }

    public AIType[] AITypes;
    public Vector3 range = new Vector3(10, 10, 10);
    public Vector2 exclusionZoneSize = new Vector2(1, 1);
    public Vector3 exclusionZoneCenter = Vector3.zero;

    private void Start()
    {
        foreach (AIType AI in AITypes)
        {
            int count = Mathf.RoundToInt(AI.factor + RandomNormalDistribution() * (AI.maxCount - AI.minCount) / 2);
            count = Mathf.Clamp(count, AI.minCount, AI.maxCount);
            for (int i = 0; i < count; i++)
            {
                Vector3 randomPosition = transform.position + new Vector3(
                    Random.Range(-range.x, range.x),
                    Random.Range(-range.y, range.y),
                    Random.Range(-range.z, range.z)
                );
                if (!(randomPosition.x > exclusionZoneCenter.x - exclusionZoneSize.x / 2 &&
                      randomPosition.x < exclusionZoneCenter.x + exclusionZoneSize.x / 2 &&
                      randomPosition.z > exclusionZoneCenter.z - exclusionZoneSize.y / 2 &&
                      randomPosition.z < exclusionZoneCenter.z + exclusionZoneSize.y / 2))
                {
                    GameObject instance = Instantiate(AI.prefab, randomPosition, Quaternion.identity);
                }
            }
        }
    }

    private float RandomNormalDistribution()
    {
        float u1 = Random.Range(0f, 1f);
        float u2 = Random.Range(0f, 1f);
        float z0 = Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Cos(2f * Mathf.PI * u2);
        return z0;
    }
}
