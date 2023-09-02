using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactorscr : MonoBehaviour
{
    public LayerMask interactLayer;
    // Interactable interactable;
    UnityEvent onInteract;
    public GameObject eInteractUI;
    public GameObject interactObj;
    private string interactType;
    private AudioSource interactSFX;

    private NoteInteraction noteInteraction;

    // Start is called before the first frame update
    void Start()
    {
        noteInteraction = GameObject.Find("NotesUI").GetComponent<NoteInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 7, interactLayer)) // Starting point of raycast, direction, ..., max distance, layer to do raycast on.
        {
            onInteract = hit.collider.GetComponent<Interactable>().onInteract; // Gets function of Interactable script from currently focused interactable.
            interactType = hit.collider.GetComponent<Interactable>().interactType;
            eInteractUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !noteInteraction.exitingNote)
            {
                interactObj = hit.collider.gameObject;
                if (interactType == "note")
                {
                    interactSFX = GameObject.Find("NoteSFX").GetComponent<AudioSource>();
                    noteInteraction.PickUpNote();
                }
                else if (interactType == "lantern")
                {
                    interactSFX = GameObject.Find("LanternSFX").GetComponent<AudioSource>();
                }
                else if (interactType == "knife")
                {
                    interactSFX = GameObject.Find("KnifeSFX").GetComponent<AudioSource>();
                }
                else if (interactType == "door")
                {
                    interactSFX = GameObject.Find("DoorSFX").GetComponent<AudioSource>();
                }
                if (interactSFX != null) { interactSFX.Play(); }
                onInteract.Invoke();
                Debug.Log("Player interacted with: " + interactObj.name);
            }
        }

        else { eInteractUI.SetActive(false); }
    }

    void EquipItem(GameObject item)
    {
        // Figure out how to add to the items array here???
    }
}
