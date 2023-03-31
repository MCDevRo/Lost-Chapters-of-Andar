using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    public GameObject objectToMove;
    public Vector3 moveDirection;
    public GameObject runeIndicator;
    public GameObject destroyedBridgePart;
    public GameObject explosionFX;
   
    

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log(GameObject.FindWithTag("BridgeSpawn"));
        

        if (other.gameObject.tag == "TriggerBox")
        {
            FindObjectOfType<AudioManager>().Play("FireHitOmni");
            Instantiate(explosionFX, destroyedBridgePart.transform.position, Quaternion.identity);
            //runeIndicator.SetActive(true);
            Destroy(GameObject.FindWithTag("Bridge"));
            
        }
        else if(other.gameObject.tag == "TriggerPlatformB")
        {
            //runeIndicator.SetActive(false);
            objectToMove.transform.position += moveDirection;
            other.gameObject.SetActive(false);
        }
       
            
        
    }
}