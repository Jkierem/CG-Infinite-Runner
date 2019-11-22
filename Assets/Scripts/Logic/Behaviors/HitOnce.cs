using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOnce : MonoBehaviour
{
    void OnTriggerExit(){
        Collider[] colls = GetComponents<Collider>();
        foreach( Collider c in colls ){
            if( c.tag == Constants.ObstacleTag ){
                c.enabled = false;
            }
        }
    }
}
