using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.position);

        other.GetComponent<CharacterController>().enabled = false;
        other.transform.position = destination.position + new Vector3(1, 0, 0);
        other.GetComponent<CharacterController>().enabled = true;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
