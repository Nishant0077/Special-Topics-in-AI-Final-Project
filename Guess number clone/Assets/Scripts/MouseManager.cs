using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseManager : MonoBehaviour
{

    public GameObject selectedObject;
    public int r = 0, g = 0, b = 0;
    Color32 selectionColor = new Color(255, 215, 0);
    public Transform indicator;
    GameController gc;
   

    // Use this for initialization
    void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();
    }

    
    // Update is called once per frame
    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.Log("Mouse is over: " + hitInfo.collider.name );

            // The collider we hit may not be the "root" of the object
            // You can grab the most "root-est" gameobject using
            // transform.root, though if your objects are nested within
            // a larger parent GameObject (like "All Units") then this might
            // not work.  An alternative is to move up the transform.parent
            // hierarchy until you find something with a particular component.

            GameObject hitObject = hitInfo.transform.root.gameObject;

           // SelectObject(hitObject);
        }


        if (Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = hitInfo.transform.gameObject;

            if (hitObject.tag == "Alien")
            {

                SelectObject(hitObject);
                
                Debug.Log(hitObject.tag);
                
            }
        }

        gc.SetCurrentSelectedObject(selectedObject);
        

    }
    

   

    void SelectObject(GameObject obj)
    {

        gc.input.ActivateInputField();

        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;

            ClearSelection();
        }

        selectedObject = obj;
        gc.SetCurrentSelectedObject(selectedObject);
        Debug.Log(gc.currentSelection.tag + "this is current selection");
        gc.currentImageFromSelection = gc.currentSelection.GetComponent<SpriteRenderer>().sprite;

        

        Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            Material m = r.material;
            m.color = selectionColor;
            r.material = m;

        }

        if (selectedObject.tag == "Alien")
        {
            indicator = selectedObject.gameObject.transform.GetChild(0);
            indicator.gameObject.SetActive(true);
        }
    }

    void ClearSelection()
    {
        

        if (selectedObject == null)
            return;

        Renderer[] rs = selectedObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            Material m = r.material;
            m.color = Color.white;
            r.material = m;
        }

        indicator = selectedObject.gameObject.transform.GetChild(0);
        indicator.gameObject.SetActive(false);
        selectedObject = null;
        

    }
}
