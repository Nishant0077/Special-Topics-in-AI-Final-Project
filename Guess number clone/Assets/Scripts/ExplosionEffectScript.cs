using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffectScript : MonoBehaviour {

    public GameObject effect;
   public float time = 2;

	// Use this for initialization
	void Start () {

        
	}
	
	public void Explode()
    {
        
        Instantiate(effect, gameObject.transform.position, gameObject.transform.rotation);
        
    }

    public void ExplodeOnContact()
    {
        Vector3 newPos = new Vector3(this.transform.position.x, this.transform.position.y - 2, this.transform.position.z);
        Instantiate(effect, newPos, gameObject.transform.rotation);

    }
}
