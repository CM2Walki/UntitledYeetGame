using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MergeableObject : MonoBehaviour
{
    [ShowOnly]
    public float Damage;
    [ShowOnly]
    public float Weight;
    public Vector3Int VectorMaterial;

    private float StartingDamage;
    private float StartingWeight;
    private Vector3Int StartingVectorMaterial;

    private static MergeableObjectManager mergeableObjectManager;
    private GameObject activeGameObject;

#if UNITY_EDITOR
    [SerializeField]
    private Vector3Int DebugVectorMaterial;

    [Button]
    private void AddDebugVectorMaterial()
    {
        UpgradeObjectByAddition(DebugVectorMaterial);
    }

    [Button]
    private void AddWood()
    {
        UpgradeObjectByAddition(ObjectMaterial.Wood);
    }

    [Button]
    private void AddSteel()
    {
        UpgradeObjectByAddition(ObjectMaterial.Steel);
    }

    [Button]
    private void AddFire()
    {
        UpgradeObjectByAddition(ObjectMaterial.Fire);
    }

    [Button]
    private void ResetButton()
    {
        ResetObject();
    }
#endif

    private void ResetObject()
    {
        Damage = StartingDamage;
        Weight = StartingWeight;
        VectorMaterial = StartingVectorMaterial;
        UpdateModel(null, true);
    }

    private void Awake()
    {
        StartingDamage = Damage;
        StartingWeight = Weight;
        StartingVectorMaterial = VectorMaterial;

        if (mergeableObjectManager == null)
        {
            mergeableObjectManager = FindObjectOfType<MergeableObjectManager>();
        }

        var startingGO = mergeableObjectManager.GetObjectFromMaterial(VectorMaterial);

        UpdateModel(startingGO, true);
    }

    private void UpdateModel(GameObject go, bool clearOld)
    {
        if (clearOld && activeGameObject != null)
        {
            Destroy(activeGameObject);
        }

        if (go == null)
        {
            return;;
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
                if ((ObjectMaterial)VectorMaterial[i] == ObjectMaterial.None)
                {
                    // Try to upgrade using a clone of the material
                    var tempVector = VectorMaterial;
                    tempVector[i] = (int)materialToAdd;
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

    public bool UpgradeObjectByAddition(Vector3Int inputMaterial)
    {
        if (IsUpgradeAllowed(ref inputMaterial))
        {
            Vector3Int tempVector = VectorMaterial;
            for (int i = 0; i < 3; i++)
            {
                if ((ObjectMaterial) VectorMaterial[i] == ObjectMaterial.None)
                {
                    // Try to upgrade using a clone of the material
                    tempVector[i] = inputMaterial[i];
                }
            }

            if (VectorMaterial != tempVector)
            {
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
        return false;
    }

    public bool UpgradeObjectByOverride(Vector3Int inputMaterial)
    {
        var go = mergeableObjectManager.GetObjectFromMaterial(inputMaterial);
        if (go != null)
        {
            // Upgrade is valid
            VectorMaterial = inputMaterial;
            UpdateModel(go, true);
            return true;
        }
        return false;
    }

    private bool IsUpgradeAllowed(ObjectMaterial materialToAdd)
    {
        return CountUnOccupiedMaterials(ref VectorMaterial) >= 1;
    }

    private bool IsUpgradeAllowed(ref Vector3Int inputMaterial)
    {
        var occupied = CountOccupiedMaterials(ref inputMaterial);
        return occupied != 0 && CountUnOccupiedMaterials(ref VectorMaterial) >= occupied;
    }

    private int CountUnOccupiedMaterials(ref Vector3Int materialVector)
    {
        var count = 0;
        for (int i = 0; i < 3; i++)
        {
            if (materialVector[i] == (int)ObjectMaterial.None)
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
