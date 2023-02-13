/* (c) Irina Astafeva, 2023 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : PlayerData
{
    public SoundController SoundController;
    public UIController UIController;

    private float _startZ;
    private float _maxReachedZ;
    private float _endZ;


    private void Awake()
    {
        SoundController.SetSoundMute(IsMute());
        UIController.SetLevel(LevelIndex + 1);
        CurrentState = Constants.State.Playing;
    }

    private void Start()
    {
        _startZ = 0;
        _maxReachedZ = 0;
        GameObject finish = GameObject.Find("Finish");
        _endZ = finish.transform.position.z;
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

    public void DoMute(bool isMute)
    {
        if (isMute) SoundMute = Constants.MUTE_KEY;
        else SoundMute = Constants.UNMUTE_KEY;
        SoundController.SetSoundMute(IsMute());
    }

    public void ChangeLifes(int value)
    {
        PlayerLifes += value;
    }

    public void SetProgress(float maxReachedZ)
    {
        _maxReachedZ = Mathf.Max(_maxReachedZ, maxReachedZ);
        UIController.SetProgress(Mathf.InverseLerp(_startZ, _endZ, _maxReachedZ));
    }
}
