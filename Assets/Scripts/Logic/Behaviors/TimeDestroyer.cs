using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyer : MonoBehaviour
{

    public GameObject anchor = null;
 
    void Start()
    {
        Invoke("DestroyObject", LifeTime);
    }
 
    void DestroyObject()
    {
        if (GameManager.Instance.GameState != GameState.Dead){
            if( anchor != null ){
                anchor.transform.DetachChildren();
            }
            Destroy(gameObject);
        }

    }
 
    public float LifeTime = 10f;
}