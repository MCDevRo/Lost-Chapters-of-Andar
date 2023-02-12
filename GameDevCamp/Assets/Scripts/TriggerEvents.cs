using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    public GameObject objectToMove;
    public Vector3 moveDirection;

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log(GameObject.FindWithTag("BridgeSpawn"));
        

        if (other.gameObject.tag == "TriggerBox")
        {
            Destroy(GameObject.FindWithTag("Bridge"));
            
        }
        else if(other.gameObject.tag == "TriggerPlatformB")
        {
            objectToMove.transform.position += moveDirection;
            other.gameObject.SetActive(false);
        }
       
            
        
    }
}