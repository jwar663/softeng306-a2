using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public Image previousItem;
    public Image currentItem;
    public Image nextItem;
    public TextMeshProUGUI itemName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setItems(Item previous, Item current, Item next) {
        previousItem.sprite = previous.useOtherSprite ? previous.otherSprite : previous.sprite;
        currentItem.sprite = current.useOtherSprite ? current.otherSprite : current.sprite;
        nextItem.sprite = next.useOtherSprite ? next.otherSprite : next.sprite;
        itemName.text = current.name;
    }
}
