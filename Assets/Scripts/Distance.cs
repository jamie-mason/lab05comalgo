using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{
    [SerializeField] private Transform object1;
    public bool right;
    float distanceX;
    float distanceY;
    float distanceZ;
    void Start()
    {
        if (right)
        {
            distanceX = object1.position.x + object1.lossyScale.x / 2f + transform.lossyScale.x / 2;
        }
        else { 
            distanceX = object1.position.x - object1.lossyScale.x / 2f - transform.lossyScale.x / 2; 
        }
        distanceY = object1.position.y + object1.lossyScale.y / 2f + transform.lossyScale.y / 2;
        distanceZ = object1.position.z + object1.lossyScale.z / 2f + transform.lossyScale.z / 2;
        Debug.Log($"X Position: {distanceX}");
        Debug.Log($"Y Position: {distanceY}");
        Debug.Log($"Z Position: {distanceZ}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
