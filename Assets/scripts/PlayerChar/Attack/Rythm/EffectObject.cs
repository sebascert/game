using UnityEngine;

public class EffectObjects : MonoBehaviour
{
    public float lifeTime = .2f;

    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}