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
        PlayStepJumpOrLand(audSorc, staticRef.playerFootstepArray);
    }

    public static void PlayPlayerJump(AudioSource audSorc)
    {
        PlayStepJumpOrLand(audSorc, staticRef.playerJumpArray);
    }

    public static void PlayPlayerLand(AudioSource audSorc)
    {
        PlayStepJumpOrLand(audSorc, staticRef.playerLandArray);
    }

    private static void PlayStepJumpOrLand(AudioSource audSorc, AudioClip[] audClipArray)
    {
        int soundToPlay = (int)Random.Range(0.0f, audClipArray.Length);

        audSorc.PlayOneShot(audClipArray[soundToPlay]);
    }
}