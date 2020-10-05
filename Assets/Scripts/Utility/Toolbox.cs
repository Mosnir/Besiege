using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Toolbox : Singleton<Toolbox>
{
    Dictionary<Type, MonoBehaviour> managers = new Dictionary<Type, MonoBehaviour>();

    private void Awake()
    {
        AddManager<LevelsManager>();
        AddManager<PartsManager>();
        AddManager<GarageManager>();
        AddManager<SaveManager>();
    }

    public void AddManager<T>() where T : MonoBehaviour
    {
        if(!managers.ContainsKey(typeof(T)))
        {
            GameObject go = new GameObject(typeof(T).Name);
            managers[typeof(T)] = go.AddComponent<T>();
            go.transform.parent = transform;
        }
    }

    public void RemoveManager<T>() where T : MonoBehaviour
    {
        if (managers.ContainsKey(typeof(T)))
        {
            Destroy(managers[typeof(T)].gameObject);
            managers.Remove(typeof(T));
        }
    }

    public T Get<T>() where T : MonoBehaviour
    {
        if (managers.ContainsKey(typeof(T)))
        {
            return managers[typeof(T)] as T;
        }
        return null;
    }
}