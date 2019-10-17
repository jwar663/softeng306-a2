using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Image previousItem;
    public Image currentItem;
    public Image nextItem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setItems(Sprite previous, Sprite current, Sprite next) {
        previousItem.sprite = previous;
        currentItem.sprite = current;
        nextItem.sprite = next;
    }
}
