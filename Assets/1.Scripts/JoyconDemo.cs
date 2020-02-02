using RotaryHeart.Lib.PhysicsExtension;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Physics = UnityEngine.Physics;
using Random = UnityEngine.Random;

public class JoyconDemo : MonoBehaviour
{
    public enum HandState
    {
        Idle,
        GoingDown,
        GoingUp,
        ChargingYeet,
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

    public bool holdingSomethingYeetable
    {
        get { return currentMergeableObject != null && currentMergeableObject.Yeetable; }
    }
    public float yeetingPower = 0f;

    public float yeetRotMin;
    public float yeetRotMax;
    public bool yeetDirection;

    public Transform yeetingContrainerTransform;
    public float yeetingRotationMaxTime;
    public float yeetingRotationTimer;

    public Transform ourPosition;

    public JoyconDemo opponent;
    public Transform opponentPosition;
    public LayerMask grabbiesLayerMask;

    public Transform grabbyPoint;
    private MergeableObject currentMergeableObject = null;
    public float grabbyYOriginalPoint;

    public float grabbySpeedIncrement = 1.1f;
    public Transform frontCloud;
    public Transform backCloud;

    private Hover hover1;
    private Hover hover2;

    [System.Serializable]
    public class YeetEvent : UnityEvent<int>
    {
    }

    public YeetEvent onYeetEvent;
    private const int MAX_YEETING_POWER = 500;
    public float yeetRotOriginal;

    void Start()
    {
        transform = gameObject.transform;
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);

        handState = HandState.Idle;
        materialOffset = material.mainTextureOffset;
        materialOffset.y = -0.5f;

        material.mainTextureOffset = materialOffset;

        hover1 = frontCloud.GetComponent<Hover>();
        hover2 = backCloud.GetComponent<Hover>();

        hover1.UpdateRotation = false;
        hover2.UpdateRotation = false;

        // get the public Joycon array attached to the JoyconManager in scene
        //joycons = JoyconManager.Instance.j;
        //if (joycons.Count < jc_ind + 1)
        //{
        //    Destroy(gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFlowManager.Instance.GameOver) return;

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

            //if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
            //{
            //    Debug.Log("Rumble");

            //    // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
            //    // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

            //    j.SetRumble(160, 320, 0.6f, 200);

            //    // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
            //    // (Useful for dynamically changing rumble values.)
            //    // Then call SetRumble(0,0,0) when you want to turn it off.
            //}

            //if (j.GetButtonDown(Joycon.Button.DPAD_LEFT))
            //{
            //    Debug.Log("Rumble");

            //    // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
            //    // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

            //    int low_freq = Random.Range(100, 150);
            //    int high_freq = Random.Range(320, 500);
            //    float amp = Random.Range(0.5f, 1f);
            //    int time = Random.Range(100, 500);
            //    j.SetRumble(low_freq, high_freq, amp, time);

            //    // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
            //    // (Useful for dynamically changing rumble values.)
            //    // Then call SetRumble(0,0,0) when you want to turn it off.
            //}

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

            hover1.UpdateRotation = false;
            hover2.UpdateRotation = false;

            if (handState == HandState.Idle)
            {
                if (j.GetButtonDown(Joycon.Button.SHOULDER_1))
                {
                    handState = HandState.GoingDown;
                }
                else if (holdingSomethingYeetable && j.GetButtonDown(Joycon.Button.SHOULDER_2))
                {
                    handState = HandState.ChargingYeet;
                }
            }

            if (handState == HandState.ChargingYeet)
            {
                if (j.GetButton(Joycon.Button.SHOULDER_2))
                {
                    yeetingPower += (oldAccel - accel).magnitude;
                    yeetingPower = Mathf.Clamp(yeetingPower, 0, MAX_YEETING_POWER);

                    var yeetNormalized = Mathf.Clamp01(yeetingPower / MAX_YEETING_POWER);
                    var yoteMax = Mathf.Lerp(80, 320, yeetNormalized);
                    var yoteMin = Mathf.Lerp(80, 160, yeetNormalized);
                    j.SetRumble(yoteMin, yoteMax, 0.6f);
                }
                else if (j.GetButtonUp(Joycon.Button.SHOULDER_2))
                {
                    //Debug.LogFormat("Yoting {0}", yeetingPower);
                    handState = HandState.Yeeting;

                    var yeetNormalized = Mathf.Clamp01(yeetingPower / MAX_YEETING_POWER);
                    var yoteMax = Mathf.Lerp(80, 320, yeetNormalized);
                    var yoteMin = Mathf.Lerp(80, 160, yeetNormalized);
                    j.SetRumble(yoteMin, yoteMax, 0.6f, 200);

                    var direction = ourPosition.position.x <= opponentPosition.position.x;
                    if (yeetDirection != direction)
                    {
                        yeetRotMin *= -1;
                        yeetRotMax *= -1;
                    }
                    yeetDirection = direction;
                }
            }

