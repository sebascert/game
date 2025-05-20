using System;

using UnityEngine;

class GameMenuSytem : GameSystem
{
    // ascending rendering order
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject configMenu;

    public override void Init()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        configMenu.SetActive(false);
    }

    public void EnableMainMenu(bool value)
    {
        if (value && pauseMenu.activeSelf)
            pauseMenu.SetActive(false);

        mainMenu.SetActive(value);
    }
    public void EnablePauseMenu(bool value)
    {
        if (mainMenu.activeSelf)
            return;
        pauseMenu.SetActive(value);
    }
    public void EnableConfigMenu(bool value)
    {
        // no direct access to the configs menu
        // if (!mainMenu.activeSelf && !pauseMenu.activeSelf)
        //     return
        mainMenu.SetActive(value);
    }
}