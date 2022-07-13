using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GUI_Controller : MonoBehaviour
{

    public void StartScene()
    {
        SceneManager.LoadScene(0);//Level01
    }

    public void RestartScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);//當下關卡
        LevelManagerInit.instance.ClearAll();
        SceneManager.LoadScene(0);//Level01(關卡名/序列)
    }

    public void QuitOption()
    {
        Application.Quit();//Leave game
    }
}
