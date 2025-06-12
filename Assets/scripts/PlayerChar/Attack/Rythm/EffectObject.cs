using System.Collections;

using UnityEngine;

public class EffectObjects : MonoBehaviour
{
    public float lifeTime = .2f;

    void Update()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        Destroy(gameObject);
    }
}