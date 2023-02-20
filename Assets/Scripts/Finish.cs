/* (c) Irina Astafeva, 2023 */

using UnityEngine;

public class Finish : MonoBehaviour
{
    public ParticleSystem Confetti;

    public void PlayConfetty()
    {
        Confetti.Play();
    }
}
