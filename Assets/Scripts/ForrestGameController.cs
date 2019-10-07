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
        yield return new WaitForSeconds(1f);
        List<string> dialogue = new List<string>();
        dialogue.Add("Look at the world.....");
        dialogue.Add("....Look at what climate change has done.");
        dialogue.Add("...it has destroyed the world irreversably.");
        dialogue.Add("But there is still hope.");
        dialogue.Add("You must travel back in time to climate disasters and stop them from ever happening");
        dialogue.Add("You are humanity's last hope!");
        dialogue.Add("Good luck!");
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
        dialogue.Add("Look at the world.....");
        dialogue.Add("....Look at what climate change has done.");
        dialogue.Add("...it has destroyed the world irreversably.");
        dialogue.Add("But there is still hope.");
        dialogue.Add("You must travel back in time to climate disasters and stop them from ever happening");
        dialogue.Add("You are humanity's last hope!");
        dialogue.Add("Good luck!");
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
