using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public MostBasicCameraFollow cam; 
    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay=2;


    private void Start()
    {
        if (gm==null)
        {
            gm = GameObject.Find("GameManager").GetComponent<GameMaster>();

        }
    }

    public static void Killplayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer()); //Coroutine pour fonctionnement du yield
    }

    //IEnumerator pour pouvoir utiliser yield
    public IEnumerator RespawnPlayer()
    {
        Debug.Log("Suggestion : Ajouter son d'attente");
        yield return new WaitForSeconds(spawnDelay);//provoque le délai avant l'exécution des étapes suivantes
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);//Instancie le personnage au point de spawn
        cam.target = GameObject.FindGameObjectWithTag("Player").transform; //donner à la caméra l'instance du personnage comme cible
        Debug.Log("Suggestion : Ajouter effets de particule au respawn");


    }

}
