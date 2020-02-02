using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PrefabGridScript : MonoBehaviour
{
    void Start()
    {
        var mergeableObjectManager = FindObjectOfType<MergeableObjectManager>();
        int spacing = 8;
        int rowspacing = 8;
        int linebreakAfterSteps = 6;
        Vector3Int gridPos = new Vector3Int(spacing, 0, spacing);

        int step = 0;

        foreach (var vec3 in mergeableObjectManager.objectMergeDictionary)
        {
            step++;
            if (step == linebreakAfterSteps)
            {
                step = 0;
                gridPos = new Vector3Int(0, 0, gridPos.z + rowspacing);
            }
            gridPos = new Vector3Int(gridPos.x + spacing, gridPos.y, gridPos.z);
            var obj = Instantiate(mergeableObjectManager.baseMergeableObjectPrefab, gridPos, Quaternion.identity);
            var mergeObj = obj.GetComponent<MergeableObject>();
            mergeObj.UpgradeObjectByOverride(vec3.Key);
        }
    }
}
