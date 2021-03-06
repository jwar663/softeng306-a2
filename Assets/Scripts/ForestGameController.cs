﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestGameController : NPCBehaviour, LevelController
{
    public NPC npc;
    private bool notRun = true;
    public Transform portal;
    public Transform burningTree;
    public Transform largeFire;
    public Transform smallFire;
    public Camera camera;
    private bool isCompleted = true;
    private bool allCutsceneDone = false;
    public int score;
    public Text scoreText;
    private Player player;
    public List<NPC> npcs;
    public List<FireTree> fireTrees;
    
    // total number of fire trees
    public int totalFireTrees;
    // number of fire trees to complete level (not all have to be extinguished)
    public int fireTreesTarget;
    // number of fire trees extinguished
    public int fireTreesExtinguished;

    public override void dialogueCompleted()
    {
        isCompleted = true;
        
        if (allCutsceneDone) {
            GameManager.getInstance().items[1].unlocked = true;
            FindObjectOfType<ToastMessage>().show("Received item: Water Gun");
            
            FindObjectOfType<Player>().updateItemView();
        }
    }

    public override void interact(NPC npc)
    {
    }
    
    public List<NPC> getNPCs() {
        return npcs;
    }

    // Start is called before the first frame update
    void Start()
    {
        // stops errors from filling output when forest scene run without running menu scene
        if (GameManager.getInstance() == null) {
            (new GameObject("GameManager")).AddComponent<GameManager>();
        }
        
        player = FindObjectOfType<Player>();
        fireTrees = new List<FireTree>(FindObjectsOfType<FireTree>());
        npcs = new List<NPC>(FindObjectsOfType<NPC>());
        this.score = 9;
        StartCoroutine("Run");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Fires Remaining: " + score.ToString();
        GameManager.getInstance().score = 9-score;
        GameManager.getInstance().time = Time.timeSinceLevelLoad;
        
        if (Input.GetKeyDown(KeyCode.M)) {
            StopCoroutine("Run");
            allCutsceneDone = true;
            FindObjectOfType<DialogueManager>().EndDialogue();
            camera.GetComponent<CameraMovement>().enabled = true;
        }
    }

    void moveCamera(Vector3 cameraPos, Vector3 newPos, Camera camera)
    {
        newPos.z = cameraPos.z;
        camera.transform.position = Vector3.Lerp(cameraPos, newPos, Time.deltaTime);
    }

    IEnumerator Run()
    {
        camera.GetComponent<CameraMovement>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        List<string> dialogue = new List<string>();
        dialogue.Add("Welcome to the rainforest.");
        dialogue.Add("Home to thousands of animal and plant species.");
        dialogue.Add("Rainforests play a huge role in regulating the worlds CO2 levels.");
        dialogue.Add("Each year we humans set them on fire to make way for agriculatural land.");
        dialogue.Add("If you don't put a stop to this, we wont have any rainforests left.");
        dialogue.Add("The lives and homes of millions of animals will all perish.");
        dialogue.Add("Unfortunately, even after the fire, the effects will be felt all over the world.");
        dialogue.Add("Mass amounts of CO2 will be released into the atmosphere, and there will be no forest left to regulate these CO2 levels.");

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
            moveCamera(camera.transform.position, portal.transform.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("You can use that portal to leave the area.");
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
        dialogue.Add("The portal is being guarded by that fire monster.");
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
        dialogue.Add("You must put out the burning trees to weaken the fire monster.");
        dialogue.Add("If you put out enough fires, he will be too weak to continue burning and go out.");
        dialogue.Add("You can interact with the environment by pressing either X and SPACE when you're next to the object.");
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
            moveCamera(camera.transform.position, smallFire.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("Try and avoid these smaller fire monsters, they will hurt you.");
        dialogue.Add("Move around by pressing WASD or the arrow keys!");
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
        dialogue.Add("Please hurry, the future of our Rainforests depend on you.");
        dialogue.Add("Oh, and you'll need this.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue, false);
    }
}
