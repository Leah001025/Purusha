using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable _interactObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _interactObject = other.GetComponent<IInteractable>();
            _interactObject?.Enter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _interactObject?.Exit();
        _interactObject = null;
    }
}
