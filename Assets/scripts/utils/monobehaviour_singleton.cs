using System;

using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    public static T instance { get; protected set; }

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this);
            throw new Exception($"An instance of {typeof(T)} already exists.");
        }
        instance = (T)this;
    }
}