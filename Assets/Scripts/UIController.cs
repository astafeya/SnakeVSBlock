/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject Mute;
    public GameObject Unmute;
    public Slider Progress;
    public Text CurrentLevel;
    public Text NextLevel;
    public Game Game;

    private void Awake()
    {
        SetWinPanel(false);
        SetLosePanel(false);
        SetSound(Game.IsMute());
    }

    public void SetLevel(int level)
    {
        CurrentLevel.text = level.ToString();
        NextLevel.text = (level + 1).ToString();
    }

    public void SetSound(bool doMute)
    {
        Mute.SetActive(doMute);
        Unmute.SetActive(!doMute);
    }

    public void SetProgress(float progress)
    {
        Progress.value = progress;
    }

    public void SetWinPanel(bool doShown)
    {
        WinPanel.SetActive(doShown);
    }
    
    public void SetLosePanel(bool doShown)
    {
        LosePanel.SetActive(doShown);
    }

    public void OnSoundButtonClick()
    {
        bool isMute = Game.IsMute();
        Game.DoMute(!isMute);
        SetSound(!isMute);
    }

    public void OnRestartButtonClick()
    {
        Game.ReloadLevel();
    }

    public void OnNextLevelButtonClick()
    {
        Game.ReloadLevel();
    }
}
