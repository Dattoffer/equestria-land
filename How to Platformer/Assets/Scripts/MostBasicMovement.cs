using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostBasicMovement : MonoBehaviour {

    public Transform[] destination;
    public int currentDestination;
    public float speed = 3f;
    [HideInInspector]
    public float destinationDistance = 3f; //distance à laquelle on reconnait avoir atteint la destination
    [HideInInspector]
    public float dist;
    [HideInInspector]
    private bool m_FacingRight = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, destination[currentDestination].position, speed * Time.deltaTime);

         dist = Vector3.Distance(transform.position, destination[currentDestination].position);
        if (dist < destinationDistance) //si la distance actuelle est plus petite que la distance reconnue alors...
        {
            currentDestination++; //...incrémenter le point de passage pour passer au suivant
        }

        //Boucler le mouvement
        if (currentDestination == destination.Length)
        {
            currentDestination = 0;

        }
        //changer le regard du monstre pour le faire regarder sa destination
        if (transform.position.x > destination[currentDestination].position.x && !m_FacingRight)
        {
            Flip();
        }

        else if (transform.position.x < destination[currentDestination].position.x && m_FacingRight)
        {
            Flip();
        }
    }

     private void Flip()
    {
        //change le booléen qui indique la direction du monstre
        m_FacingRight = !m_FacingRight;

        //Multiplier l'échelle X par -1 pour changer le sprite de direction.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
