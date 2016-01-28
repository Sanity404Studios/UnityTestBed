using UnityEngine;
using System.Collections;

public class DestroyParticleSystem : MonoBehaviour {

    float timeSinceCreated;

	// Update is called once per frame
	void Update () {

        timeSinceCreated += Time.deltaTime;

        if(timeSinceCreated >= 2.50f)
        {
            GameObject.Destroy(gameObject);
        }
	}
}