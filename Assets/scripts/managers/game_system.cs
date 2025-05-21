using System;

using UnityEngine;

abstract class GameSystemBase : MonoBehaviour
{
    public abstract void Init();
}

abstract class GameSystem<T> : GameSystemBase where T : GameSystem<T>
{
    public static T Instance { get; private set; }

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