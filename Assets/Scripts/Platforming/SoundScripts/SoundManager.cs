using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] playerFootstepArray;
    public AudioClip[] playerJumpArray;
    public AudioClip[] playerLandArray;

    private static SoundManager staticRef;


    void Awake()
    {
        staticRef = GetComponent<SoundManager>();
    }

    public static void PlayPlayerFootstep(AudioSource audSorc)
    {
        PlaySound(audSorc, staticRef.playerFootstepArray);
    }

    public static void PlayPlayerJump(AudioSource audSorc)
    {
        PlaySound(audSorc, staticRef.playerJumpArray);
    }

    public static void PlayPlayerLand(AudioSource audSorc)
    {
        PlaySound(audSorc, staticRef.playerLandArray);
    }

    private static void PlaySound(AudioSource audSorc, AudioClip[] audClipArray)
    {
        int soundToPlay = (int)Random.Range(0.0f, audClipArray.Length);

        audSorc.PlayOneShot(audClipArray[soundToPlay]);
    }
}