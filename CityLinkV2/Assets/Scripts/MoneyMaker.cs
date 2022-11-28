using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyMaker : MonoBehaviour
{
    [SerializeField] public List<GameObject> allTowns = new List<GameObject>();
    [SerializeField] public Text moneyText;
    [SerializeField] public Text scoreText;
    [SerializeField] public Text bestScoreText;

    public Text score;
    public Text highScore;

    private float totalMoneyRate;
    private float time;

    private float moneyTotal = 15f;

    private float moneyRate = 2f;



    private void Start()
    {
        StartCoroutine(TownProduction());
        highScore.text = PlayerPrefs.GetFloat("HighScore", 0).ToString();

    }

    public void addTown(GameObject t)
    {
        allTowns.Add(t);
    }

    private void Update()
    {
        
    }

    private void setRate()
    {
        totalMoneyRate = 0;
        for (int i = 0; i < allTowns.Count; i++)
        {
            if (allTowns[i].GetComponent<Town>().getLink() > 0)
            {
                totalMoneyRate += allTowns[i].GetComponent<Town>().getMoneyRate();
            }

        }
    }

    IEnumerator TownProduction()
    {
        yield return new WaitForSeconds(2);
        setRate();
        moneyTotal += totalMoneyRate;
        moneyText.GetComponent<Text>().text = moneyTotal.ToString();
        scoreText.GetComponent<Text>().text = Mathf.Round(moneyTotal).ToString();
        StartCoroutine(TownProduction());
    }

    public float getTotal()
    {
        return moneyTotal;
    }
    public void minusUpgrade()
    {
        moneyTotal -= 20f;
    }

    public void minusLine()
    {
        moneyTotal -= 15f;
    }

    public void setHighScore()
    {
        score.text = moneyTotal.ToString();

        PlayerPrefs.SetFloat("HighScore", moneyTotal);
    }

}
