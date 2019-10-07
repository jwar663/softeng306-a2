using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBehaviour : MonoBehaviour
{   
    public abstract void interact(NPC npc);
    public abstract void dialogueCompleted();
}
