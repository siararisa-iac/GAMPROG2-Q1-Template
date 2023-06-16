using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastController : MonoBehaviour
{
    [SerializeField]
    private float raycastDistance = 5.0f;

    [SerializeField]
    //The layer that will determine what the raycast will hit
    LayerMask layerMask;
    //The UI text component that will display the name of the interactable hit
    public TextMeshProUGUI interactionInfo;

    // Update is called once per frame
    private void Update()
    {
        if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out RaycastHit hitinfo, raycastDistance, layerMask))
        {
            //Debug.Log("Hit Something");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.green);

            Interactable interactable = hitinfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactionInfo.gameObject.SetActive(true);
                interactionInfo.text = interactable.id;

                if (Input.GetMouseButtonDown(0))
                {
                    interactable.Interact();
                }
            }
            else
            {
                interactionInfo.gameObject.SetActive(false);
            }

        }
        else
        {
            //Debug.Log("Hit Nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
            interactionInfo.gameObject.SetActive(false);
        }
        //TODO: Raycast
        //1. Perform a raycast originating from the gameobject's position towards its forward direction.
        //   Make sure that the raycast will only hit the layer specified in the layermask
        //2. Check if the object hits any Interactable. If it does, show the interactionInfo and set its text
        //   to the id of the Interactable hit. If it doesn't hit any Interactable, simply disable the text
        //3. Make sure to interact with the Interactable only when the mouse button is pressed.
    }
 
}