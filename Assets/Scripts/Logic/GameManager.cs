using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : ScriptableObject
{
    void Awake()
    {
        this.init();
    }

    void init(){
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
    
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameManager)CreateInstance("GameManager");
            }
            return instance;
        }
    }

    private Queue<int> queue;
    protected GameManager()
    {
        GameState = GameState.Playing;
        queue = new Queue<int>();
    }

    public GameState GameState { get; set; }
    
    public void Die()
    {
        this.GameState = GameState.Dead;
        SceneManager.LoadScene("Scenes/MenuScene");
        UIManager.Instance.Reset();
    }

    public void GenerateQueue(){
        queue = PathGenerator.GenerateQueue();
    }

    public static void AddScore(int amount){
        UIManager.Instance.AddScore(amount);
    }

    public void Play(){
        this.GameState = GameState.Playing;
    }

    public int GetNextSegment(){
        if( queue.Count == 0 ){
            GenerateQueue();
        }
        return queue.Dequeue();
    }
}