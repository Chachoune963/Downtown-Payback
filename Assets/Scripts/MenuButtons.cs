using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public GameObject OptionMenu;

    public void PlayGame()
    {
        Debug.Log("PLAYGAME :)");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitQame()
    {
        Application.Quit();
    }

 


}
