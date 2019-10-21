using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilGameController : NPCBehaviour
{

    public Camera camera;
    public ShipController player;
    public int score;
    public Text scoreText;
    int totDucks;
    int totSpills;
    public GameObject portal;
    public NPC npc;

    private bool notRun = true;
    public Transform burningTree;
    public Transform largeFire;
    public Transform smallFire;
    private bool isCompleted = true;
    private bool allCutsceneDone = false;

    // Start is called before the first frame update
    void Start()
    {
        totSpills = GameObject.FindGameObjectsWithTag("Oil Field").Length;
        totDucks = GameObject.FindGameObjectsWithTag("Duck").Length;
        StartCoroutine("Run");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.getInstance() == null)
        {
            (new GameObject("GameManager")).AddComponent<GameManager>();
        }

        int remainingOil = GameObject.FindGameObjectsWithTag("Oil Field").Length; ;
        scoreText.text = "Oil spills left: " + remainingOil;
        GameManager.getInstance().score = (remainingOil*250);
        GameManager.getInstance().time = Time.timeSinceLevelLoad;

        if (GameObject.FindGameObjectsWithTag("Oil Field").Length == 0)
        {
            GameManager.getInstance().levelsCompleted = 2;
            GameManager.getInstance().score = (remainingOil * 250) + GameObject.FindGameObjectsWithTag("Duck").Length*300;
            portal.SetActive(true);

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            StopCoroutine("Run");
            allCutsceneDone = true;
            FindObjectOfType<DialogueManager>().EndDialogue();
            camera.GetComponent<CameraMovement>().enabled = true;
            player.setCanMove(true);
        }
    }

    public override void dialogueCompleted()
    {
        isCompleted = true;
    }

    void moveCamera(Vector3 cameraPos, Vector3 newPos, Camera camera)
    {
        newPos.z = cameraPos.z;
        camera.transform.position = Vector3.Lerp(cameraPos, newPos, Time.deltaTime);
    }

   

    IEnumerator Run()
    {
        player.setCanMove(false);
        camera.GetComponent<CameraMovement>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        List<string> dialogue = new List<string>();
        dialogue.Add("This is the Gulf Ocean.");
        dialogue.Add("Home to millions of different species.");
        dialogue.Add("Unfortunately there has been an Oil spill.");
        dialogue.Add("Oil is very toxic to the animals living here, you must clean up the ocean before animals start dying!");


        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue, false);
        isCompleted = false;
        player.setCanMove(false);
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }
        player.setCanMove(false);

        for (int i = 0; i < 200; i++)
        {
            Debug.Log("moving camera");
            moveCamera(camera.transform.position, portal.transform.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("A portal will appear hear once you are done.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue, false);
        isCompleted = false;
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }
        player.setCanMove(false);

        for (int i = 0; i < 200; i++)
        {
            moveCamera(camera.transform.position, largeFire.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("These poor ducks are covered in oil, make sure to save as many as you can.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue, false);
        isCompleted = false;
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }
        player.setCanMove(false);

        for (int i = 0; i < 200; i++)
        {
            moveCamera(camera.transform.position, burningTree.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("Avoid these at all cost, they will damage your boat.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue, false);
        isCompleted = false;
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }

        camera.GetComponent<CameraMovement>().enabled = true;
        allCutsceneDone = true;
        dialogue = new List<string>();
        player.setCanMove(true);
    }

    public override void interact(NPC npc)
    {
    }
}
