using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    float timer = 0f;
    public float duration = 0.25f;
    public float maxScale = 0.1f;
    private Vector3 originalScale;
    private void Awake()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    void Update()
    {
        timer += Time.deltaTime;

        var scaleDelta = Mathf.Clamp01(timer / maxScale);
        transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, scaleDelta);

        if (timer > duration)
        {
            Destroy(this.gameObject);
        }
    }
}
