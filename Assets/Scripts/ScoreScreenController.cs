using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenController : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI timeBonus;
    public TextMeshProUGUI total;
    void Start()
    {
        score.text = GameManager.getInstance().score.ToString();
        int bonus = (int)(2000 - (GameManager.getInstance().time / 50.0f * 2000));
        if (bonus < 0)
        {
            bonus = 0;
        }
        timeBonus.text = bonus.ToString();
        total.text = (bonus + GameManager.getInstance().score).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
