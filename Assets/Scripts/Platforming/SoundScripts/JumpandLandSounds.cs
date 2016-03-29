using UnityEngine;
using System.Collections;

public class JumpandLandSounds : MonoBehaviour {

    public AudioClip jumpSound;
    public AudioClip landSound;

    private static JumpandLandSounds staticRefToSelf;

    void Awake()
    {
        staticRefToSelf = GetComponent<JumpandLandSounds>();
    }

    public static void DoJumpSound(AudioSource audSource)
    {
        audSource.PlayOneShot(staticRefToSelf.jumpSound);
    }
    public static void DoLandSound(AudioSource audSource)
    {
        audSource.PlayOneShot(staticRefToSelf.landSound);
    }
}