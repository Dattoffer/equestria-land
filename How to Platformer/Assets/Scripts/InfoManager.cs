using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class InfoManager : MonoBehaviour {

    [SerializeField] //(afficher dans éditeur, même en étant private)
    private RectTransform barrePV;
    [SerializeField]
    private Text textPV;
    private bool m_FacingRight = true;


    // Use this for initialization
    void Start () {
	    	

	}
	
	// Update is called once per frame
	void Update () {
        //-----------Assurer que l'interface ne se tourne pas quand le personnage tourne
		if(transform.parent.localScale.x < 0 && transform.localScale.x > 0 || transform.parent.localScale.x > 0 && transform.localScale.x < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

 

    public void SetPV(int currentPV, int maxPV)
    {
        float valuePV = (float)currentPV / maxPV;
        barrePV.localScale = new Vector3(valuePV, 1f, 1f);
        textPV.text = currentPV + "/" + maxPV + "PV";
    }
}
