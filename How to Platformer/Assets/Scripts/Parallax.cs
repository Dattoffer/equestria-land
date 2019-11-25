using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public Transform[] backgrounds; //Tableau contenant les arrière plans
    private float[] parallaxScales; //Définit le mouvement des arrière plans par rapport au mouvement de la caméra
    public float smoothing = 1f; //A garder au dessus de 0.0

    private Transform cam; //La caméra du Jeu
    private Vector3 previousCamPosition; //position de la caméra dans la frame précédente

    // Appelé avant le Start, mais après la définition des Game Objects dans l'espace de jeu. 
    //Utile pour définir des variables avant le lancement de l'objet
    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        previousCamPosition = cam.position;

        parallaxScales = new float[backgrounds.Length]; //définir un tableau de la même taille que le tableau backgrounds

        for(int i =0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; 

        }

    }
	
	// Update is called once per frame
	void Update () {
		
        for(int i=0; i<backgrounds.Length;i++)
        {
            //définir parallax par opposition au mouvement de la caméra
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];
            
            //position "cible" qui est la position actuelle + le parallax en x
            Vector3 backgroundTargetPos = new Vector3(backgrounds[i].position.x + parallax, backgrounds[i].position.y, backgrounds[i].position.z);

            //créer le mouvement du background de la position actuelle à la position cible via la fonction Lerp.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing);  //smoothing = vitesse
        }
        //Définir la nouvelle previousCamPosition
        previousCamPosition = cam.position;
	}
}
