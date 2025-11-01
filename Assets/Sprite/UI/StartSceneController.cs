using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void OnStartButtonClick() {
        SceneManager.LoadScene("Menu");
    }
    public void OnStartGameButtonClick() {
        SceneManager.LoadScene("GameScene");
    }

}
