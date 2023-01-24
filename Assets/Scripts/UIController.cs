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
    public Player Player;

    private void Awake()
    {
        SetWinPanel(false);
        SetLosePanel(false);
        SetSound(Player.IsMute());
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
        if (progress < 0 || progress > 1) return;
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
        bool isMute = Player.IsMute();
        Player.DoMute(!isMute);
        SetSound(!isMute);
    }

    public void OnRestartButtonClick()
    {

    }

    public void OnNextLevelButtonClick()
    {

    }
}
