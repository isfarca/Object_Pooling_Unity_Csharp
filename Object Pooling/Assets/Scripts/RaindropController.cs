using UnityEngine;

public class RaindropController : MonoBehaviour
{
    #region Declare variables

    // Reference types
    private CloudController cloudControllerScript;

    #endregion

    #region System functions

    private void Awake()
    {
        cloudControllerScript = FindObjectOfType<CloudController>();
        Debug.Log(cloudControllerScript);
    }

    private void OnTriggerEnter(Collider other)
    {
        cloudControllerScript.ReturnToPool(gameObject);
    }

    #endregion
}
