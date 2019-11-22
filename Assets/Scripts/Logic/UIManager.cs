using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    //singleton implementation
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null){
                instance = new UIManager();
            }
            return instance;
        }
    }

    protected UIManager()
    {
    }

    private float score = 0;
    private float lives = 3;


    public void Reset()
    {
        score = 0;
        lives = 3;
        UpdateScoreText();
        UpdateLivesText();
    }
    
    public void SetScore(float value)
    {
        score = value;
        UpdateScoreText();
    }

    public void AddScore(float value)
    {
        score += value;
        UpdateScoreText();
    }
    
    private void UpdateScoreText()
    {
        ScoreText.text = "Score: " + score.ToString();
    }

    public void DecreaseLives(){
        this.lives--;
        this.UpdateLivesText();
        if( lives < 0 ){
            GameManager.Instance.Die();
        }
    }

    private void UpdateLivesText()
    {
        LivesText.text = "Lives: " + lives;
    }

    public Text ScoreText;
    public Text LivesText;

}
