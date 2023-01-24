/* (c) Irina Astafeva, 2023 */

using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string SoundMute
    {
        get => PlayerPrefs.GetString(Constants.SOUND_MUTE, Constants.UNMUTE);
        protected set
        {
            PlayerPrefs.SetString(Constants.SOUND_MUTE, value);
            PlayerPrefs.Save();
        }
    }

    public Constants.State CurrentState { get; protected set; }

    public int LevelIndex
    {
        get => PlayerPrefs.GetInt(Constants.LEVEL_INDEX_KEY, 0);
        protected set
        {
            PlayerPrefs.SetInt(Constants.LEVEL_INDEX_KEY, value);
            PlayerPrefs.Save();
        }
    }
}
