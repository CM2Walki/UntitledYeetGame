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
