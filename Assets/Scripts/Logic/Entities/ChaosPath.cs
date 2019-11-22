using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosPath : MonoBehaviour
{
    [Min(1)]
    public int obstacles = 5;
    public bool randomObstacles = false;
    [Min(1)]
    public int minObstacles = 1;
    public int maxObstacles = 10;
    public GameObject obstacle;
    public GameObject coin;

    private void createCoin(float x, float y, float z){
        Instantiate(coin, new Vector3(x,y,z) + this.transform.position, Quaternion.Euler(90,0,0));
    }

    private void createObstacle(float x, float y, float z){
        Instantiate(obstacle, new Vector3(x,y,z) + this.transform.position, Quaternion.identity);
    }

    void Start()
    {
        if( obstacle == null ){
            obstacle = Resources.Load("Prefabs/MovingObstacle", typeof(GameObject)) as GameObject;
        }
        if( randomObstacles ){
            obstacles = Random.Range(minObstacles,maxObstacles+1);
        }
        for( int i = 0 ; i < obstacles ; i++ ){
            float x = Random.Range(-3.1f , 3.1f);
            float y = 0;
            float z = 27;
            createObstacle(x,y,z);
        }
        float xc = Random.Range(-2f , 2f);
        float zc = Random.Range(15,22);
        createCoin(xc,1,zc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
