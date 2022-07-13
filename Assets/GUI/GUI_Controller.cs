using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GUI_Controller : MonoBehaviour
{

    public void StartScene()
    {
        SceneManager.LoadScene(0);//Level01
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void RestartScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//當下關卡
        SceneManager.LoadScene(0);//Level01(關卡名/序列)
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void QuitOption()
    {
        Application.Quit();//Leave game

        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
