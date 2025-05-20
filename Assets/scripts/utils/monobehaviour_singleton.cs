using System;

using UnityEngine;

class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    public static T Instance { get; protected set; }

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            throw new Exception($"An instance of {typeof(T)} already exists.");
        }
        Instance = (T)this;
    }
}