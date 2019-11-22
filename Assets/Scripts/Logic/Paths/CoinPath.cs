using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPath : MonoBehaviour
{
    public GameObject coin;
    public GameObject obstacle;
    // Start is called before the first frame update
    private void createCoin(float x, float y, float z){
        Instantiate(coin,new Vector3(x,y,z) + this.transform.position,Quaternion.Euler(90,0,0));
    }

    private void createObstacle(float x, float y, float z){
        float xi = x < -2.1f ? -2.1f : x;
        xi = x > 2.1f ? 2.1f : xi;
        Instantiate(obstacle, new Vector3(xi,y,z) + this.transform.position, Quaternion.identity);
    }
    void Start()
    {
        int type = Random.Range(0,3);
        switch(type){
            case 0:
                straightLine();
                break;
            case 1:
                diagonalLine();
                break;
            case 2:
                jumpingLine();
                break;
        }
    }

    private void straightLine(){
        float x = Random.Range(-3,3);
        float z = 4;
        for( int i = 0 ; i < 5 ; i++ ){
            float zi = z + 2*i;
            createCoin(x,1,zi);
        }
        createObstacle(x,0,14);
    }

    private void diagonalLine(){
        int dir = Random.Range(0,2) == 1? 1 : -1;
        int x = dir * 3;
        float z = 4;
        for( int i = 0 ; i < 5 ; i++ ){
            float xi = x + (-dir * i * 1.2f);
            float zi = z + 2*i;
            createCoin(xi,1,zi);
        }
        createObstacle(x - dir*4.8f,0,14);
    }

    private void jumpingLine(){
        float x = Random.Range(-3,3);
        float y = 1;
        float z = 4;
        for( int i = 0 ; i < 5 ; i++ ){
            float yi =  i < 1 || i == 4 ? y : 2.4f;
            float zi = z + i;
            createCoin(x,yi,zi);
        }
        createObstacle(x,0,7);
    }
    void Update()
    {
        
    }
}
