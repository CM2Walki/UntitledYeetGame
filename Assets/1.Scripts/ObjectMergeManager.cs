using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectMergeManager : MonoBehaviour
{
    [Serializable]
    public enum Material:int
    {
        None,
        Wood,
        Steel,
        Fire
    }

    [Serializable]
    public class ObjectMergeDictionary : SerializableDictionaryBase<Vector3Int, GameObject> { }

    public ObjectMergeDictionary objectMergeDictionary;

    void Start()
    {
        if (objectMergeDictionary.Count == 1)
        {
            objectMergeDictionary = new ObjectMergeDictionary();
            // 1
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, 0, 0), null); // W
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, 0, 0), null); // S
            objectMergeDictionary.Add(new Vector3Int((int)Material.Fire, 0, 0), null); // F

            // 2
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Wood, 0), null); // WW - Longwood
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Fire, 0), null); // WF - Torch
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Steel, 0), null); // WS - Knife
            objectMergeDictionary.Add(new Vector3Int((int)Material.Fire, (int)Material.Wood, 0), null); // FW - Torch
            objectMergeDictionary.Add(new Vector3Int((int)Material.Fire, (int)Material.Steel, 0), null); // FS - Throwing Star
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Wood, 0), null); // SW - Hammer
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Fire, 0), null); // SF - Throwing Star
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Steel, 0), null); // SS - Crowbar

            // 3
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Wood, (int)Material.Wood), null); // WWW - Quarterstaff
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Wood, (int)Material.Fire), null); // WWF - Wizard's Wand
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Wood, (int)Material.Steel), null); // WWS - Spear
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Fire, (int)Material.Wood), null); // WFW - Wooden Lantern
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Steel, (int)Material.Fire), null); // WSF - Flaming Knife
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Steel, (int)Material.Wood), null); // WSW - Nunchucks
            objectMergeDictionary.Add(new Vector3Int((int)Material.Wood, (int)Material.Steel, (int)Material.Steel), null); // WSS - Sword
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Wood, (int)Material.Fire), null); // SWF - Thor’s hammer
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Wood, (int)Material.Wood), null); // SWW - Sign/Billboard
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Steel, (int)Material.Fire), null); // SSF - Flaming Crowbar
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Steel, (int)Material.Wood), null); // SSW - Bar Stool
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Steel, (int)Material.Steel), null); // SSS - Anvil
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Fire, (int)Material.Wood), null); // SFW - Thor’s hammer
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Fire, (int)Material.Steel), null); // SFS - Steel Lantern
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Fire, (int)Material.Fire), null); // SFF - Flaming throwing star
            objectMergeDictionary.Add(new Vector3Int((int)Material.Steel, (int)Material.Wood, (int)Material.Steel), null); // SWS - Double-ended Axe
            objectMergeDictionary.Add(new Vector3Int((int)Material.Fire, (int)Material.Steel, (int)Material.Wood), null); // FSW - Flaming nunchucks
            objectMergeDictionary.Add(new Vector3Int((int)Material.Fire, (int)Material.Steel, (int)Material.Fire), null); // FSF - Flaming throwing star
            objectMergeDictionary.Add(new Vector3Int((int)Material.Fire, (int)Material.Fire, (int)Material.Steel), null); // FFS - Flaming throwing star
            objectMergeDictionary.Add(new Vector3Int((int)Material.Fire, (int)Material.Steel, (int)Material.Steel), null); // FSS - Flaming crowbar
        }
    }

    void Update()
    {

    }
}