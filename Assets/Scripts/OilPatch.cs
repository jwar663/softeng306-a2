using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPatch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SuckOil()
    {
        StartCoroutine("Run");
    }

    IEnumerator Run()
    {
        for(int i = 20; i>0; i--)
        {
            transform.localScale = new Vector3(0.050f*i, 0.050f * i, 0.050f * i);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