            if (handState == HandState.GoingDown)
            {
                position.y -= descentSpeed * Time.deltaTime;

                var grabbyPointPosition = grabbyPoint.localPosition;
                grabbyPointPosition.y -= descentSpeed * grabbySpeedIncrement * Time.deltaTime;
                grabbyPoint.localPosition = grabbyPointPosition;

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

                var grabbyPointPosition = grabbyPoint.localPosition;
                grabbyPointPosition.y += ascentSpeed * grabbySpeedIncrement * Time.deltaTime;

                var positionTraveled = Mathf.InverseLerp(MaxDescentPosition, OriginalYPosition, position.y);
                materialOffset.y = GetTextureOffsetModifier(positionTraveled);
                //Debug.LogFormat("GoingUp: Position Traveled = {0} => Mat offset = {1}", positionTraveled, materialOffset.y);
                material.mainTextureOffset = materialOffset;

                if (position.y >= OriginalYPosition)
                {
                    position.y = OriginalYPosition;
                    handState = HandState.Idle;

                    materialOffset.y = -0.5f;
                    material.mainTextureOffset = materialOffset;
                    grabbyPointPosition.y = grabbyYOriginalPoint;
                }

                grabbyPoint.localPosition = grabbyPointPosition;
            }
            else if (handState == HandState.Yeeting)
            {
                var rotation = yeetingContrainerTransform.rotation;
                var euler = rotation.eulerAngles;
                yeetingRotationTimer += Time.deltaTime;
                var delta = Mathf.Clamp01(yeetingRotationTimer / yeetingRotationMaxTime);

                if (delta >= 1)
                {
                    handState = HandState.Idle;
                    yeetingRotationTimer = 0f;
                    euler.z = yeetRotOriginal;
                    yeetingPower = 0;

                    AudioManager.Instance.PlayYeet();
                }
                else
                {
                    euler.z = Mathf.Lerp(yeetRotMin, yeetRotMax, delta);
                }

                if (delta > 0.8f && currentMergeableObject != null)
                {
                    currentMergeableObject.transform.SetParent(null);
                    currentMergeableObject.hoverScript.UpdateRotation = true;
                    currentMergeableObject.YeetToPosition.YeetInit(currentMergeableObject.transform.position, opponent.backCloud, 30, 0.5f);
                    currentMergeableObject.YeetToPosition.Yeet(Mathf.RoundToInt(currentMergeableObject.Damage * yeetingPower), onYeetEvent);
                    currentMergeableObject = null;
                }

                rotation.eulerAngles = euler;
                yeetingContrainerTransform.rotation = rotation;
            }
            else if (handState == HandState.ChargingYeet)
            {
                hover1.UpdateRotation = true;
                hover2.UpdateRotation = true;
            }

