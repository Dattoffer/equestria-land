using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostBasicCameraFollow : MonoBehaviour {

    public Transform target;
    public float yRestriction=-1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            return; //Si pas de cible, alors inutile d'aller plus loin

        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, yRestriction, Mathf.Infinity), -10f);


	}
}
