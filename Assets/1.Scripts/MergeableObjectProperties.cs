//#define DEBUG_CLASS

using TMPro;
using UnityEngine;

public class MergeableObjectProperties : MonoBehaviour
{
    public float Damage;
    public float Weight;
    public bool Yeetable = true;

    public TextMeshPro debugText;

    private void Awake()
    {
        if (Damage == 0)
        {
            Damage = MergeableObjectManager.DEFAULT_OBJECT_DAMAGE;
        }

        if (Weight == 0)
        {
            Weight = MergeableObjectManager.DEFAULT_OBJECT_WEIGHT;
        }

#if DEBUG_CLASS && UNITY_EDITOR
        debugText.gameObject.SetActive(true);
#else
        debugText.gameObject.SetActive(false);
#endif
    }

    public void SetDebugText(string text)
    {
        debugText.text = text;
    }
}
