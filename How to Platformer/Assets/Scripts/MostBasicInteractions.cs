using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostBasicInteractions : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    
    
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //--Si le joueur est au dessus à l'impact, on subit des dégâts
        if (collision.gameObject.tag == "Player" && collision.transform.GetChild(0).position.y >= transform.position.y)
        {
            Debug.Log(collision.transform.GetChild(0).name);
            Destroy(gameObject);
        }

        //--Si le joueur est en dessous à l'impact, il subit des dégâts
        else if (collision.gameObject.tag == "Player" && collision.transform.GetChild(0).position.y < transform.position.y && !collision.GetComponent<Player>().hasTakenDamage)
        {
            Debug.Log(collision.transform.GetChild(0).name);

            collision.GetComponent<Player>().DamagePlayer(50);
            collision.GetComponent<Player>().hasTakenDamage = true;

        }

    }
    


}
