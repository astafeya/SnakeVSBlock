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
}
