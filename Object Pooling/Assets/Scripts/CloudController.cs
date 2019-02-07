using UnityEngine;
using System.Collections.Generic;

public class CloudController : MonoBehaviour
{
    #region Declare variables

    // Value types
    public float raindropRange = 10f;
    public int raindropsPerFrame = 1;
    public int initialPoolAmount = 100;

    // Reference types
    private Stack<GameObject> inactiveObjects;
    private GameObject newRaindrop;
    public GameObject raindropPrefab;

    #endregion

    #region Main function "Update()"

    // Update is called once per frame
    void Update()
    {
        // Algorithm
        RaindropAutoMovement();
    }

    #endregion

    #region System functions

    private void Awake()
    {
        inactiveObjects = new Stack<GameObject>();

        PoolObjects();
        Debug.LogFormat("inactiveObjects count: {0}", inactiveObjects.Count);
    }

    #endregion

    #region Custom functions

    #region Functions for "Update()"

    void RaindropAutoMovement()
    {
        if (raindropPrefab != null)
        {
            for (int i = 0; i < raindropsPerFrame; i++)
            {
                newRaindrop = GetObjectFromPool();

                newRaindrop.transform.position = new Vector3
                (
                    Random.Range(-raindropRange, raindropRange),
                    transform.position.y,
                    Random.Range(-raindropRange, raindropRange)
                );

                newRaindrop.SetActive(true);
            }
        }
    }

    #endregion

    #region Other auxilary functions

    private void PoolObjects()
    {
        for (int i = 0; i < initialPoolAmount; i++)
            CreateObject();
    }

    public void ReturnToPool(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
        inactiveObjects.Push(pooledObject);
    }

    #endregion

    #endregion

    #region Return values

    private GameObject CreateObject()
    {
        // Declare variables
        GameObject newObject = Instantiate(raindropPrefab, transform.position, Quaternion.identity);

        newObject.transform.parent = transform;

        newObject.SetActive(false);
        inactiveObjects.Push(newObject);

        return newObject;
    }

    private GameObject GetObjectFromPool()
    {
        // Declare variables
        GameObject objectFromPool;

        if (inactiveObjects.Count > 0)
        {
            objectFromPool = inactiveObjects.Pop();
            
            return objectFromPool;
        }
        else
            return CreateObject();
    }

    #endregion
}
