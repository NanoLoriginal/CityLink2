using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject target;

    

    float shortestDistance = 1000000f;

    int step = 0;

    private float Health = 100f;

    private float baseDamages = 20f;

    private float cooldown = 0.1f;


    GameManager gm;
    MoneyMaker mm;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        mm = FindObjectOfType<MoneyMaker>();
        

        //gm.TownList.AddRange(GameObject.FindGameObjectsWithTag("Ville"));


        if (target == null)
        {
            for (int i = 0; i < gm.TownList.Count; i++)
            {
                if ((transform.position - gm.TownList[i].transform.position).magnitude < shortestDistance)
                {
                    target = gm.TownList[i].gameObject;
                    shortestDistance = (gm.TownList[i].transform.position - this.transform.position).magnitude;
                }
            }
        }
    }

    void Update()
    {

        if (target != null)
        {
            GetComponent<NavMeshAgent>().destination = target.transform.position;
        }

        if (target == null)
        {
            findNewTarget();
        }
    }

    public void setTarget(GameObject t)
    {
        target = t;
    }

    public void findNewTarget()
    {
        shortestDistance = 1100000f;
        if (target == null)
        {
            for (int i = 0; i < gm.TownList.Count; i++)
            {
                if ((transform.position - gm.TownList[i].transform.position).magnitude < shortestDistance)
                {
                    target = gm.TownList[i].gameObject;
                    shortestDistance = (gm.TownList[i].transform.position - this.transform.position).magnitude;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Town>().getHealth() > 0)
        {
            cooldown -= Time.deltaTime;
            if(cooldown < 0)
            {
                other.GetComponent<Town>().setHealth(baseDamages);
                Debug.Log("santé de la ville :" + other.GetComponent<Town>().getHealth());
                cooldown = 2f;
            }

            
        }
        else
        {

            gm.TownList.Remove(other.gameObject);
            mm.allTowns.Remove(other.gameObject);
            target = null;
            findNewTarget();
            Destroy(other.gameObject);
        }
    }
}
