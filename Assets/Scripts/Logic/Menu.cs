using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button PlayButton;
    void Start(){
        PlayButton.onClick.AddListener(OnClick);
    }

    void OnClick(){
        GameManager.Instance.Play();
        SceneManager.LoadScene("Scenes/PlayingScene");
    }
}
