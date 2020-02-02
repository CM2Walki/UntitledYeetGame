using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ObjectMaterial
{
    None,
    Wood,
    Steel,
    Fire
}

[ExecuteInEditMode]
public class MergeableObjectManager : MonoBehaviour
{
    [Serializable]
    public class ObjectMergeDictionary : SerializableDictionaryBase<Vector3Int, GameObject> { }
    
    public ObjectMergeDictionary objectMergeDictionary;

    public static readonly float DEFAULT_OBJECT_DAMAGE = 1;
    public static readonly float DEFAULT_OBJECT_WEIGHT = 1;

    public GameObject baseMergeableObjectPrefab;

    void Start()
    {
        if (objectMergeDictionary.Count == 0)
        {
            objectMergeDictionary = new ObjectMergeDictionary();
            // 1
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, 0, 0), null); // W
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, 0, 0), null); // S
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Fire, 0, 0), null); // F

            // 2
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Wood, 0), null); // WW - Longwood
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Fire, 0), null); // WF - Torch
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Steel, 0), null); // WS - Knife
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Fire, (int)ObjectMaterial.Wood, 0), null); // FW - Torch
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Fire, (int)ObjectMaterial.Steel, 0), null); // FS - Throwing Star
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Wood, 0), null); // SW - Hammer
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Fire, 0), null); // SF - Throwing Star
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Steel, 0), null); // SS - Crowbar

            // 3
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Wood, (int)ObjectMaterial.Wood), null); // WWW - Quarterstaff
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Wood, (int)ObjectMaterial.Fire), null); // WWF - Wizard's Wand
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Wood, (int)ObjectMaterial.Steel), null); // WWS - Spear
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Fire, (int)ObjectMaterial.Wood), null); // WFW - Wooden Lantern
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Fire), null); // WSF - Flaming Knife
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Wood), null); // WSW - Nunchucks
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Wood, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Steel), null); // WSS - Sword
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Wood, (int)ObjectMaterial.Fire), null); // SWF - Flaming hammer
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Wood, (int)ObjectMaterial.Wood), null); // SWW - Sign/Billboard
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Fire), null); // SSF - Flaming Crowbar
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Wood), null); // SSW - Bar Stool
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Steel), null); // SSS - Anvil
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Fire, (int)ObjectMaterial.Wood), null); // SFW - Flaming hammer
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Fire, (int)ObjectMaterial.Steel), null); // SFS - Steel Lantern
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Fire, (int)ObjectMaterial.Fire), null); // SFF - Flaming throwing star
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Steel, (int)ObjectMaterial.Wood, (int)ObjectMaterial.Steel), null); // SWS - Double-ended Axe
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Fire, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Wood), null); // FSW - Flaming hammer
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Fire, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Fire), null); // FSF - Flaming throwing star
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Fire, (int)ObjectMaterial.Fire, (int)ObjectMaterial.Steel), null); // FFS - Flaming throwing star
            objectMergeDictionary.Add(new Vector3Int((int)ObjectMaterial.Fire, (int)ObjectMaterial.Steel, (int)ObjectMaterial.Steel), null); // FSS - Flaming crowbar
        }
    }

    /// <summary>
    /// Returns the MergeableObject Prefab correlating to the combination
    /// </summary>
    /// <param name="vectorMaterial"></param>
    /// <returns></returns>
    public GameObject GetObjectFromMaterial(Vector3Int vectorMaterial)
    {
        GameObject go_out;
        objectMergeDictionary.TryGetValue(vectorMaterial, out go_out);
        return go_out;
    }

    /// <summary>
    /// Returns the base (length 1) Prefab elements of a any length MergeableObject
    /// </summary>
    /// <param name="vectorMaterial"></param>
    /// <returns></returns>
    public static Dictionary<Vector3, Vector3Int> SplitObjectToMaterials(Vector3Int vectorMaterial)
    {
        if (vectorMaterial.magnitude == 0)
        {
            return null;
        }

        Dictionary<Vector3, Vector3Int> gameObjects = new Dictionary<Vector3, Vector3Int>();

        for (int i = 0; i < 3; i++)
        {
            Vector3Int go_out = Vector3Int.zero;
            go_out.x = vectorMaterial[i];

            if ((ObjectMaterial) vectorMaterial[i] != ObjectMaterial.None)
            {
                var position = Random.insideUnitSphere * 20000;

                while (gameObjects.ContainsKey(position))
                {
                    position = Random.insideUnitSphere * 20000;
                }
                gameObjects.Add(position, go_out);
            }
        }

        return gameObjects;
    }
}