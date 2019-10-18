using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerShoot playerShoot;
    public MouseLook mouseLook;

    public GameObject pauseScreen;
    public GameObject playerUI;

    public void Start()
    {
        PauseGame();
        Cursor.visible = true;
        playerManager.canLook = false;
        mouseLook.canLook = false;
        playerShoot.canShoot = false;
        pauseScreen.SetActive(false);
        playerUI.SetActive(false);
        isFullscreen = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void OpenPauseMenu()
    {
        PauseGame();
        Cursor.visible = true;
        playerManager.canLook = false;
        mouseLook.canLook = false;
        playerShoot.canShoot = false;
        pauseScreen.SetActive(true);
        playerUI.SetActive(false);
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        playerManager.canLook = true;
        mouseLook.canLook = true;
        playerShoot.canShoot = true;
        pauseScreen.SetActive(false);
        playerUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("GameExit");
        Application.Quit();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }

    #region Options
    public bool isFullscreen;

    public void SetFullscreen()
    {
        Screen.fullScreen = !isFullscreen;
        Debug.Log("Fullscreen Toggled");
    }

    public void SetMasterVolume(float value)
    {
        //AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }
    #endregion
}