using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] //vérifie que l'objet auquel est attaché le script contient un SpriteRenderer

public class Tiling : MonoBehaviour {

    public int offsetX = 2;
    public bool hasARightSide = false;
    public bool hasALeftSide = false;

    public bool reverseScale = false; //si l'objet ne se répète pas

    private float spriteLargeur = 0f;
    private Camera cam;
    //private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
       // myTransform = transform;
    }

    // Use this for initialization
    void Start () {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteLargeur = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasALeftSide || !hasARightSide)
        {
            //calculer l'étendue (moitié de largeur) de ce que la caméra voit en coordonnée de l'espace de jeu global
            float camHorizontalExtend = cam.orthographicSize * (Screen.width / Screen.height);

            //calculer la position à laquelle la caméra verra la fin du sprite
            float edgePositionVisibleRight = (transform.position.x + spriteLargeur / 2) - camHorizontalExtend;
            float edgePositionVisibleLeft = (transform.position.x - spriteLargeur / 2) + camHorizontalExtend;

            if(cam.transform.position.x >= edgePositionVisibleRight - offsetX && hasARightSide == false)
            {
                MakeASide(1);
                hasARightSide = true;

            }

            else if (cam.transform.position.x <= edgePositionVisibleLeft + offsetX && hasALeftSide == false)
            {


                MakeASide(-1);
                hasALeftSide = true;
            }
        }
    }

    void MakeASide(int RightOrLeft){ // -1 = Left 1 = Right

        Vector3 newPosition = new Vector3(transform.position.x + spriteLargeur * RightOrLeft, transform.position.y, transform.position.z);
        Transform newSide = (Transform)Instantiate(transform, newPosition, transform.rotation);

        if(reverseScale)
        {
        newSide.localScale = new Vector3(newSide.localScale.x * -1, newSide.localScale.y, newSide.localScale.z);        
        }

        newSide.parent = transform.parent;
        if(RightOrLeft > 0)
        {
            newSide.GetComponent<Tiling>().hasALeftSide = true;

        }

        else if (RightOrLeft < 0)
        {
            newSide.GetComponent<Tiling>().hasARightSide = true;

        }
    }
}
