using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ScoreScreenController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI timeBonus;
    public TextMeshProUGUI total;
    public string theName;
    public GameObject inputField;
    public TextMeshProUGUI textDisplay;

    void Start()
    {
        SortedDictionary<int, string> highScores = GameManager.getInstance().highScores;
        Debug.Log("Starting the game");
        Debug.Log(GameManager.getInstance().score.ToString());
        score.text = GameManager.getInstance().score.ToString();
        int bonus = (int)((2000 - (GameManager.getInstance().time))/200);
        if (bonus < 0)
        {
            bonus = 0;
        }
        timeBonus.text = bonus.ToString();
        total.text = (bonus + GameManager.getInstance().score).ToString();
        textDisplay.text = highScores[highScores.Keys.Last()] + ", " + highScores.Keys.Last().ToString();
        Debug.Log("Current High score by " + highScores[highScores.Keys.Last()] + highScores.Keys.Last().ToString());

    }

    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        //textDisplay.GetComponent<Text>().text = "High score by: " + theName;
        SortedDictionary<int, string> highScores = GameManager.getInstance().highScores;
        if (highScores.Keys.Last() <= GameManager.getInstance().score)
        {
            textDisplay.text = theName + GameManager.getInstance().score.ToString();
        }
        GameManager.getInstance().setScore(GameManager.getInstance().score, theName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
