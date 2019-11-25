using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Définition de la classe stockant les stats du joueur
    [System.Serializable] //Permet de faire apparaître les variables dans l'éditeur
    public class PlayerStats {

        public int maxPV=100;
        private int currentPV; //----Variable stockée
        public int tempPV //-----Variable manipulée par les calculs
        {
            get { return currentPV; } //get : va chercher la valeur stockée
            set { currentPV = Mathf.Clamp(value, 0, maxPV); } //set : change la valeur stockée, mais reste entre 0 et le maximum de PV
        }

     
        public void Init()
        {
            tempPV = maxPV;
        }

    }

    //Instanciation de la classe PlayerStats pour ce script.
    public PlayerStats thisPlayerStats = new PlayerStats();
    public float fallLimit = -10;
    private Animator m_Anim;
    public float deathTimer;
    [SerializeField]
    private InfoManager infoStats;

 [Header("Frame d'invincibilités")]
    public bool hasTakenDamage;
    private float durationInvincibilityFrame=3f;
    public float timerInvincibilityFrame;
    private bool fadeOut = true;



    private void Start()
    {
        m_Anim = GetComponent<Animator>();
        timerInvincibilityFrame = durationInvincibilityFrame;

        thisPlayerStats.Init();
        if(infoStats)
        {
            infoStats.SetPV(thisPlayerStats.tempPV, thisPlayerStats.maxPV);
        }
    }

    private void Update()
    {
       
        //---Si le joueur tombe, il prend des dommages
        if (transform.position.y <= fallLimit )
        {
            DamagePlayer(9999);          

        }

        if (thisPlayerStats.tempPV <= 0)
        {
            deathTimer -= Time.deltaTime;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        if (deathTimer <= 0)
        {
            Debug.Log("Dieded !");
            GameMaster.Killplayer(this);
        }



        //---Gestion frames d'invincibilités
        if (hasTakenDamage && thisPlayerStats.tempPV > 0  && timerInvincibilityFrame <= durationInvincibilityFrame)
        {
            timerInvincibilityFrame -= Time.deltaTime;
            //---Clignotement par opacité
            if(GetComponent<SpriteRenderer>().color.a >= 0 && fadeOut)
            {
                Debug.Log("Fading out");
                float opacity = GetComponent<SpriteRenderer>().color.a;
                opacity -= Time.deltaTime*2;
                GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, opacity);
            }
            if(GetComponent<SpriteRenderer>().color.a <= 0 && fadeOut)
            {
                Debug.Log("Is out");

                fadeOut = false;
            }
            if (GetComponent<SpriteRenderer>().color.a < 1 && !fadeOut)
            {
                Debug.Log("Fading in");

                float opacity = GetComponent<SpriteRenderer>().color.a;
                opacity += Time.deltaTime*2;
                GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, opacity);
            }
            if (GetComponent<SpriteRenderer>().color.a >= 1 && !fadeOut)
            {
                Debug.Log("Is in");

                fadeOut = true;
            }
            //----Clignotement par opacité : fin
        }

        if (hasTakenDamage && timerInvincibilityFrame <= 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1);
            timerInvincibilityFrame = durationInvincibilityFrame;
            hasTakenDamage = false;

        }



    }

    //Définition de la fonction de dommage
    public void DamagePlayer(int damageDone)
    {
        thisPlayerStats.tempPV -= damageDone;

        if (infoStats)
        {
            infoStats.SetPV(thisPlayerStats.tempPV, thisPlayerStats.maxPV);
        }

        //----Si la vie tombe à zéro
        if (thisPlayerStats.tempPV <= 0)
        {
            //Lancer l'animation de mort
            m_Anim.Play("Death");

            
        }
    }



}
