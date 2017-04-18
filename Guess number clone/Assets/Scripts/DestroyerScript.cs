using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerScript : MonoBehaviour {

    AudioClip explosionSound;
    AudioSource audio;
    RandomImageDisplay randomDisplayObject;

    private void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();
        explosionSound = audio.clip;
        randomDisplayObject = GameObject.FindObjectOfType<RandomImageDisplay>();
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Alien"))
        {
            if (other.gameObject.transform.parent)
            {
                audio.PlayOneShot(explosionSound);
                //other.transform.root.transform.gameObject.SetActive(false);
                //other.gameObject.transform.parent.gameObject.SetActive(false);
                this.gameObject.GetComponent<ExplosionEffectScript>().Explode();
                Destroy(other.transform.root.transform.gameObject);
                Destroy(other.gameObject.transform.parent.gameObject);
                //   Debug.Log("Collision!!!");
               

            }


            else
            {
                //  other.gameObject.SetActive(false);
                // other.transform.root.transform.gameObject.SetActive(false);
                other.GetComponent<ExplosionEffectScript>().ExplodeOnContact();
                audio.PlayOneShot(explosionSound);
                Destroy(other.transform.root.transform.gameObject);
                // Debug.Log("Collision!!!");                


            }
        }
    }
}
