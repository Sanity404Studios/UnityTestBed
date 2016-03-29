using UnityEngine;
using System.Collections;

public class FootstepManager : MonoBehaviour
{
    
    public AudioClip[] playerFootstepArray;

    private static FootstepManager staticRef;
    

    void Awake()
    {
        staticRef = GetComponent<FootstepManager>();
    }

    public static void PlayPlayerFootstep(AudioSource audSorc)
    {
        int stepToPlay = (int)Random.Range((float)0, (float)staticRef.playerFootstepArray.Length);
        Debug.Log(stepToPlay);

        audSorc.PlayOneShot(staticRef.playerFootstepArray[stepToPlay]);
    }
}