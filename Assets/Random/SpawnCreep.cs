using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreep : MonoBehaviour
{
    public GameObject prefab;
    public float spawnSpeed = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            GameObject SpawnCreep = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody SpawnedCreepRB = SpawnCreep.GetComponent<Rigidbody>();
            SpawnedCreepRB.velocity = transform.forward * spawnSpeed;
        }
    }
}
