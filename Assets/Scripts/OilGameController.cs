using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilGameController : MonoBehaviour
{

    public Camera camera;
    private Player player;
    public int score;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        this.score = 10;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Oil spills left: " + score.ToString();
        GameManager.getInstance().score = 10 - score;
        GameManager.getInstance().time = Time.timeSinceLevelLoad;
    }
}
