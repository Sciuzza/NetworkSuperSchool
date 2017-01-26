using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {



	// Use this for initialization
	void Start () {
        Invoke("DestroyMe",1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
