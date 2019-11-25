using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameMaster GM;
    public bool isChecked;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si le personnage entre en collision avec le checkpoint, alors ce checkpoint devient le point de respawn
        if (collision.gameObject.tag == "Player" && !isChecked)
        {
            GM.GetComponent<GameMaster>().spawnPoint = this.transform;
            isChecked = true;
            Debug.Log("Checkpoint !");
        }

    }

}