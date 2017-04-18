using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderResize : MonoBehaviour {

    Sprite currentSprite;

    public void Start()
    {
        Sprite currentSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    void UpdateCollider()
    {
        gameObject.GetComponent<BoxCollider>().size = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
       // gameObject.GetComponent<BoxCollider>().contactOffset = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.center;
    }

   void Update()
    {
        if (currentSprite != GetComponent<SpriteRenderer>().sprite)
        {
            currentSprite = GetComponent<SpriteRenderer>().sprite;
            UpdateCollider();
        }
    }
}
