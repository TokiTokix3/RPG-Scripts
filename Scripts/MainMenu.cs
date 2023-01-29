using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerCharacter player;
    public void PlayGame()
    {
        player.actions[1] = null;
        player.actions[2] = null;
        player.actions[3] = null;
        SceneManager.LoadScene("Overworld Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
