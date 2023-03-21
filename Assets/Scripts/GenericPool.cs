using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class GenericPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T prefab;

    public static GenericPool<T> Instance { get; private set; }
    private Queue<T> pool = new Queue<T>();

    void Awake()
    {
		Instance = this;
    }

    public T Get()
    {
        //Create or get one instance
        if (pool.Count == 0) AddObjectsToPool(1);
        T newT = pool.Dequeue();
        newT.gameObject.SetActive(true);
        return newT;
    }

    private void AddObjectsToPool(int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            T newT = Instantiate(prefab);
            newT.gameObject.SetActive(false);
            pool.Enqueue(newT);
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}
