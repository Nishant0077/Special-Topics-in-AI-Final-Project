using UnityEngine;
using System.Collections;

public class SelectionIndicator : MonoBehaviour {

	MouseManager mm;
    public float XOffset = 0;
    public float sizeOffset = 0;

	// Use this for initialization
	void Start () {
		mm = GameObject.FindObjectOfType<MouseManager>();
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(mm.selectedObject != null) {

           // Debug.Log(mm.selectedObject.tag);
			Bounds bigBounds = mm.selectedObject.GetComponent<SpriteRenderer>().bounds;
           // Debug.Log(bigBounds.size);

            // This "diameter" only works correctly for relatively circular or square objects
            float diameter = bigBounds.size.z;
            diameter *= 1.25f;

            // Transform parentOfIndicator = this.transform.parent;
            sizeOffset = 6;
            XOffset = (bigBounds.size.x * sizeOffset) + 5f;
           // Debug.Log(XOffset);
            this.transform.position = new Vector3(bigBounds.center.x + 43, bigBounds.center.y, bigBounds.center.z);
            this.transform.localScale = new Vector3(bigBounds.size.x + 5, bigBounds.size.y + 5, bigBounds.size.z);

        }
	}
}
 