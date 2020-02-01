using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JoyconDemo : MonoBehaviour
{
    public enum HandState
    {
        Idle,
        GoingDown,
        GoingUp,
        Yeeting,
    }

    public List<Joycon> joycons;
    public HandState handState;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    private Vector3 oldAccel;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;
    
    public float descentSpeed = 0.05f;
    public float ascentSpeed = 0.15f;

    private Transform transform;
    public float sensitivity;

    public float MaxDescentPosition;
    public float OriginalYPosition;
    public Material material;
    private Vector2 materialOffset;

    public bool holdingSomethingYeetable = true;
    public float yeetingPower = 0f;

    public float yeetRotMin;
    public float yeetRotMax;
    public bool yeetDirectio;

    public Transform yeetingContrainerTransform;
    public float yeetingRotationMaxTime;
    public float yeetingRotationTimer;

    public Transform ourPosition;
    public Transform opponentPosition;

    void Start()
    {
        transform = gameObject.transform;
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);

        handState = HandState.Idle;
        materialOffset = material.mainTextureOffset;
        materialOffset.y = -0.5f;

        material.mainTextureOffset = materialOffset;

        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // make sure the Joycon only gets checked if attached
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];

            // GetButtonDown checks if a button has been pressed (not held)
            //if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            //{
            //    Debug.Log("Shoulder button 2 pressed");
            //    // GetStick returns a 2-element vector with x/y joystick components
            //    Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}", j.GetStick()[0], j.GetStick()[1]));

            //    // Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
            //    j.Recenter();
            //}

            if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                Debug.Log("Rumble");

                // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
                // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

                j.SetRumble(160, 320, 0.6f, 200);

                // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
                // Then call SetRumble(0,0,0) when you want to turn it off.
            }

            if (j.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                Debug.Log("Rumble");

                // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
                // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

                int low_freq = Random.Range(100, 150);
                int high_freq = Random.Range(320, 500);
                float amp = Random.Range(0.5f, 1f);
                int time = Random.Range(100, 500);
                j.SetRumble(low_freq, high_freq, amp, time);

                // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
                // Then call SetRumble(0,0,0) when you want to turn it off.
            }

            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            oldAccel = accel;
            accel = j.GetAccel();

            orientation = j.GetVector();
            var position = transform.position;
            float sensitivity = this.sensitivity / 1000;
            position.x += stick[0] * sensitivity;
            position.z += stick[1] * sensitivity;

            ClampPosition(ref position);

            if (handState == HandState.Idle)
            {
                if (j.GetButtonDown(Joycon.Button.SHOULDER_1))
                {
                    handState = HandState.GoingDown;
                }
                else if (holdingSomethingYeetable)
                {
                    if (j.GetButton(Joycon.Button.SHOULDER_2))
                    {
                        yeetingPower += (oldAccel - accel).magnitude;

                        var yeetNormalized = Mathf.Clamp01(yeetingPower / 500);
                        var yoteMax = Mathf.Lerp(80, 320, yeetNormalized);
                        var yoteMin = Mathf.Lerp(80, 160, yeetNormalized);
                        j.SetRumble(yoteMin, yoteMax, 0.6f);
                    }
                    else if (j.GetButtonUp(Joycon.Button.SHOULDER_2) && yeetingPower >= 25)
                    {
                        //Debug.LogFormat("Yoting {0}", yeetingPower);
                        handState = HandState.Yeeting;

                        var yeetNormalized = Mathf.Clamp01(yeetingPower / 500);
                        var yoteMax = Mathf.Lerp(80, 320, yeetNormalized);
                        var yoteMin = Mathf.Lerp(80, 160, yeetNormalized);
                        j.SetRumble(yoteMin, yoteMax, 0.6f, 200);

                        yeetingPower = 0;
                    }
                }
            }

            if (handState == HandState.GoingDown)
            {
                position.y -= descentSpeed * Time.deltaTime;
                var positionTraveled = Mathf.InverseLerp(MaxDescentPosition, OriginalYPosition, position.y);
                materialOffset.y = GetTextureOffsetModifier(positionTraveled);
                //Debug.LogFormat("GoingDown: Position Traveled = {0} => Mat offset = {1}", positionTraveled, materialOffset.y);
                material.mainTextureOffset = materialOffset;

                if (position.y <= MaxDescentPosition)
                {
                    position.y = MaxDescentPosition;
                    DetectObjectCollision();
                    handState = HandState.GoingUp;

                    materialOffset.y = 0;
                    material.mainTextureOffset = materialOffset;
                }
            }
            else if (handState == HandState.GoingUp)
            {
                position.y += ascentSpeed * Time.deltaTime;
                var positionTraveled = Mathf.InverseLerp(MaxDescentPosition, OriginalYPosition, position.y);
                materialOffset.y = GetTextureOffsetModifier(positionTraveled);
                //Debug.LogFormat("GoingUp: Position Traveled = {0} => Mat offset = {1}", positionTraveled, materialOffset.y);
                material.mainTextureOffset = materialOffset;

                if (position.y >= OriginalYPosition)
                {
                    position.y = OriginalYPosition;
                    HandleMerge();
                    handState = HandState.Idle;

                    materialOffset.y = -0.5f;
                    material.mainTextureOffset = materialOffset;
                }
            }
            else if (handState == HandState.Yeeting)
            {
                var rotation = yeetingContrainerTransform.rotation;
                var euler = rotation.eulerAngles;
                yeetingRotationTimer += Time.deltaTime;
                var delta = Mathf.Clamp01(yeetingRotationTimer / yeetingRotationMaxTime);
                euler.y = Mathf.Lerp(yeetRotMin, yeetRotMax, delta);
                rotation.eulerAngles = euler;
                yeetingContrainerTransform.rotation = rotation;

                if (delta >= 1)
                {
                    handState = HandState.Idle;
                    yeetingRotationTimer = 0f;
                }
            }

            transform.position = position;
        }
    }

    private float GetTextureOffsetModifier(float positionTraveled)
    {
        return Mathf.Lerp(0f, -0.5f, positionTraveled);
    }

    private void DetectObjectCollision()
    {
        Debug.Log("Detect object collision");
    }

    private void HandleMerge()
    {
        Debug.Log("Handle merge");
    }

    private void ClampPosition(ref Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, -13f, 13f);
        position.z = Mathf.Clamp(position.z, -13f, 13f);
    }
}