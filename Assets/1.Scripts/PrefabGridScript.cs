using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class PrefabGridScript : MonoBehaviour
{
    private static MergeableObjectManager mergeableObjectManager;

    void Start()
    {
        mergeableObjectManager = FindObjectOfType<MergeableObjectManager>();

        Dictionary<Vector3, Vector3Int> startingObjects = new Dictionary<Vector3, Vector3Int>();

        GenerateMaterials(ref startingObjects, 15, false);
        GenerateRandomPositions(ref startingObjects, 20, Vector3.zero);
        SpawnObjectsInGrid(ref startingObjects);
    }

    public static void SpawnObjectsInGrid(ref Dictionary<Vector3, Vector3Int> objectDict)
    {
        foreach (var vec in objectDict)
        {
            var obj = Instantiate(mergeableObjectManager.baseMergeableObjectPrefab, vec.Key, Quaternion.identity);
            var mergeableObject = obj.GetComponent<MergeableObject>();
            mergeableObject.UpgradeObjectByOverride(vec.Value);
        }
    }

    public static void GenerateMaterials(ref Dictionary<Vector3, Vector3Int> dictionary, int count, bool random = false)
    {
        if (random)
        {
            for (int i = 0; i < count; i++)
            {
                var position = Random.insideUnitSphere * 20000;
                while (dictionary.ContainsKey(position))
                {
                    position = Random.insideUnitSphere * 20000;
                }
                dictionary.Add(position, new Vector3Int(Random.Range(1, 4), 0, 0));
            }
        }
        else
        {
            int totalofeach = Mathf.CeilToInt(count / 3);

            int curMat = 1;
            for (int i = 0; i < count; i++)
            {
                // Fill position with junk
                var position = Random.insideUnitSphere * 20000;
                while (dictionary.ContainsKey(position))
                {
                    position = Random.insideUnitSphere * 20000;
                }
                dictionary.Add(position, new Vector3Int(curMat, 0, 0));

                if (i % totalofeach == 0)
                {
                    curMat++;
                }
            }
        }
    }

    public static void GenerateRandomPositions(ref Dictionary<Vector3, Vector3Int> dictionary, float radius, Vector3 rootPosition)
    {
        foreach (var key in dictionary.Keys.ToList())
        {
            var position = rootPosition + Random.insideUnitSphere * radius;

            JoyconDemo.ClampPosition(ref position);

            while (dictionary.ContainsKey(position))
            {
                position = rootPosition + Random.insideUnitSphere * radius;
                JoyconDemo.ClampPosition(ref position);
            }

            Debug.DrawLine(position, position * 1.1f, Color.cyan, 999999f);

            var val = dictionary[key];
            dictionary.Remove(key);
            dictionary.Add(position, val);
        }
    }

    private void SpawnAll()
    {
        int spacing = 8;
        int rowspacing = 8;
        int linebreakAfterSteps = 6;
        Vector3 gridPos = new Vector3(spacing + transform.position.x, transform.position.y, spacing + transform.position.z);

        int step = 0;

        foreach (var vec3 in mergeableObjectManager.objectMergeDictionary)
        {
            step++;
            if (step == linebreakAfterSteps)
            {
                step = 0;
                gridPos = new Vector3(transform.position.x, transform.position.y, gridPos.z + rowspacing);
            }
            var obj = Instantiate(mergeableObjectManager.baseMergeableObjectPrefab, gridPos, Quaternion.identity);
            gridPos = new Vector3(gridPos.x + spacing, gridPos.y, gridPos.z);

            var mergeObj = obj.GetComponent<MergeableObject>();
            mergeObj.UpgradeObjectByOverride(vec3.Key);
        }
    }
}
