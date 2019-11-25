using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    public Transform target;
    public float UpdateFrequence = 2f; //Fréquence à laquelle le chemin sera réactualisé
    private Seeker seeker;
    private Rigidbody2D body;

    public Path path;    //le chemin calculé
    public float speed = 300f;
    public ForceMode2D fMode; //change la façon dont la force s'applique sur un rigidbody

    public bool pathIsEnded = false;
    public float nextWayPointDistance = 3; //à quelle distance de son étape l'objet doit se trouver pour considérer l'avoir franchi
    private int currentWayPoint = 0; //Le point vers lequel on se dirige

    // Use this for initialization
    void Start() {
        seeker = GetComponent<Seeker>();
        body = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            Debug.LogError("No target");
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }
    public void OnPathComplete(Path p)
    {
        Debug.Log("Chemin trouvé.");
        if (!p.error) {
            path = p;
            currentWayPoint = 0;
        }
    }

    public IEnumerator UpdatePath(){
        if(target ==null)
        {
            yield return false; 
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / UpdateFrequence);
        StartCoroutine(UpdatePath());

    }


    // FixedUpdate = Update mais quand on utilise la physique du moteur de jeu
    void FixedUpdate () {
        if (target == null)
        {
            return;
        }

        if(path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            else
            {
                pathIsEnded = true;
                return;
            }

        }

        pathIsEnded = false;
        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime; //utiliser fixedDeltaTim dans un FixedUpdate

        //Le vrai mouvement commence ici.
        body.AddForce(dir, fMode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if (dist < nextWayPointDistance)
        {
            currentWayPoint++; //incrémenter le point de passage pour passer au suivant
            return;
        }

    }
}
