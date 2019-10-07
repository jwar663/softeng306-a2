using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForrestGameController : NPCBehaviour
{
    public NPC npc;
    private bool notRun = true;
    public Transform portal;
    public Transform burningTree;
    public Transform largeFire;
    public Transform smallFire;
    public Camera camera;
    private bool isCompleted = true;

    public override void dialogueCompleted()
    {
        isCompleted = true;
    }

    public override void interact(NPC npc)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Run");
    }

    // Update is called once per frame
    void Update()
    {
        
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
        dialogue.Add("Rainforest play a huge role in regulating the worlds CO2 levels.");
        dialogue.Add("Each year we humans set them on fire to make way for agriculatural land.");
        dialogue.Add("If we dont put a stop to this, we wont have any rainforests left.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue);
        isCompleted = false;
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }

        for (int i = 0; i<200; i++)
        {
            moveCamera(camera.transform.position, portal.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("You can use that portal to leave the area.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue);
        isCompleted = false;
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }

        for (int i = 0; i < 200; i++)
        {
            moveCamera(camera.transform.position, largeFire.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("The portal is being guarded by that fire monster.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue);
        isCompleted = false;
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }

        for (int i = 0; i < 200; i++)
        {
            moveCamera(camera.transform.position, burningTree.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("You must put out the burning trees to weaken the fire monster.");
        dialogue.Add("If you put out enough fires, he will be too weak to continue burning and go out.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue);
        isCompleted = false;
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }

        for (int i = 0; i < 200; i++)
        {
            moveCamera(camera.transform.position, smallFire.position, camera);
            yield return null;
        }
        dialogue = new List<string>();
        dialogue.Add("Try and avoid these smaller fire monsters, they will hurt you.");
        FindObjectOfType<DialogueTrigger>().TriggerDialogue(npc, dialogue);
        isCompleted = false;
        yield return null;
        while (!isCompleted)
        {
            yield return null;
        }

        camera.GetComponent<CameraMovement>().enabled = true;
    }
}
