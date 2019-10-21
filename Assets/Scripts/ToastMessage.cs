using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMessage : MonoBehaviour
{
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.canvasRenderer.SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void show(string s) {
        //collect item sound
        FindObjectOfType<AudioManager>().Play("CollectItem");
        text.text = s;
        text.canvasRenderer.SetAlpha(1f);
        StartCoroutine("hide");
    }
    
    private IEnumerator hide() {
        yield return new WaitForSeconds(1f);
        text.CrossFadeAlpha(0f, 0.5f, false);
    }
}
