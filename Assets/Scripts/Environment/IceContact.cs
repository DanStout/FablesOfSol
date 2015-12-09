using UnityEngine;
using System.Collections;

public class IceContact : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "IceSurface")
        {
            if (gameObject.GetComponent<PlayerMovement>().enabled == true)
            {
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                gameObject.GetComponent<IceMovement>().enabled = true;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "IceSurface")
        {
            if (gameObject.GetComponent<PlayerMovement>().enabled == false)
            {
                gameObject.GetComponent<PlayerMovement>().enabled = true;
				gameObject.GetComponent<PlayerMovement>().DoIgnoreNextFall = true;
                gameObject.GetComponent<IceMovement>().enabled = false;
            }
        }
    }
}
