using System;

using UnityEngine;

class GameMenuSytem : GameSystem<GameMenuSytem>
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
        mainMenu.SetActive(value);
    }
    public void EnablePauseMenu(bool value)
    {
        pauseMenu.SetActive(value);
    }
    public void EnableConfigMenu(bool value)
    {
        mainMenu.SetActive(value);
    }
}