            transform.position = position;
        }
#if UNITY_EDITOR
        else
        {
            var position = transform.position;
            float sensitivity = this.sensitivity / 1000;
            position.x += Input.GetAxis("Horizontal") * sensitivity;
            position.z += Input.GetAxis("Vertical") * sensitivity;

            ClampPosition(ref position);

            hover1.UpdateRotation = false;
            hover2.UpdateRotation = false;

            if (handState == HandState.Idle)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    handState = HandState.GoingDown;
                }
                else if (holdingSomethingYeetable && Input.GetKeyDown(KeyCode.Space))
                {
                    handState = HandState.ChargingYeet;
                }
            }

            if (handState == HandState.ChargingYeet)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    yeetingPower += (oldAccel - accel).magnitude;
                    yeetingPower = Mathf.Clamp(yeetingPower, 0, MAX_YEETING_POWER);

                    var yeetNormalized = Mathf.Clamp01(yeetingPower / MAX_YEETING_POWER);
                    var yoteMax = Mathf.Lerp(80, 320, yeetNormalized);
                    var yoteMin = Mathf.Lerp(80, 160, yeetNormalized);
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    //Debug.LogFormat("Yoting {0}", yeetingPower);
                    handState = HandState.Yeeting;

                    var yeetNormalized = Mathf.Clamp01(yeetingPower / MAX_YEETING_POWER);
                    var yoteMax = Mathf.Lerp(80, 320, yeetNormalized);
                    var yoteMin = Mathf.Lerp(80, 160, yeetNormalized);

                    var direction = ourPosition.position.x <= opponentPosition.position.x;
                    if (yeetDirection != direction)
                    {
                        yeetRotMin *= -1;
                        yeetRotMax *= -1;
                    }
                    yeetDirection = direction;
                }
            }

            if (handState == HandState.GoingDown)
            {
                position.y -= descentSpeed * Time.deltaTime;

                var grabbyPointPosition = grabbyPoint.localPosition;
                grabbyPointPosition.y -= descentSpeed * grabbySpeedIncrement * Time.deltaTime;
                grabbyPoint.localPosition = grabbyPointPosition;

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

                var grabbyPointPosition = grabbyPoint.localPosition;
                grabbyPointPosition.y += ascentSpeed * grabbySpeedIncrement * Time.deltaTime;

                var positionTraveled = Mathf.InverseLerp(MaxDescentPosition, OriginalYPosition, position.y);
                materialOffset.y = GetTextureOffsetModifier(positionTraveled);
                //Debug.LogFormat("GoingUp: Position Traveled = {0} => Mat offset = {1}", positionTraveled, materialOffset.y);
                material.mainTextureOffset = materialOffset;

                if (position.y >= OriginalYPosition)
                {
                    position.y = OriginalYPosition;
                    handState = HandState.Idle;

                    materialOffset.y = -0.5f;
                    material.mainTextureOffset = materialOffset;
                    grabbyPointPosition.y = grabbyYOriginalPoint;
                }

                grabbyPoint.localPosition = grabbyPointPosition;
            }
            else if (handState == HandState.Yeeting)
            {
                var rotation = yeetingContrainerTransform.rotation;
                var euler = rotation.eulerAngles;
                yeetingRotationTimer += Time.deltaTime;
                var delta = Mathf.Clamp01(yeetingRotationTimer / yeetingRotationMaxTime);

                if (delta >= 1)
                {
                    handState = HandState.Idle;
                    yeetingRotationTimer = 0f;
                    euler.z = yeetRotOriginal;
                    yeetingPower = 0;
                }
                else
                {
                    euler.z = Mathf.Lerp(yeetRotMin, yeetRotMax, delta);
                }

                if (delta > 0.8f && currentMergeableObject != null)
                {
                    currentMergeableObject.transform.SetParent(null);
                    currentMergeableObject.hoverScript.UpdateRotation = true;
                    currentMergeableObject.YeetToPosition.YeetInit(currentMergeableObject.transform.position, opponent.backCloud, 30, 0.5f);
                    currentMergeableObject.YeetToPosition.Yeet(Mathf.RoundToInt(currentMergeableObject.Damage * yeetingPower), onYeetEvent);
                    currentMergeableObject = null;
                }

                rotation.eulerAngles = euler;
                yeetingContrainerTransform.rotation = rotation;
            }
            else if (handState == HandState.ChargingYeet)
            {
                hover1.UpdateRotation = true;
                hover2.UpdateRotation = true;
            }

            transform.position = position;
        }
#endif

    }

    public void GameOver(bool winner)
    {
        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            if (winner)
            {
                j.SetRumble(80, 320, 0.6f, 200);
            }
            else
            {
                j.SetRumble(80, 160, 0.6f, 200);
            }
        }
    }

    private float GetTextureOffsetModifier(float positionTraveled)
    {
        return Mathf.Lerp(0f, -0.5f, positionTraveled);
    }

    private void DetectObjectCollision()
    {
        var collisions = Physics.OverlapSphere(transform.position, 1.5f, grabbiesLayerMask);
        DebugExtensions.DebugWireSphere(transform.position, Color.black, 1.5f, 2f);

        if (collisions.Length == 0)
        {
            return;
        }

        var mergeableObject = collisions[0].GetComponentInParent<MergeableObject>();
        if (currentMergeableObject == null)
        {
            currentMergeableObject = mergeableObject;
            mergeableObject.transform.SetParent(grabbyPoint);
            mergeableObject.transform.localPosition = Vector3.zero;
            mergeableObject.transform.localRotation = Quaternion.identity;
            mergeableObject.transform.localScale = Vector3.one;
            collisions[0].enabled = false;
            mergeableObject.GetComponent<FakeGravity>().enabled = false;

            AudioManager.Instance.PlayCombineSuccess();
            //Close hands
        }
        else
        {
            if (currentMergeableObject.UpgradeObjectByAddition(mergeableObject.VectorMaterial))
            {
                AudioManager.Instance.PlayCombineSuccess();
                Destroy(mergeableObject.gameObject);
            }
            else
            {
                AudioManager.Instance.PlayCombineFail();
            }
        }
    }

    public static void ClampPosition(ref Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, -13f, 13f);
        position.z = Mathf.Clamp(position.z, -13f, 13f);
    }
}