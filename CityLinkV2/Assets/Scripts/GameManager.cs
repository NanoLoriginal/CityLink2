using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Transform lineContainer;

    // référence du prefab line renderer
    [SerializeField] GameObject lineRendRef;


    // position de a souris
    [SerializeField] GameObject Mouse3Dref;
    [SerializeField] LayerMask lmVilles;

    [SerializeField] List<GameObject> villesLinked = new List<GameObject>();

    [SerializeField] public List<GameObject> TownList = new List<GameObject>();

    LineRendScript LineRendererScript;

    private GameObject lineRendClone;
    private GameObject villeActuelle = null;
    private GameObject villeActuelle1 = null;
    private GameObject[] villeList;

    

    [SerializeField] Canvas winScreen;

    public bool cantTraceLine = false;
    public bool sortieForEach = false;
    public bool creatingLine = false;

    public bool lineMode = false;
    public bool upgradeMode = false;
    public bool gameOver = false;

    MoneyMaker mm;


    private float Timer;


    private void Awake()
    {
        villeList = GameObject.FindGameObjectsWithTag("Ville");
    }

    private void Start()
    {
        mm = FindObjectOfType<MoneyMaker>();
    }
    private void Update()
    {
        Timer += Time.deltaTime;

        Debug.Log("temps :" + Mathf.Round(Timer));

        if (TownList.Count<2)
        {
            winScreen.gameObject.SetActive(true);
            StopAllCoroutines();
            gameOver = true;
            mm.setHighScore();
            mm.bestScoreText.text = PlayerPrefs.GetFloat("HighScore", 0).ToString();
            Debug.Log("jeu fini");
        }

        if (Input.GetMouseButtonDown(0) && lineMode == true)
        {

            villeActuelle1 = Mouse3Dref.GetComponent<Mouse3DPosition>().hitObject;

            if (Mouse3Dref.GetComponent<Mouse3DPosition>().hitObject.layer == 6 && villeActuelle1.GetComponent<Town>().getLink() < 2)
            {
                if (cantTraceLine == false)
                {
                    creatingLine = true;
                    lineRendClone = Instantiate(lineRendRef, lineContainer);
                    LineRendererScript = lineRendClone.GetComponent<LineRendScript>();
                    cantTraceLine = true;
                }
            }
        }

        else if (Input.GetMouseButtonUp(0) && lineMode == true)
        {

            //villeActuelle = Mouse3Dref.GetComponent<Mouse3DPosition>().hitObject;
            villeActuelle = Mouse3Dref.GetComponent<Mouse3DPosition>().hitObject;

            if (Mouse3Dref.GetComponent<Mouse3DPosition>().hitObject.layer == 6 && villeActuelle.GetComponent<Town>().getLink() < 2 && villeActuelle != villeActuelle1)
            {
                //villeActuelle = Mouse3Dref.GetComponent<Mouse3DPosition>().hitObject;

                Debug.Log("entree if");

                cantTraceLine = true;

                LineRendererScript.setEndPos();

                //LineRendererScript.enabled = false;
                villesLinked.Add(villeActuelle);

                villeActuelle.GetComponent<Town>().setLinked(LineRendererScript.gameObject);
                villeActuelle.GetComponent<Town>().addLink();
                villeActuelle1.GetComponent<Town>().setLinked(LineRendererScript.gameObject);
                villeActuelle1.GetComponent<Town>().addLink();

                mm.minusLine();
                cantTraceLine = false;

            }
            else
            {
                if (creatingLine == true)
                {
                    Destroy(lineRendClone);
                }
                cantTraceLine = false;
            }

            creatingLine = false;
        }

    }
    public void setLineMode()
    {
        lineMode = true;
        upgradeMode = false;
    }

    public void setUpgradeMode()
    {
        upgradeMode = true;
        lineMode = false;
    }

}
