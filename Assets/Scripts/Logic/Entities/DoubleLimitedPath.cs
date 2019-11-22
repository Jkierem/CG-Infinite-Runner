using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleLimitedPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MonoBehaviour[] children = GetComponentsInChildren<MonoBehaviour>();
        int p = Random.Range(0,2);
        float dir = Mathf.Pow(-1, p);
        foreach( MonoBehaviour g in children ){
            if( g.tag == Constants.CoinTag ){
                g.transform.position += new Vector3(0.7f,0,0) *  dir;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
