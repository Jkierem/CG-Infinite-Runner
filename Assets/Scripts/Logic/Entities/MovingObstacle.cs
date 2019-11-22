using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 direction = Vector3.back;
    public float speed = 2.0f;
    [Tooltip("If set to true, speed attribute is ignored and a random speed is asigned. The speed limited by the minSpeed and maxSpeed attributes")]
    public bool randomSpeed = false;
    public float minSpeed = 1;
    public float maxSpeed = 10;
    public float height = 0.5f;
    [Range(0,2*Mathf.PI)]
    public float deltaT = 0.1f;
    private float t = 0;

    Vector3 getMovement(){
        return direction * Time.deltaTime * speed;
    }

    Vector3 getPosition(){
        Vector3 pos = transform.position;
        float y = Mathf.Abs(Mathf.Sin(t));
        Vector3 xzProjection = new Vector3(1,0,1);
        Vector3 yVector = new Vector3(0,y,0) * height;
        return Vector3.Scale(pos , xzProjection) + yVector + getMovement();
    }
    void Start() 
    {
        t += Random.Range(0, 0.25f * Mathf.PI);
        if( randomSpeed ){
            speed = Random.Range(minSpeed,maxSpeed);
        }
    }

    void Update()
    {
        t += deltaT;
        transform.position = getPosition();
    }
}
