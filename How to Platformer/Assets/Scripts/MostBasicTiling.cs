using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostBasicTiling : MonoBehaviour {
    public int offsetX = 2;
    public Transform RightSide;
    public Transform LeftSide;
    public bool hasARightSide = false;
    public bool hasALeftSide = false;

    public bool reverseScale = false; //inverser l'objet pour éviter la répétition

    private float spriteLargeur = 0f;
    private Camera cam;
    //private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
        // myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteLargeur = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {

        if(LeftSide == null)
        {
            hasALeftSide = false;
        }
        else if (LeftSide != null)
        {
            hasALeftSide = true;

        }

        if (RightSide == null)
        {
            hasARightSide = false;
        }
        else if (RightSide != null)
        {
            hasARightSide = true;

        }

        if (!hasALeftSide || !hasARightSide)
        {
            //calculer l'étendue (moitié de largeur) de ce que la caméra voit en coordonnée de l'espace de jeu global
            float camHorizontalExtend = cam.orthographicSize * (Screen.width / Screen.height);

            //calculer la position à laquelle la caméra verra la fin du sprite
            float edgePositionVisibleRight = (transform.position.x + spriteLargeur / 2) - camHorizontalExtend;
            float edgePositionVisibleLeft = (transform.position.x - spriteLargeur / 2) + camHorizontalExtend;

            //si la caméra dépasse le sprite sur la droite
            if (cam.transform.position.x >= edgePositionVisibleRight - offsetX && hasARightSide == false)
            {
                TakeASide(1);

            }

            //si la caméra dépasse le sprite sur la gauche
            else if (cam.transform.position.x <= edgePositionVisibleLeft + offsetX && hasALeftSide == false)
            {
            
                TakeASide(-1);
            }
        }
    }

    void TakeASide(int RightOrLeft) // -1 = Left 1 = Right
    { 
       //Définir position du nouveau côté
        Vector3 newPosition = new Vector3(transform.position.x + spriteLargeur * RightOrLeft, transform.position.y, transform.position.z);
        Transform newSide;
              
        if (RightOrLeft > 0)
        {
            Debug.Log("A droite");
            //Prendre le plus à gauche pour en faire le nouveau côté et le déplacer
            newSide = LeftSide.GetComponent<MostBasicTiling>().LeftSide;
            newSide.position = newPosition;

            //Le retourner pour ne pas le répéter
            if (reverseScale)
            {
                newSide.localScale = new Vector3(newSide.localScale.x * -1, newSide.localScale.y, newSide.localScale.z);
            }

            //Refaire les variables en accord avec la nouvelle organisation
            newSide.GetComponent<MostBasicTiling>().LeftSide = transform;
            newSide.GetComponent<MostBasicTiling>().RightSide = null;
            RightSide = newSide;
            LeftSide.GetComponent<MostBasicTiling>().LeftSide = null;
            newSide = null;

        }

        else if (RightOrLeft < 0)
        {
            Debug.Log("A gauche");

            newSide = RightSide.GetComponent<MostBasicTiling>().RightSide;
            newSide.position = newPosition;
            if (reverseScale)
            {
                newSide.localScale = new Vector3(newSide.localScale.x * -1, newSide.localScale.y, newSide.localScale.z);
            }

            //newSide.parent = transform.parent;
            newSide.GetComponent<MostBasicTiling>().RightSide = transform;
            newSide.GetComponent<MostBasicTiling>().LeftSide = null;
            LeftSide = newSide;
            RightSide.GetComponent<MostBasicTiling>().RightSide = null;
            newSide = null;
        }
    }
}
