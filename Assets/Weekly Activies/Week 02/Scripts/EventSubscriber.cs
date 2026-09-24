using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSubscriber : MonoBehaviour
{
    private void OnEnable()
    {
        // // Subscribe to events we want to listen to
        // if (EventAnnouncer.onSpaceBarPressed != null)
        // {
        //     EventAnnouncer.onSpaceBarPressed.AddListener(PrintConsoleMessage);
        // }
        EventAnnouncer.onSpaceBarPressed?.AddListener(PrintConsoleMessage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Checks that this onSpaceBarPressed exists/has unity event attached to it
            EventAnnouncer.onSpaceBarPressed?.Invoke();
        }
    }

    private void OnDisable()
    {
        // Unsubscribe to all events we have subscribed to
        if (EventAnnouncer.onSpaceBarPressed != null)
        {
            EventAnnouncer.onSpaceBarPressed.RemoveListener(PrintConsoleMessage);
        }
    }

    private void PrintConsoleMessage()
    {
        Debug.Log("Hello World");
    }
}
