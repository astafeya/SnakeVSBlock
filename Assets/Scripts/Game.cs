/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : PlayerData
{
    public SoundController SoundController;
    public UIController UIController;


    private void Awake()
    {
        SoundController.SetSoundMute(IsMute());
    }

    public void OnPlayerDied()
    {
        if (CurrentState != Constants.State.Playing) return;
        SoundController.PlayLoseSound();
        CurrentState = Constants.State.Loss;
        UIController.SetLosePanel(true);
        PlayerLifes = 4;
        Debug.Log("Game Over!");
    }

    public void OnPlayerWin()
    {
        if (CurrentState != Constants.State.Playing) return;
        SoundController.PlayWinSound();
        CurrentState = Constants.State.Won;
        UIController.SetWinPanel(true);
        LevelIndex++;
        Debug.Log("You Won!");
    }

    public void OnPlayerEat()
    {
        SoundController.PlayEatSound();
    }

    public void OnPlayerPopHead()
    {
        SoundController.PlayPopSound();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsMute()
    {
        return SoundMute == Constants.MUTE_KEY;
    }

    public void DoMute(bool value)
    {
        if (value) SoundMute = Constants.MUTE_KEY;
        else SoundMute = Constants.UNMUTE_KEY;
        SoundController.SetSoundMute(IsMute());
    }

    public void ChangeLifes(int value)
    {
        PlayerLifes += value;
    }
}
