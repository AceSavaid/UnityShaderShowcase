using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextZone : MonoBehaviour
{
    [SerializeField] string text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.GetComponent<Player>().ShowTextBox(text);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.GetComponent<Player>().HideTextBox();

        }
    }

}
