using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Done_LaserController : MonoBehaviour {

    public LineRenderer[] lr;
	
	void Update () {
        // lr.SetPosition(0, new Vector3(0,0,100));
        for (int i = 0; i < lr.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(lr[i].transform.position, lr[i].transform.forward, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    lr[i].SetPosition(1, hit.point);
                    // Debug.Log("hit "+hit.collider.name);
                }
            }
            else{
                lr[i].SetPosition(1, transform.forward*5000);
            } 
        }
        
	}
}