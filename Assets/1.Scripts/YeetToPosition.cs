using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.Windows.WebCam;

public class YeetToPosition : MonoBehaviour
{
    private Vector3 startMarker;
    private Transform endMarker;

    // Movement speed in units per second.
    private float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private bool startYeet;

    private bool fireAndForget;
    private float offset;
    public void YeetInit(Vector3 start, Transform end, float s, float poffset = 0, bool forget = true)
    {
        startMarker = start;
        endMarker = end;
        speed = s;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(start, new Vector3(end.position.x, end.position.y, end.position.z - poffset));
        fireAndForget = forget;
        offset = poffset;
    }

    private JoyconDemo.YeetEvent finalStateEvent;
    private int points;

    public void Yeet(int ppoints, JoyconDemo.YeetEvent pEvent)
    {
        finalStateEvent = pEvent;
        points = ppoints;
        startYeet = true;
    }

    void Update()
    {
        if (GameFlowManager.Instance.GameOver) return;

        if (!startYeet)
            return;

        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker, new Vector3(endMarker.position.x, endMarker.position.y, endMarker.position.z - offset), fractionOfJourney);

        if (fireAndForget && fractionOfJourney >= 1.0)
        {
            if (finalStateEvent != null)
            {
                finalStateEvent.Invoke(points);
            }
            Destroy(gameObject);
        }
    }
}
