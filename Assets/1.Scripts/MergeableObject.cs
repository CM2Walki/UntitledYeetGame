using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeableObject : MonoBehaviour
{
    [ShowOnly]
    public float Damage;
    [ShowOnly]
    public float Weight;
    public Vector3Int VectorMaterial;

    private static MergeableObjectManager mergeableObjectManager;
    private GameObject activeGameObject;

    private void Awake()
    {
        if (mergeableObjectManager == null)
        {
            mergeableObjectManager = FindObjectOfType<MergeableObjectManager>();
        }

        var startingGO = mergeableObjectManager.GetObjectFromMaterial(VectorMaterial);

        if (startingGO != null)
        {
            UpdateModel(startingGO, true);
        }
    }

    private void UpdateModel(GameObject go, bool clearOld)
    {
        if (clearOld)
        {
            Destroy(activeGameObject);
        }

        activeGameObject = Instantiate(go, transform);

        var props = activeGameObject.GetComponent<MergeableObjectProperties>();

        Damage += props.Damage;
        Weight += props.Weight;

#if UNITY_EDITOR
        var x = ((ObjectMaterial)VectorMaterial.x).ToString();
        var y = ((ObjectMaterial)VectorMaterial.y).ToString();
        var z = ((ObjectMaterial)VectorMaterial.z).ToString();
        props.SetDebugText($"{x} {y} {z}<br>{activeGameObject.name.Split('-')[0]}");
#endif
    }

    public bool UpgradeObjectByAddition(ObjectMaterial materialToAdd)
    {
        if (IsUpgradeAllowed(materialToAdd))
        {
            for (int i = 0; i < 3; i++)
            {
                if ((ObjectMaterial) VectorMaterial[i] == ObjectMaterial.None)
                {
                    // Try to upgrade using a clone of the material
                    var tempVector = VectorMaterial;
                    tempVector[i] = (int) materialToAdd;
                    var go = mergeableObjectManager.GetObjectFromMaterial(tempVector);
                    if (go != null)
                    {
                        // Upgrade is valid
                        VectorMaterial = tempVector;
                        UpdateModel(go, true);
                        return true;
                    }

                    break;
                }
            }
        }
        return false;
    }

    public bool UpgradeObjectByAddition(Vector3Int vectorMaterial)
    {
        if (IsUpgradeAllowed(ref vectorMaterial))
        {
            var freeCount = 0;
            for (int i = 0; i < 3; i++)
            {
                if ((ObjectMaterial)VectorMaterial[i] == ObjectMaterial.None)
                {
                    freeCount++;
                }

                if (freeCount >= CountOccupiedMaterials(ref VectorMaterial))
                {
                    // Try to upgrade using a clone of the material
                    var tempVector = VectorMaterial;
                    tempVector[i - 1] = VectorMaterial[0];
                    tempVector[i] = VectorMaterial[1];
                    var go = mergeableObjectManager.GetObjectFromMaterial(tempVector);
                    if (go != null)
                    {
                        // Upgrade is valid
                        VectorMaterial = tempVector;
                        UpdateModel(go, true);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool IsUpgradeAllowed(ObjectMaterial materialToAdd)
    {
        return CountUnOccupiedMaterials(ref VectorMaterial) >= 1;
    }

    private bool IsUpgradeAllowed(ref Vector3Int inputMaterial)
    {
        return CountUnOccupiedMaterials(ref VectorMaterial) >= CountOccupiedMaterials(ref inputMaterial);
    }

    private int CountUnOccupiedMaterials(ref Vector3Int materialVector)
    {
        var count = 0;
        for (int i = 0; i < 3; i++)
        {
            if (materialVector[i] == (int) ObjectMaterial.None)
            {
                count++;
            }
        }
        return count;
    }

    private int CountOccupiedMaterials(ref Vector3Int materialVector)
    {
        var count = 0;
        for (int i = 0; i < 3; i++)
        {
            if (materialVector[i] != (int)ObjectMaterial.None)
            {
                count++;
            }
        }
        return count;
    }
}
