using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable _interactObject;
    public GameObject attackPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _interactObject = other.GetComponent<IInteractable>();
            _interactObject?.Enter();
        }
        if (other.gameObject.layer == 7)
        {
            attackPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _interactObject?.Exit();
            _interactObject = null;
        }
        if (other.gameObject.layer == 7)
        {
            attackPanel.SetActive(false);
        }
    }
}
