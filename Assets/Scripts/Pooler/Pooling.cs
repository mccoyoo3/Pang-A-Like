using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private Queue<GameObject> availablBullets = new Queue<GameObject>();

    public static Pooling Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(bullet);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }
    //add to current pool
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availablBullets.Enqueue(instance);
    }
    //show object in current pool
    public GameObject GetFromPool()
    {
        if (availablBullets.Count == 0)
        {
            GrowPool();
        }

        var instance = availablBullets.Dequeue();

        instance.SetActive(true);
        return instance;
    }
}
