using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [SerializeField] Material colorMaterial;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color linkedColor;
    [SerializeField] private Color upgradedColor;

    private bool isLinked = false;
    private int nbLink = 0;
    private GameObject LineRenderer1;
    private GameObject LineRenderer2;

    private float moneyRate = 10f;

    private int level = 1;
    private bool up=false;

    private float health = 100f;

    GameManager gm;
    MoneyMaker mm;

    //[SerializeField] private float health = 100f;

    private void Start()
    {
        colorMaterial = GetComponent<MeshRenderer>().material;
        colorMaterial.color = baseColor;
        gm = FindObjectOfType<GameManager>();
        mm = FindObjectOfType<MoneyMaker>();

    }

    private void Update()
    {

        if(level == 2 && up == false)
        {
            moneyRate += 3;
            up = true;
        }

        if(LineRenderer1 == null || LineRenderer2 == null && isLinked == true)
        {
            nbLink = 1;
        }

        if(LineRenderer1 == null && LineRenderer2 == null)
        {
            isLinked = false;
            nbLink = 0;
            colorMaterial.color=baseColor;
        }

    }

    private void OnMouseDown()
    {
        if(gm.upgradeMode == true && mm.getTotal()>20)
        {
            level = 2;
            mm.minusUpgrade();
            colorMaterial.color=upgradedColor;
            
        }
    }

    public void setLinked(GameObject lr)
    {
        isLinked = true;
        if (LineRenderer1 == null)
        {
            LineRenderer1 = lr;
        }
        else
        {
            LineRenderer2 = lr;
        }
        
        Debug.Log("isLinked = " + isLinked);
        colorMaterial.color = linkedColor;
    }

    public void addLink()
    {
        if (nbLink < 2)
        {
            this.nbLink++;
            Debug.Log("nombre de links :" + nbLink);
        }
    }

    public int getLink()
    {
        return nbLink;
    }

    public float getMoneyRate()
    {
        return moneyRate;
    }

    public void setMoneyRate(float val)
    {
        moneyRate = val;
    }

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float val)
    {
        health -= val;
    }

}
