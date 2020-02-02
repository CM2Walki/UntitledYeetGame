using RotaryHeart.Lib.UnityGLDebug;
using UnityEngine;
using UEPhysics = UnityEngine.Physics;
using UEPhysics2D = UnityEngine.Physics2D;

namespace RotaryHeart.Lib.PhysicsExtension
{
    public enum PreviewCondition
    {
        None, Editor, Game
    }

    /// <summary>
    /// This is an extension for UnityEngine.Physics, it have all the cast, overlap, and checks with an option to preview them.
    /// </summary>
    public static class Physics
    {
        #region Unity Engine Physics
        //Global variables for use on default values, this is left here so that it can be changed easily
        private static Quaternion ORIENTATION = default(Quaternion);
        private static float MAX_DISTANCE = Mathf.Infinity;
        private static int LAYER_MASK = -1;
        private static QueryTriggerInteraction QUERY_TRIGGER_INTERACTION = QueryTriggerInteraction.UseGlobal;
        private static Color CAST_COLOR = new Color(1, 0.5f, 0, 1);

        #region BoxCast

        #region Boxcast single
        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return BoxCast(center, halfExtents, direction, out rayInfo, ORIENTATION, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return BoxCast(center, halfExtents, direction, out rayInfo, orientation, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit rayInfo, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCast(center, halfExtents, direction, out rayInfo, ORIENTATION, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return BoxCast(center, halfExtents, direction, out rayInfo, orientation, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit rayInfo, Quaternion orientation, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCast(center, halfExtents, direction, out rayInfo, orientation, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return BoxCast(center, halfExtents, direction, out rayInfo, orientation, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit rayInfo, Quaternion orientation, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCast(center, halfExtents, direction, out rayInfo, orientation, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return BoxCast(center, halfExtents, direction, out rayInfo, orientation, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit rayInfo, Quaternion orientation, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCast(center, halfExtents, direction, out rayInfo, orientation, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            bool collided = UEPhysics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);

                if (collided)
                {
                    DebugExtensions.DebugPoint(hitInfo.point, Color.red, 0.5f, drawDuration, preview);
                    maxDistance = hitInfo.distance;
                }

                DebugExtensions.DebugBox(center, halfExtents, direction, maxDistance, CAST_COLOR, orientation, collided ? (hitColor ?? Color.green) : CAST_COLOR, true, drawDuration, preview);
            }

            return collided;
        }
        #endregion

        #region Boxcast all
        public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastAll(center, halfExtents, direction, ORIENTATION, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastAll(center, halfExtents, direction, orientation, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastAll(center, halfExtents, direction, orientation, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, LayerMask layermask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layermask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, LayerMask layermask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit[] hitInfo = UEPhysics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layermask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                float previewDistance = 0;

                foreach (RaycastHit hit in hitInfo)
                {
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    DebugExtensions.DebugBox(center + direction * hit.distance, halfExtents, (hitColor ?? Color.green), orientation, drawDuration, preview);

                    if (hit.distance > previewDistance)
                        previewDistance = hit.distance;
                }

                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);
                DebugExtensions.DebugBox(center, halfExtents, direction, maxDistance, CAST_COLOR, orientation, CAST_COLOR, true, drawDuration, preview);
            }

            return hitInfo;
        }
        #endregion

        #region Boxcast non alloc
        public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastNonAlloc(center, halfExtents, direction, results, ORIENTATION, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, int layermask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layermask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int size = UEPhysics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layermask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                float previewDistance = 0;

                for (int i = 0; i < size; i++)
                {
                    RaycastHit hit = results[i];
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    DebugExtensions.DebugBox(center + direction * hit.distance, halfExtents, (hitColor ?? Color.green), orientation, drawDuration, preview);

                    if (hit.distance > previewDistance)
                        previewDistance = hit.distance;
                }

                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);
                DebugExtensions.DebugBox(center, halfExtents, direction, maxDistance, CAST_COLOR, orientation, CAST_COLOR, true, drawDuration, preview);
            }

            return size;
        }
        #endregion

        #endregion

        #region Capsule Cast

        #region Capsulecast single
        public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return CapsuleCast(point1, point2, radius, direction, out rayInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return CapsuleCast(point1, point2, radius, direction, out rayInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCast(point1, point2, radius, direction, out hitInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return CapsuleCast(point1, point2, radius, direction, out rayInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return CapsuleCast(point1, point2, radius, direction, out rayInfo, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            bool collided = UEPhysics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);

                if (collided)
                {
                    maxDistance = hitInfo.distance;
                    DebugExtensions.DebugPoint(hitInfo.point, Color.red, 0.5f, drawDuration, preview);
                }

                Vector3 midPoint = (point2 + point1) / 2;

                DebugExtensions.DebugCapsule(point1, point2, CAST_COLOR, radius, true, drawDuration, preview);

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(midPoint, midPoint + direction * maxDistance, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(midPoint, midPoint + direction * maxDistance, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);

                DebugExtensions.DebugCapsule(point1 + direction * maxDistance, point2 + direction * maxDistance, collided ? (hitColor ?? Color.green) : CAST_COLOR, radius, true, drawDuration, preview);

            }

            return collided;
        }
        #endregion

        #region Capsulecast all
        public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastAll(point1, point2, radius, direction, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastAll(point1, point2, radius, direction, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layermask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastAll(point1, point2, radius, direction, maxDistance, layermask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit[] hitInfo = UEPhysics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layermask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = false;
                float maxDistanceRay = 0;

                foreach (RaycastHit hit in hitInfo)
                {
                    collided = true;

                    if (hit.distance > maxDistanceRay)
                        maxDistanceRay = hit.distance;

                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);
                    DebugExtensions.DebugCapsule(point1 + direction * hit.distance, point2 + direction * hit.distance, (hitColor ?? Color.green), radius, true, drawDuration, preview);
                }

                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);
                Vector3 midPoint = (point2 + point1) / 2;

                DebugExtensions.DebugCapsule(point1, point2, CAST_COLOR, radius, true, drawDuration, preview);

                Vector3 endCollisionPoint = midPoint + direction * maxDistanceRay;

                if (preview == PreviewCondition.Editor)
                {
                    Debug.DrawLine(midPoint, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                    Debug.DrawLine(endCollisionPoint, midPoint + direction * maxDistance, (noHitColor ?? Color.red), drawDuration);
                }
                else
                {
                    GLDebug.DrawLine(midPoint, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);
                    GLDebug.DrawLine(endCollisionPoint, midPoint + direction * maxDistance, (noHitColor ?? Color.red), drawDuration);
                }

                DebugExtensions.DebugCapsule(point1 + direction * maxDistance, point2 + direction * maxDistance, CAST_COLOR, radius, true, drawDuration, preview);
            }

            return hitInfo;
        }
        #endregion

        #region Capsulecast non alloc
        public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastNonAlloc(point1, point2, radius, direction, results, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layermask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layermask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layermask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int size = UEPhysics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layermask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = false;
                float maxDistanceRay = 0;

                for (int i = 0; i < size; i++)
                {
                    collided = true;

                    RaycastHit hit = results[i];

                    if (hit.distance > maxDistanceRay)
                        maxDistanceRay = hit.distance;

                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);
                    DebugExtensions.DebugCapsule(point1 + direction * hit.distance, point2 + direction * hit.distance, (hitColor ?? Color.green), radius, true, drawDuration, preview);
                }

                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);
                Vector3 midPoint = (point2 + point1) / 2;

                DebugExtensions.DebugCapsule(point1, point2, CAST_COLOR, radius, true, drawDuration, preview);

                Vector3 endCollisionPoint = midPoint + direction * maxDistanceRay;

                if (preview == PreviewCondition.Editor)
                {
                    Debug.DrawLine(midPoint, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                    Debug.DrawLine(endCollisionPoint, midPoint + direction * maxDistance, (noHitColor ?? Color.red), drawDuration);
                }
                else
                {
                    GLDebug.DrawLine(midPoint, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);
                    Debug.DrawLine(endCollisionPoint, midPoint + direction * maxDistance, (noHitColor ?? Color.red), drawDuration);
                }

                DebugExtensions.DebugCapsule(point1 + direction * maxDistance, point2 + direction * maxDistance, CAST_COLOR, radius, true, drawDuration, preview);
            }

            return size;
        }
        #endregion

        #endregion

        #region Check Box
        public static bool CheckBox(Vector3 center, Vector3 halfExtents, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CheckBox(center, halfExtents, ORIENTATION, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CheckBox(center, halfExtents, orientation, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layermask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CheckBox(center, halfExtents, orientation, layermask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            bool collided = UEPhysics.CheckBox(center, halfExtents, orientation, layermask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugBox(center, halfExtents, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), orientation, drawDuration, preview);
            }

            return collided;
        }
        #endregion

        #region Check Capsule
        public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CheckCapsule(start, end, radius, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layermask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CheckCapsule(start, end, radius, layermask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layermask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            bool collided = UEPhysics.CheckCapsule(start, end, radius, layermask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugCapsule(start, end, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, false, drawDuration, preview);
            }

            return collided;
        }
        #endregion

        #region Check Sphere
        public static bool CheckSphere(Vector3 position, float radius, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CheckSphere(position, radius, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CheckSphere(Vector3 position, float radius, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CheckSphere(position, radius, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool CheckSphere(Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            bool collided = UEPhysics.CheckSphere(position, radius, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugWireSphere(position, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, drawDuration, preview);
            }

            return collided;
        }
        #endregion

        #region Linecast
        public static bool Linecast(Vector3 start, Vector3 end, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Linecast(start, end, out rayInfo, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Linecast(Vector3 start, Vector3 end, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Linecast(start, end, out rayInfo, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Linecast(Vector3 start, Vector3 end, int layerMask, QueryTriggerInteraction querryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Linecast(start, end, out rayInfo, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Linecast(start, end, out hitInfo, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Linecast(start, end, out hitInfo, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask, QueryTriggerInteraction querryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Vector3 heading = end - start;
            return Raycast(start, heading, out hitInfo, heading.magnitude, layerMask, querryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }
        #endregion

        #region Overlap Box
        #region OverlapBox alloc
        public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBox(center, halfExtents, ORIENTATION, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBox(center, halfExtents, orientation, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBox(center, halfExtents, orientation, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider[] colliders = UEPhysics.OverlapBox(center, halfExtents, orientation, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = colliders.Length > 0;

                DebugExtensions.DebugBox(center, halfExtents, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), orientation, drawDuration, preview);
            }

            return colliders;
        }
        #endregion

        #region OverlapBox non alloc
        public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxNonAlloc(center, halfExtents, results, ORIENTATION, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxNonAlloc(center, halfExtents, results, orientation, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxNonAlloc(center, halfExtents, results, orientation, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int size = UEPhysics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = size > 0;

                DebugExtensions.DebugBox(center, halfExtents, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), orientation, drawDuration, preview);
            }

            return size;
        }
        #endregion
        #endregion

        #region Overlap Capsule
        #region OverlapCapsule alloc
        public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsule(point0, point1, radius, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsule(point0, point1, radius, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider[] colliders = UEPhysics.OverlapCapsule(point0, point1, radius, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = colliders.Length > 0;

                DebugExtensions.DebugCapsule(point0, point1, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, false, drawDuration, preview);
            }

            return colliders;
        }
        #endregion

        #region OverlapCapsule non alloc
        public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsuleNonAlloc(point0, point1, radius, results, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int size = UEPhysics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = size > 0;

                DebugExtensions.DebugCapsule(point0, point1, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, false, drawDuration, preview);
            }

            return size;
        }
        #endregion
        #endregion

        #region Overlap Sphere
        #region OverlapSphere alloc
        public static Collider[] OverlapSphere(Vector3 position, float radius, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapSphere(position, radius, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapSphere(position, radius, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider[] colliders = UEPhysics.OverlapSphere(position, radius, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = colliders.Length > 0;

                DebugExtensions.DebugWireSphere(position, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, drawDuration, preview);
            }

            return colliders;
        }
        #endregion

        #region OverlapSphere non alloc
        public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapSphereNonAlloc(position, radius, results, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapSphereNonAlloc(position, radius, results, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int size = UEPhysics.OverlapSphereNonAlloc(position, radius, results, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = size > 0;

                DebugExtensions.DebugWireSphere(position, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, drawDuration, preview);
            }

            return size;
        }
        #endregion
        #endregion

        #region Raycast

        #region Raycast single
        #region Vector3
        public static bool Raycast(Vector3 origin, Vector3 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Raycast(origin, direction, out rayInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Raycast(origin, direction, out rayInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Raycast(origin, direction, out rayInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Raycast(origin, direction, out rayInfo, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(origin, direction, out hitInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(origin, direction, out hitInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(origin, direction, out hitInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            return Raycast(new Ray(origin, direction), out hitInfo, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }
        #endregion

        #region Ray
        public static bool Raycast(Ray ray, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Raycast(ray, out rayInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Ray ray, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Raycast(ray, out rayInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Ray ray, out RaycastHit hitInfo, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(ray, out hitInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Ray ray, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Raycast(ray, out rayInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(ray, out hitInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit rayInfo;
            return Raycast(ray, out rayInfo, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(ray, out hitInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            ray.direction.Normalize();
            bool collided = UEPhysics.Raycast(ray, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                Vector3 end = ray.origin + ray.direction * (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);

                if (collided)
                {
                    collided = true;
                    end = hitInfo.point;

                    DebugExtensions.DebugPoint(end, Color.red, 0.5f, drawDuration, preview);
                }

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(ray.origin, end, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(ray.origin, end, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
            }

            return collided;
        }

        #endregion
        #endregion

        #region Raycast all
        #region Vector3
        public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(origin, direction, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(origin, direction, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(origin, direction, maxDistance, (int)layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(new Ray(origin, direction), maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }
        #endregion

        #region Ray
        public static RaycastHit[] RaycastAll(Ray ray, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(ray, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(ray, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, LayerMask layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(ray, maxDistance, (int)layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            ray.direction.Normalize();
            RaycastHit[] raycastInfo = UEPhysics.RaycastAll(ray, maxDistance, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                Vector3 end = ray.origin + ray.direction * (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);
                Vector3 previewOrigin = ray.origin;
                Vector3 sectionOrigin = ray.origin;

                foreach (RaycastHit hit in raycastInfo)
                {
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    if (preview == PreviewCondition.Editor)
                        Debug.DrawLine(sectionOrigin, hit.point, (hitColor ?? Color.green), drawDuration);
                    else
                        GLDebug.DrawLine(sectionOrigin, hit.point, (hitColor ?? Color.green), drawDuration);

                    if (Vector3.Distance(ray.origin, hit.point) > Vector3.Distance(ray.origin, previewOrigin))
                        previewOrigin = hit.point;

                    sectionOrigin = hit.point;
                }

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(previewOrigin, end, (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(previewOrigin, end, (noHitColor ?? Color.red), drawDuration);
            }

            return raycastInfo;
        }
        #endregion
        #endregion

        #region Raycast non alloc
        #region Vector3
        public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(origin, direction, results, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(origin, direction, results, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, LayerMask layermask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(origin, direction, results, maxDistance, layermask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, LayerMask layermask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(new Ray(origin, direction), results, maxDistance, layermask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }
        #endregion

        #region Ray
        public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(ray, results, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(ray, results, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, LayerMask layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(ray, results, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            ray.direction.Normalize();
            int size = UEPhysics.RaycastNonAlloc(ray, results, maxDistance, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                Vector3 end = ray.origin + ray.direction * (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);
                Vector3 previewOrigin = ray.origin;
                Vector3 sectionOrigin = ray.origin;

                for (int i = 0; i < size; i++)
                {
                    RaycastHit hit = results[i];
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    if (preview == PreviewCondition.Editor)
                        Debug.DrawLine(sectionOrigin, hit.point, (hitColor ?? Color.green), drawDuration);
                    else
                        GLDebug.DrawLine(sectionOrigin, hit.point, (hitColor ?? Color.green), drawDuration);

                    if (Vector3.Distance(ray.origin, hit.point) > Vector3.Distance(ray.origin, previewOrigin))
                        previewOrigin = hit.point;

                    sectionOrigin = hit.point;
                }

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(previewOrigin, end, (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(previewOrigin, end, (noHitColor ?? Color.red), drawDuration);
            }

            return size;
        }
        #endregion
        #endregion

        #endregion

        #region Sphere Cast
        #region Spherecast single
        #region Vector3
        public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit hitInfo;
            return SphereCast(origin, radius, direction, out hitInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit hitInfo;
            return SphereCast(origin, radius, direction, out hitInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit hitInfo;
            return SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit hitInfo;
            return SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCast(origin, radius, direction, out hitInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCast(origin, radius, direction, out hitInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCast(new Ray(origin, direction), radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }
        #endregion

        #region Ray
        public static bool SphereCast(Ray ray, float radius, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit hitInfo;
            return SphereCast(ray, radius, out hitInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Ray ray, float radius, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit hitInfo;
            return SphereCast(ray, radius, out hitInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCast(ray, radius, out hitInfo, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit hitInfo;
            return SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCast(ray, radius, out hitInfo, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            RaycastHit hitInfo;
            return SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            ray.direction.Normalize();
            bool collided = UEPhysics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);

                if (collided)
                {
                    maxDistance = hitInfo.distance;
                    DebugExtensions.DebugPoint(hitInfo.point, Color.red, 0.5f, drawDuration, preview);
                }

                DebugExtensions.DebugWireSphere(ray.origin, CAST_COLOR, radius, drawDuration, preview);

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);

                DebugExtensions.DebugWireSphere(ray.origin + ray.direction * maxDistance, collided ? (hitColor ?? Color.green) : CAST_COLOR, radius, drawDuration, preview);
            }

            return collided;
        }
        #endregion
        #endregion

        #region Spherecast all
        #region Vector3
        public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastAll(origin, radius, direction, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastAll(origin, radius, direction, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastAll(origin, radius, direction, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastAll(new Ray(origin, direction), radius, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }
        #endregion

        #region Ray
        public static RaycastHit[] SphereCastAll(Ray ray, float radius, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastAll(ray, radius, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastAll(ray, radius, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastAll(ray, radius, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            ray.direction.Normalize();
            RaycastHit[] hitInfo = UEPhysics.SphereCastAll(ray, radius, maxDistance, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = false;
                float maxDistanceRay = 0;

                foreach (RaycastHit hit in hitInfo)
                {
                    collided = true;

                    if (hit.distance > maxDistanceRay)
                        maxDistanceRay = hit.distance;

                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);
                    DebugExtensions.DebugWireSphere(ray.origin + ray.direction * hit.distance, (hitColor ?? Color.green), radius, drawDuration, preview);
                }

                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);

                DebugExtensions.DebugWireSphere(ray.origin, CAST_COLOR, radius, drawDuration, preview);

                Vector3 endCollisionPoint = ray.origin + ray.direction * maxDistanceRay;

                if (preview == PreviewCondition.Editor)
                {
                    Debug.DrawLine(ray.origin, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                    Debug.DrawLine(endCollisionPoint, ray.origin + ray.direction * maxDistance, (noHitColor ?? Color.red), drawDuration);
                }
                else
                {
                    GLDebug.DrawLine(ray.origin, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);
                    GLDebug.DrawLine(endCollisionPoint, ray.origin + ray.direction * maxDistance, (noHitColor ?? Color.red), drawDuration);
                }

                DebugExtensions.DebugWireSphere(ray.origin + ray.direction * maxDistance, CAST_COLOR, radius, drawDuration, preview);
            }

            return hitInfo;
        }
        #endregion
        #endregion

        #region Spherecast non alloc
        #region Vector3
        public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastNonAlloc(origin, radius, direction, results, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastNonAlloc(origin, radius, direction, results, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastNonAlloc(new Ray(origin, direction), radius, results, maxDistance, layerMask, queryTriggerInteraction, preview, drawDuration, hitColor, noHitColor);
        }
        #endregion

        #region Ray
        public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastNonAlloc(ray, radius, results, MAX_DISTANCE, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastNonAlloc(ray, radius, results, maxDistance, LAYER_MASK, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, QUERY_TRIGGER_INTERACTION, preview, drawDuration, hitColor, noHitColor);
        }

        public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            ray.direction.Normalize();
            int size = UEPhysics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, queryTriggerInteraction);

            if (preview != PreviewCondition.None)
            {
                bool collided = false;
                float maxDistanceRay = 0;

                foreach (RaycastHit hit in results)
                {
                    collided = true;

                    if (hit.distance > maxDistanceRay)
                        maxDistanceRay = hit.distance;

                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);
                    DebugExtensions.DebugWireSphere(ray.origin + ray.direction * hit.distance, (hitColor ?? Color.green), radius, drawDuration, preview);
                }

                maxDistance = (maxDistance == MAX_DISTANCE ? 1000 * 1000 : maxDistance);

                DebugExtensions.DebugWireSphere(ray.origin, CAST_COLOR, radius, drawDuration, preview);

                Vector3 endCollisionPoint = ray.origin + ray.direction * maxDistanceRay;

                if (preview == PreviewCondition.Editor)
                {
                    Debug.DrawLine(ray.origin, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                    Debug.DrawLine(endCollisionPoint, ray.origin + ray.direction * maxDistance, (noHitColor ?? Color.red), drawDuration);
                }
                else
                {
                    GLDebug.DrawLine(ray.origin, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);
                    Debug.DrawLine(endCollisionPoint, ray.origin + ray.direction * maxDistance, (noHitColor ?? Color.red), drawDuration);
                }

                DebugExtensions.DebugWireSphere(ray.origin + ray.direction * maxDistance, CAST_COLOR, radius, drawDuration, preview);
            }

            return size;
        }
        #endregion
        #endregion
        #endregion

        #endregion
    }

    /// <summary>
    /// This is an extesion for UnityEngine.Physics2D, it have all the cast and overlap with an option to preview them.
    /// </summary>
    public static class Physics2D
    {
        #region Unity Engine Physics
        //Global variables for use on default values, this is left here so that it can be changed easily
        private static float MAX_DISTANCE = Mathf.Infinity;
        private static Color CAST_COLOR = new Color(1, 0.5f, 0, 1);

        #region BoxCast

        #region BoxCast Single
        public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCast(origin, size, angle, direction, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCast(origin, size, angle, direction, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, LayerMask layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCast(origin, size, angle, direction, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, LayerMask layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCast(origin, size, angle, direction, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, LayerMask layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit2D hitInfo = UEPhysics2D.BoxCast(origin, size, angle, direction, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;

                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                bool collided = hitInfo.collider != null;

                if (collided)
                {
                    DebugExtensions.DebugPoint(hitInfo.point, Color.red, 0.5f, drawDuration, preview);
                    distance = hitInfo.distance;
                }

                DebugExtensions.DebugBox(origin, size, direction, distance, CAST_COLOR, Quaternion.Euler(0, 0, angle), collided ? (hitColor ?? Color.green) : CAST_COLOR, true, drawDuration, preview);
            }

            return hitInfo;
        }

        public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
        {
            return BoxCast(origin, size, angle, direction, contactFilter, results, MAX_DISTANCE);
        }

        public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int count = UEPhysics2D.BoxCast(origin, size, angle, direction, contactFilter, results, distance);

            if (preview != PreviewCondition.None)
            {
                size /= 2;

                float previewDistance = 0;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                foreach (RaycastHit2D hit in results)
                {
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    DebugExtensions.DebugBox(origin + direction * hit.distance, size, (hitColor ?? Color.green), rot, drawDuration, preview);

                    if (hit.distance > previewDistance)
                        previewDistance = hit.distance;
                }

                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);
                DebugExtensions.DebugBox(origin, size, direction, distance, CAST_COLOR, rot, CAST_COLOR, true, drawDuration, preview);
            }

            return count;
        }
        #endregion

        #region BoxCast All

        public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastAll(origin, size, angle, direction, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastAll(origin, size, angle, direction, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastAll(origin, size, angle, direction, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastAll(origin, size, angle, direction, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit2D[] hitInfo = UEPhysics2D.BoxCastAll(origin, size, angle, direction, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                float previewDistance = 0;
                Quaternion rot = Quaternion.Euler(0, 0, angle);

                foreach (RaycastHit2D hit in hitInfo)
                {
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    DebugExtensions.DebugBox(origin + direction * hit.distance, size, (hitColor ?? Color.green), rot, drawDuration, preview);

                    if (hit.distance > previewDistance)
                        previewDistance = hit.distance;
                }

                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);
                DebugExtensions.DebugBox(origin, size, direction, distance, CAST_COLOR, rot, CAST_COLOR, true, drawDuration, preview);
            }

            return hitInfo;
        }
        #endregion

        #region BoxCast non alloc

        public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastNonAlloc(origin, size, angle, direction, results, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastNonAlloc(origin, size, angle, direction, results, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int count = UEPhysics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;

                float previewDistance = 0;
                Quaternion rot = Quaternion.Euler(0, 0, angle);

                for (int i = 0; i < count; i++)
                {
                    RaycastHit2D hit = results[i];
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    DebugExtensions.DebugBox(origin + direction * hit.distance, size, (hitColor ?? Color.green), rot, drawDuration, preview);

                    if (hit.distance > previewDistance)
                        previewDistance = hit.distance;
                }

                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);
                DebugExtensions.DebugBox(origin, size, direction, distance, CAST_COLOR, rot, CAST_COLOR, true, drawDuration, preview);
            }

            return count;
        }
        #endregion

        #endregion

        #region CapsuleCast

        #region CapsuleCast single

        public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCast(origin, size, capsuleDirection, angle, direction, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit2D hitInfo = UEPhysics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                size /= 2;
                Vector2 point1;
                Vector2 point2;
                float radius;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                if (hitInfo.collider != null)
                {
                    distance = hitInfo.distance;
                    DebugExtensions.DebugPoint(hitInfo.point, Color.red, 0.5f, drawDuration, preview);
                }

                if (capsuleDirection == CapsuleDirection2D.Vertical)
                {
                    if (size.y > size.x)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.x;
                }
                else
                {
                    if (size.x > size.y)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.y;
                }

                point1 = (Vector2)(rot * point1) + origin;
                point2 = (Vector2)(rot * point2) + origin;

                DebugExtensions.DebugOneSidedCapsule(point1, point2, CAST_COLOR, radius, true, drawDuration, preview);

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(origin, origin + direction * distance, hitInfo ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(origin, origin + direction * distance, hitInfo ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);

                DebugExtensions.DebugOneSidedCapsule(point1 + direction * distance, point2 + direction * distance, hitInfo ? (hitColor ?? Color.green) : CAST_COLOR, radius, true, drawDuration, preview);

            }

            return hitInfo;
        }

        public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCast(origin, size, capsuleDirection, angle, direction, contactFilter, results, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int count = UEPhysics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, contactFilter, results, distance);

            if (preview != PreviewCondition.None)
            {
                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                size /= 2;
                Vector2 point1;
                Vector2 point2;
                float radius;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                if (capsuleDirection == CapsuleDirection2D.Vertical)
                {
                    if (size.y > size.x)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.x;
                }
                else
                {
                    if (size.x > size.y)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.y;
                }

                point1 = (Vector2)(rot * point1) + origin;
                point2 = (Vector2)(rot * point2) + origin;

                DebugExtensions.DebugOneSidedCapsule(point1, point2, CAST_COLOR, radius, true, drawDuration, preview);

                foreach (RaycastHit2D hitInfo in results)
                {
                    if (hitInfo.collider != null)
                    {
                        distance = hitInfo.distance;
                        DebugExtensions.DebugPoint(hitInfo.point, Color.red, 0.5f, drawDuration, preview);
                    }
                }

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(origin, origin + direction * distance, count != 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(origin, origin + direction * distance, count != 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);

                DebugExtensions.DebugOneSidedCapsule(point1 + direction * distance, point2 + direction * distance, count != 0 ? (hitColor ?? Color.green) : CAST_COLOR, radius, true, drawDuration, preview);
            }

            return count;
        }
        #endregion

        #region CapsuleCast All

        public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastAll(origin, size, capsuleDirection, angle, direction, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastAll(origin, size, capsuleDirection, angle, direction, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastAll(origin, size, capsuleDirection, angle, direction, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastAll(origin, size, capsuleDirection, angle, direction, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit2D[] hitInfo = UEPhysics2D.CapsuleCastAll(origin, size, capsuleDirection, angle, direction, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                size /= 2;
                Vector2 point1;
                Vector2 point2;
                float radius;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                bool collided = false;
                float maxDistanceRay = 0;

                if (capsuleDirection == CapsuleDirection2D.Vertical)
                {
                    if (size.y > size.x)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.x;
                }
                else
                {
                    if (size.x > size.y)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.y;
                }

                point1 = (Vector2)(rot * point1) + origin;
                point2 = (Vector2)(rot * point2) + origin;

                foreach (RaycastHit2D hit in hitInfo)
                {
                    collided = true;

                    if (hit.distance > maxDistanceRay)
                        maxDistanceRay = hit.distance;

                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);
                    DebugExtensions.DebugOneSidedCapsule(point1 + direction * hit.distance, point2 + direction * hit.distance, (hitColor ?? Color.green), radius, true, drawDuration, preview);
                }

                Vector2 midPoint = (point2 + point1) / 2;

                DebugExtensions.DebugOneSidedCapsule(point1, point2, CAST_COLOR, radius, true, drawDuration, preview);

                Vector3 endCollisionPoint = midPoint + direction * maxDistanceRay;

                if (preview == PreviewCondition.Editor)
                {
                    Debug.DrawLine(midPoint, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                    Debug.DrawLine(endCollisionPoint, midPoint + direction * distance, (noHitColor ?? Color.red), drawDuration);
                }
                else
                {
                    GLDebug.DrawLine(midPoint, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);
                    GLDebug.DrawLine(endCollisionPoint, midPoint + direction * distance, (noHitColor ?? Color.red), drawDuration);
                }

                DebugExtensions.DebugOneSidedCapsule(point1 + direction * distance, point2 + direction * distance, CAST_COLOR, radius, true, drawDuration, preview);
            }

            return hitInfo;
        }
        #endregion

        #region CapsuleCast non alloc

        public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int count = UEPhysics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                bool collided = false;
                float maxDistanceRay = 0;

                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                size /= 2;
                Vector2 point1;
                Vector2 point2;
                float radius;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                if (capsuleDirection == CapsuleDirection2D.Vertical)
                {
                    if (size.y > size.x)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.x;
                }
                else
                {
                    if (size.x > size.y)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.y;
                }

                point1 = (Vector2)(rot * point1) + origin;
                point2 = (Vector2)(rot * point2) + origin;


                for (int i = 0; i < count; i++)
                {
                    collided = true;

                    RaycastHit2D hit = results[i];

                    if (hit.distance > maxDistanceRay)
                        maxDistanceRay = hit.distance;

                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);
                    DebugExtensions.DebugOneSidedCapsule(point1 + direction * hit.distance, point2 + direction * hit.distance, (hitColor ?? Color.green), radius, true, drawDuration, preview);
                }

                Vector2 midPoint = (point2 + point1) / 2;

                DebugExtensions.DebugOneSidedCapsule(point1, point2, CAST_COLOR, radius, true, drawDuration, preview);

                Vector3 endCollisionPoint = midPoint + direction * maxDistanceRay;

                if (preview == PreviewCondition.Editor)
                {
                    Debug.DrawLine(midPoint, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                    Debug.DrawLine(endCollisionPoint, midPoint + direction * distance, (noHitColor ?? Color.red), drawDuration);
                }
                else
                {
                    GLDebug.DrawLine(midPoint, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);
                    Debug.DrawLine(endCollisionPoint, midPoint + direction * distance, (noHitColor ?? Color.red), drawDuration);
                }

                DebugExtensions.DebugOneSidedCapsule(point1 + direction * distance, point2 + direction * distance, CAST_COLOR, radius, true, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #endregion

        #region CircleCast

        #region CircleCast single

        public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCast(origin, radius, direction, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCast(origin, radius, direction, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCast(origin, radius, direction, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCast(origin, radius, direction, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit2D hitInfo = UEPhysics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                bool collided = hitInfo.collider != null;

                if (collided)
                {
                    DebugExtensions.DebugPoint(hitInfo.point, Color.red, 0.5f, drawDuration, preview);
                    distance = hitInfo.distance;
                }

                DebugExtensions.DebugCircle(origin, Vector3.forward, CAST_COLOR, radius, drawDuration, preview);

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(origin, origin + direction * distance, hitInfo ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(origin, origin + direction * distance, hitInfo ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);

                DebugExtensions.DebugCircle(origin + direction * distance, Vector3.forward, hitInfo ? (hitColor ?? Color.green) : CAST_COLOR, radius, drawDuration, preview);
            }

            return hitInfo;
        }

        public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCast(origin, radius, direction, contactFilter, results, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int count = UEPhysics2D.CircleCast(origin, radius, direction, contactFilter, results, distance);

            if (preview != PreviewCondition.None)
            {
                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                bool notEmpty = false;
                foreach (var hitInfo in results)
                {
                    bool collided = hitInfo.collider != null;

                    if (collided)
                    {
                        notEmpty = true;
                        DebugExtensions.DebugPoint(hitInfo.point, Color.red, 0.5f, drawDuration, preview);
                        distance = hitInfo.distance;
                    }
                }

                DebugExtensions.DebugCircle(origin, Vector3.forward, CAST_COLOR, radius, drawDuration, preview);

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(origin, origin + direction * distance, notEmpty ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(origin, origin + direction * distance, notEmpty ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);

                DebugExtensions.DebugCircle(origin + direction * distance, Vector3.forward, notEmpty ? (hitColor ?? Color.green) : CAST_COLOR, radius, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #region CircleCast All

        public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCastAll(origin, radius, direction, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCastAll(origin, radius, direction, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCastAll(origin, radius, direction, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCastAll(origin, radius, direction, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit2D[] hitInfo = UEPhysics2D.CircleCastAll(origin, radius, direction, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                float maxDistanceRay = 0;
                bool collided = false;

                foreach (RaycastHit2D hit in hitInfo)
                {
                    if (hit.collider)
                        collided = true;

                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    DebugExtensions.DebugCircle(origin + direction * hit.distance, Vector3.forward, (hitColor ?? Color.green), radius, drawDuration, preview);

                    if (hit.distance > maxDistanceRay)
                        maxDistanceRay = hit.distance;
                }

                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                Vector3 endCollisionPoint = origin + direction * maxDistanceRay;

                if (preview == PreviewCondition.Editor)
                {
                    Debug.DrawLine(origin, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                    Debug.DrawLine(endCollisionPoint, origin + direction * distance, (noHitColor ?? Color.red), drawDuration);
                }
                else
                {
                    GLDebug.DrawLine(origin, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);
                    GLDebug.DrawLine(endCollisionPoint, origin + direction * distance, (noHitColor ?? Color.red), drawDuration);
                }

                DebugExtensions.DebugCircle(origin, Vector3.forward, CAST_COLOR, radius, drawDuration, preview);
                DebugExtensions.DebugCircle(origin + direction * distance, Vector3.forward, CAST_COLOR, radius, drawDuration, preview);
            }

            return hitInfo;
        }

        #endregion

        #region CircleCast non alloc

        public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCastNonAlloc(origin, radius, direction, results, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCastNonAlloc(origin, radius, direction, results, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCastNonAlloc(origin, radius, direction, results, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return CircleCastNonAlloc(origin, radius, direction, results, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int count = UEPhysics2D.CircleCastNonAlloc(origin, radius, direction, results, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                float maxDistanceRay = 0;
                bool collided = false;

                foreach (RaycastHit2D hit in results)
                {
                    if (hit.collider)
                        collided = true;

                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    DebugExtensions.DebugCircle(origin + direction * hit.distance, Vector3.forward, (hitColor ?? Color.green), radius, drawDuration, preview);

                    if (hit.distance > maxDistanceRay)
                        maxDistanceRay = hit.distance;
                }

                distance = (distance == MAX_DISTANCE ? 1000 * 1000 : distance);

                Vector3 endCollisionPoint = origin + direction * maxDistanceRay;

                if (preview == PreviewCondition.Editor)
                {
                    Debug.DrawLine(origin, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                    Debug.DrawLine(endCollisionPoint, origin + direction * distance, (noHitColor ?? Color.red), drawDuration);
                }
                else
                {
                    GLDebug.DrawLine(origin, endCollisionPoint, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration, true);
                    GLDebug.DrawLine(endCollisionPoint, origin + direction * distance, (noHitColor ?? Color.red), drawDuration);
                }

                DebugExtensions.DebugCircle(origin, Vector3.forward, CAST_COLOR, radius, drawDuration, preview);
                DebugExtensions.DebugCircle(origin + direction * distance, Vector3.forward, CAST_COLOR, radius, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #endregion

        #region LineCast

        #region LineCast single

        public static RaycastHit2D Linecast(Vector2 start, Vector2 end, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Linecast(start, end, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Linecast(start, end, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Linecast(start, end, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Vector2 direction = (end - start);
            return Raycast(start, direction, direction.magnitude, layerMask, minDepth, maxDepth, preview, drawDuration, hitColor, noHitColor);
        }

        public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Vector2 direction = (end - start);
            return Raycast(start, direction, contactFilter, results, direction.magnitude, preview, drawDuration, hitColor, noHitColor);
        }

        #endregion

        #region LineCast All

        public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return LinecastAll(start, end, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return LinecastAll(start, end, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return LinecastAll(start, end, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Vector2 direction = (end - start);
            return RaycastAll(start, direction, direction.magnitude, layerMask, minDepth, maxDepth, preview, drawDuration, hitColor, noHitColor);
        }

        #endregion

        #region LineCast non alloc

        public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return LinecastNonAlloc(start, end, results, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return LinecastNonAlloc(start, end, results, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return LinecastNonAlloc(start, end, results, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Vector2 direction = (end - start);
            return RaycastNonAlloc(start, direction, results, direction.magnitude, layerMask, minDepth, maxDepth, preview, drawDuration, hitColor, noHitColor);
        }

        #endregion

        #endregion

        #region OverlapArea

        #region OverlapArea single

        public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapArea(pointA, pointB, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapArea(pointA, pointB, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapArea(pointA, pointB, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D collider = UEPhysics2D.OverlapArea(pointA, pointB, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                Vector2 topLeft = pointA;
                Vector2 bottomLeft = new Vector2(pointA.x, pointB.y);
                Vector2 bottomRight = pointB;
                Vector2 topRight = new Vector2(pointB.x, pointA.y);

                Color color = collider != null ? (hitColor ?? Color.green) : (noHitColor ?? Color.red);

                //|
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(topLeft, bottomLeft, color, drawDuration);
                else
                    GLDebug.DrawLine(topLeft, bottomLeft, color, drawDuration);

                //_
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(bottomLeft, bottomRight, color, drawDuration);
                else
                    GLDebug.DrawLine(bottomLeft, bottomRight, color, drawDuration);

                // |
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(bottomRight, topRight, color, drawDuration);
                else
                    GLDebug.DrawLine(bottomRight, topRight, color, drawDuration);

                //-
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(topRight, topLeft, color, drawDuration);
                else
                    GLDebug.DrawLine(topRight, topLeft, color, drawDuration);
            }

            return collider;
        }

        #endregion

        #region OverlapArea all

        public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapAreaAll(pointA, pointB, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapAreaAll(pointA, pointB, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapAreaAll(pointA, pointB, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D[] colliders = UEPhysics2D.OverlapAreaAll(pointA, pointB, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                Vector2 topLeft = pointA;
                Vector2 bottomLeft = new Vector2(pointA.x, pointB.y);
                Vector2 bottomRight = pointB;
                Vector2 topRight = new Vector2(pointB.x, pointA.y);

                Color color = colliders.Length > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red);

                //|
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(topLeft, bottomLeft, color, drawDuration);
                else
                    GLDebug.DrawLine(topLeft, bottomLeft, color, drawDuration);

                //_
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(bottomLeft, bottomRight, color, drawDuration);
                else
                    GLDebug.DrawLine(bottomLeft, bottomRight, color, drawDuration);

                // |
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(bottomRight, topRight, color, drawDuration);
                else
                    GLDebug.DrawLine(bottomRight, topRight, color, drawDuration);

                //-
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(topRight, topLeft, color, drawDuration);
                else
                    GLDebug.DrawLine(topRight, topLeft, color, drawDuration);
            }

            return colliders;
        }

        #endregion

        #region OverlapArea non alloc

        public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapAreaNonAlloc(pointA, pointB, results, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapAreaNonAlloc(pointA, pointB, results, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapAreaNonAlloc(pointA, pointB, results, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int size = UEPhysics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                Vector2 topLeft = pointA;
                Vector2 bottomLeft = new Vector2(pointA.x, pointB.y);
                Vector2 bottomRight = pointB;
                Vector2 topRight = new Vector2(pointB.x, pointA.y);

                Color color = size > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red);

                //|
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(topLeft, bottomLeft, color, drawDuration);
                else
                    GLDebug.DrawLine(topLeft, bottomLeft, color, drawDuration);

                //_
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(bottomLeft, bottomRight, color, drawDuration);
                else
                    GLDebug.DrawLine(bottomLeft, bottomRight, color, drawDuration);

                // |
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(bottomRight, topRight, color, drawDuration);
                else
                    GLDebug.DrawLine(bottomRight, topRight, color, drawDuration);

                //-
                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(topRight, topLeft, color, drawDuration);
                else
                    GLDebug.DrawLine(topRight, topLeft, color, drawDuration);
            }

            return size;
        }

        #endregion

        #endregion

        #region OverlapBox

        #region OverlapBox single

        public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBox(point, size, angle, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBox(point, size, angle, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBox(point, size, angle, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D collider = UEPhysics2D.OverlapBox(point, size, angle, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                DebugExtensions.DebugBox(point, size, collider ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), Quaternion.Euler(0, 0, angle), drawDuration, preview);
            }

            return collider;
        }

        public static int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int count = UEPhysics2D.OverlapBox(point, size, angle, contactFilter, results);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                DebugExtensions.DebugBox(point, size, count > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), Quaternion.Euler(0, 0, angle), drawDuration, preview);
            }

            return count;
        }

        #endregion

        #region OverlapBox all

        public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxAll(point, size, angle, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxAll(point, size, angle, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxAll(point, size, angle, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D[] colliders = UEPhysics2D.OverlapBoxAll(point, size, angle, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                DebugExtensions.DebugBox(point, size, colliders != null && colliders.Length > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), Quaternion.Euler(0, 0, angle), drawDuration, preview);
            }

            return colliders;
        }

        #endregion

        #region OverlapBox non alloc

        public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxNonAlloc(point, size, angle, results, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxNonAlloc(point, size, angle, results, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapBoxNonAlloc(point, size, angle, results, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int count = UEPhysics2D.OverlapBoxNonAlloc(point, size, angle, results, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                DebugExtensions.DebugBox(point, size, count > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), Quaternion.Euler(0, 0, angle), drawDuration, preview);
            }

            return count;
        }

        #endregion

        #endregion

        #region OverlapCapsule

        #region OverlapCapsule single

        public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsule(point, size, direction, angle, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsule(point, size, direction, angle, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsule(point, size, direction, angle, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D collider = UEPhysics2D.OverlapCapsule(point, size, direction, angle, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                Vector2 point1;
                Vector2 point2;
                float radius;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                if (direction == CapsuleDirection2D.Vertical)
                {
                    if (size.y > size.x)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.x;
                }
                else
                {
                    if (size.x > size.y)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.y;
                }

                point1 = (Vector2)(rot * point1) + point;
                point2 = (Vector2)(rot * point2) + point;

                DebugExtensions.DebugOneSidedCapsule(point1, point2, collider ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, true, drawDuration, preview);
            }

            return collider;
        }

        public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int count = UEPhysics2D.OverlapCapsule(point, size, direction, angle, contactFilter, results);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                Vector2 point1;
                Vector2 point2;
                float radius;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                if (direction == CapsuleDirection2D.Vertical)
                {
                    if (size.y > size.x)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.x;
                }
                else
                {
                    if (size.x > size.y)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.y;
                }

                point1 = (Vector2)(rot * point1) + point;
                point2 = (Vector2)(rot * point2) + point;

                DebugExtensions.DebugOneSidedCapsule(point1, point2, count > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, true, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #region OverlapCapsule all

        public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsuleAll(point, size, direction, angle, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsuleAll(point, size, direction, angle, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsuleAll(point, size, direction, angle, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D[] colliders = UEPhysics2D.OverlapCapsuleAll(point, size, direction, angle, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                Vector2 point1;
                Vector2 point2;
                float radius;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                if (direction == CapsuleDirection2D.Vertical)
                {
                    if (size.y > size.x)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.x;
                }
                else
                {
                    if (size.x > size.y)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.y;
                }

                point1 = (Vector2)(rot * point1) + point;
                point2 = (Vector2)(rot * point2) + point;

                DebugExtensions.DebugOneSidedCapsule(point1, point2, colliders != null && colliders.Length > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, true, drawDuration, preview);
            }

            return colliders;
        }

        #endregion

        #region OverlapCapsule non alloc

        public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsuleNonAlloc(point, size, direction, angle, results, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int count = UEPhysics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                size /= 2;
                Vector2 point1;
                Vector2 point2;
                float radius;

                Quaternion rot = Quaternion.Euler(0, 0, angle);

                if (direction == CapsuleDirection2D.Vertical)
                {
                    if (size.y > size.x)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.x;
                }
                else
                {
                    if (size.x > size.y)
                    {
                        point1 = new Vector3(0, 0 - size.y + size.x);
                        point2 = new Vector3(0, 0 + size.y - size.x);
                    }
                    else
                    {
                        point1 = new Vector3(0 - .01f, 0);
                        point2 = new Vector3(0 + .01f, 0);
                    }

                    radius = size.y;
                }

                point1 = (Vector2)(rot * point1) + point;
                point2 = (Vector2)(rot * point2) + point;

                DebugExtensions.DebugOneSidedCapsule(point1, point2, count > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, true, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #endregion

        #region OverlapCircle

        #region OverlapCircle single

        public static Collider2D OverlapCircle(Vector2 point, float radius, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircle(point, radius, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircle(point, radius, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircle(point, radius, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D collider = UEPhysics2D.OverlapCircle(point, radius, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugCircle(point, Vector3.forward, collider ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, drawDuration, preview);
            }

            return collider;
        }

        public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int count = UEPhysics2D.OverlapCircle(point, radius, contactFilter, results);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugCircle(point, Vector3.forward, count > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #region OverlapCircle all

        public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircleAll(point, radius, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircleAll(point, radius, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircleAll(point, radius, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D[] colliders = UEPhysics2D.OverlapCircleAll(point, radius, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugCircle(point, Vector3.forward, colliders != null && colliders.Length > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, drawDuration, preview);
            }

            return colliders;
        }

        #endregion

        #region OverlapCircle non alloc

        public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircleNonAlloc(point, radius, results, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircleNonAlloc(point, radius, results, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapCircleNonAlloc(point, radius, results, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int count = UEPhysics2D.OverlapCircleNonAlloc(point, radius, results, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugCircle(point, Vector3.forward, count > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), radius, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #endregion

        #region OverlapPoint

        #region OverlapPoint single

        public static Collider2D OverlapPoint(Vector2 point, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPoint(point, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapPoint(Vector2 point, int layerMask, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPoint(point, layerMask, -MAX_DISTANCE, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPoint(point, layerMask, minDepth, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth, float maxDepth, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D collider = UEPhysics2D.OverlapPoint(point, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugPoint(point, collider ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), size, drawDuration, preview);
            }

            return collider;
        }

        public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int count = UEPhysics2D.OverlapPoint(point, contactFilter, results);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugPoint(point, count > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), size, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #region OverlapPoint all

        public static Collider2D[] OverlapPointAll(Vector2 point, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPointAll(point, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPointAll(point, layerMask, -MAX_DISTANCE, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPointAll(point, layerMask, minDepth, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth, float maxDepth, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            Collider2D[] colliders = UEPhysics2D.OverlapPointAll(point, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugPoint(point, colliders != null && colliders.Length > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), size, drawDuration, preview);
            }

            return colliders;
        }

        #endregion

        #region OverlapPoint non alloc

        public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPointNonAlloc(point, results, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPointNonAlloc(point, results, layerMask, -MAX_DISTANCE, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return OverlapPointNonAlloc(point, results, layerMask, minDepth, MAX_DISTANCE, size, preview, drawDuration, hitColor, noHitColor);
        }

        public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth, float maxDepth, float size = 6, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            int count = UEPhysics2D.OverlapPointNonAlloc(point, results, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                DebugExtensions.DebugPoint(point, count > 0 ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), size, drawDuration, preview);
            }

            return count;
        }

        #endregion

        #endregion

        #region RayCast

        #region RayCast single

        public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(origin, direction, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(origin, direction, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(origin, direction, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(origin, direction, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit2D hitInfo = UEPhysics2D.Raycast(origin, direction, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                Vector3 end = origin + direction * (distance == MAX_DISTANCE ? 1000 * 1000 : distance);
                bool collided = false;

                if (hitInfo.collider != null)
                {
                    collided = true;
                    end = hitInfo.point;

                    DebugExtensions.DebugPoint(end, Color.red, 0.5f, drawDuration, preview);
                }

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(origin, end, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(origin, end, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
            }

            return hitInfo;
        }


        public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return Raycast(origin, direction, contactFilter, results, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int size = UEPhysics2D.Raycast(origin, direction, contactFilter, results, distance);

            if (preview != PreviewCondition.None)
            {
                Vector3 end = origin + direction * (distance == MAX_DISTANCE ? 1000 * 1000 : distance);
                bool collided = false;

                foreach (var hitInfo in results)
                {
                    if (hitInfo.collider != null)
                    {
                        collided = true;
                        end = hitInfo.point;

                        DebugExtensions.DebugPoint(end, Color.red, 0.5f, drawDuration, preview);
                    }
                }

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(origin, end, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(origin, end, collided ? (hitColor ?? Color.green) : (noHitColor ?? Color.red), drawDuration);
            }

            return size;
        }

        #endregion

        #region RayCast all

        public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(origin, direction, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(origin, direction, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(origin, direction, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastAll(origin, direction, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            RaycastHit2D[] raycastInfo = UEPhysics2D.RaycastAll(origin, direction, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                Vector3 previewOrigin = origin;
                Vector3 sectionOrigin = origin;

                foreach (RaycastHit2D hit in raycastInfo)
                {
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    if (preview == PreviewCondition.Editor)
                        Debug.DrawLine(sectionOrigin, hit.point, (hitColor ?? Color.green), drawDuration);
                    else
                        GLDebug.DrawLine(sectionOrigin, hit.point, (hitColor ?? Color.green), drawDuration);

                    if (Vector3.Distance(origin, hit.point) > Vector3.Distance(origin, previewOrigin))
                        previewOrigin = hit.point;

                    sectionOrigin = hit.point;
                }

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(previewOrigin, origin + direction * distance, (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(previewOrigin, origin + direction * distance, (noHitColor ?? Color.red), drawDuration);
            }

            return raycastInfo;
        }

        #endregion

        #region RayCast non alloc

        public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(origin, direction, results, MAX_DISTANCE, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(origin, direction, results, distance, UEPhysics2D.AllLayers, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(origin, direction, results, distance, layerMask, -MAX_DISTANCE, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            return RaycastNonAlloc(origin, direction, results, distance, layerMask, minDepth, MAX_DISTANCE, preview, drawDuration, hitColor, noHitColor);
        }

        public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth, PreviewCondition preview = PreviewCondition.None, float drawDuration = 0, Color? hitColor = null, Color? noHitColor = null)
        {
            direction.Normalize();
            int size = UEPhysics2D.RaycastNonAlloc(origin, direction, results, distance, layerMask, minDepth, maxDepth);

            if (preview != PreviewCondition.None)
            {
                Vector3 previewOrigin = origin;
                Vector3 sectionOrigin = origin;

                foreach (RaycastHit2D hit in results)
                {
                    DebugExtensions.DebugPoint(hit.point, Color.red, 0.5f, drawDuration, preview);

                    if (preview == PreviewCondition.Editor)
                        Debug.DrawLine(sectionOrigin, hit.point, (hitColor ?? Color.green), drawDuration);
                    else
                        GLDebug.DrawLine(sectionOrigin, hit.point, (hitColor ?? Color.green), drawDuration);

                    if (Vector3.Distance(origin, hit.point) > Vector3.Distance(origin, previewOrigin))
                        previewOrigin = hit.point;

                    sectionOrigin = hit.point;
                }

                if (preview == PreviewCondition.Editor)
                    Debug.DrawLine(previewOrigin, origin + direction * distance, (noHitColor ?? Color.red), drawDuration);
                else
                    GLDebug.DrawLine(previewOrigin, origin + direction * distance, (noHitColor ?? Color.red), drawDuration);
            }

            return size;
        }

        #endregion

        #endregion

        #endregion
    }

    /// <summary>
    /// Class used to draw additional debugs, this was based of the Debug Drawing Extension from the asset store (https://www.assetstore.unity3d.com/en/#!/content/11396)
    /// </summary>
    public static class DebugExtensions
    {
        public static void DebugSquare(Vector3 origin, Vector3 halfExtents, Color color, Quaternion orientation, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            Vector3 _forward = orientation * Vector3.forward;
            Vector3 _up = orientation * Vector3.up;
            Vector3 _right = orientation * Vector3.right;

            Vector3 topMinY1 = origin + (_right * halfExtents.x) + (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 topMaxY1 = origin - (_right * halfExtents.x) + (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 botMinY1 = origin + (_right * halfExtents.x) - (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 botMaxY1 = origin - (_right * halfExtents.x) - (_up * halfExtents.y) + (_forward * halfExtents.z);

            switch (preview)
            {
                case PreviewCondition.Editor:
                    Debug.DrawLine(topMinY1, botMinY1, color, drawDuration);
                    Debug.DrawLine(topMaxY1, botMaxY1, color, drawDuration);
                    Debug.DrawLine(topMinY1, topMaxY1, color, drawDuration);
                    Debug.DrawLine(botMinY1, botMaxY1, color, drawDuration);
                    break;

                case PreviewCondition.Game:
                    GLDebug.DrawLine(topMinY1, botMinY1, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxY1, botMaxY1, color, drawDuration, true);
                    GLDebug.DrawLine(topMinY1, topMaxY1, color, drawDuration, true);
                    GLDebug.DrawLine(botMinY1, botMaxY1, color, drawDuration, true);
                    break;
            }
        }

        public static void DebugBox(Vector3 origin, Vector3 halfExtents, Vector3 direction, float maxDistance, Color color, Quaternion orientation, Color endColor, bool drawBase = true, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            direction.Normalize();
            Vector3 end = origin + direction * (maxDistance == Mathf.Infinity ? 1000 * 1000 : maxDistance);

            Vector3 _forward = orientation * Vector3.forward;
            Vector3 _up = orientation * Vector3.up;
            Vector3 _right = orientation * Vector3.right;

            #region Coords
            #region End coords
            //trans.position = end;
            Vector3 topMinX0 = end + (_right * halfExtents.x) + (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 topMaxX0 = end - (_right * halfExtents.x) + (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 topMinY0 = end + (_right * halfExtents.x) + (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 topMaxY0 = end - (_right * halfExtents.x) + (_up * halfExtents.y) + (_forward * halfExtents.z);

            Vector3 botMinX0 = end + (_right * halfExtents.x) - (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 botMaxX0 = end - (_right * halfExtents.x) - (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 botMinY0 = end + (_right * halfExtents.x) - (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 botMaxY0 = end - (_right * halfExtents.x) - (_up * halfExtents.y) + (_forward * halfExtents.z);
            #endregion

            #region Origin coords
            //trans.position = origin;
            Vector3 topMinX1 = origin + (_right * halfExtents.x) + (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 topMaxX1 = origin - (_right * halfExtents.x) + (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 topMinY1 = origin + (_right * halfExtents.x) + (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 topMaxY1 = origin - (_right * halfExtents.x) + (_up * halfExtents.y) + (_forward * halfExtents.z);

            Vector3 botMinX1 = origin + (_right * halfExtents.x) - (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 botMaxX1 = origin - (_right * halfExtents.x) - (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 botMinY1 = origin + (_right * halfExtents.x) - (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 botMaxY1 = origin - (_right * halfExtents.x) - (_up * halfExtents.y) + (_forward * halfExtents.z);
            #endregion
            #endregion

            #region Draw lines
            switch (preview)
            {
                case PreviewCondition.Editor:
                    #region Origin box
                    if (drawBase)
                    {
                        Debug.DrawLine(topMinX1, botMinX1, color, drawDuration);
                        Debug.DrawLine(topMaxX1, botMaxX1, color, drawDuration);
                        Debug.DrawLine(topMinY1, botMinY1, color, drawDuration);
                        Debug.DrawLine(topMaxY1, botMaxY1, color, drawDuration);

                        Debug.DrawLine(topMinX1, topMaxX1, color, drawDuration);
                        Debug.DrawLine(topMinX1, topMinY1, color, drawDuration);
                        Debug.DrawLine(topMinY1, topMaxY1, color, drawDuration);
                        Debug.DrawLine(topMaxY1, topMaxX1, color, drawDuration);

                        Debug.DrawLine(botMinX1, botMaxX1, color, drawDuration);
                        Debug.DrawLine(botMinX1, botMinY1, color, drawDuration);
                        Debug.DrawLine(botMinY1, botMaxY1, color, drawDuration);
                        Debug.DrawLine(botMaxY1, botMaxX1, color, drawDuration);
                    }
                    #endregion

                    #region Connection between boxes
                    Debug.DrawLine(topMinX0, topMinX1, color, drawDuration);
                    Debug.DrawLine(topMaxX0, topMaxX1, color, drawDuration);
                    Debug.DrawLine(topMinY0, topMinY1, color, drawDuration);
                    Debug.DrawLine(topMaxY0, topMaxY1, color, drawDuration);

                    Debug.DrawLine(botMinX0, botMinX1, color, drawDuration);
                    Debug.DrawLine(botMinX0, botMinX1, color, drawDuration);
                    Debug.DrawLine(botMinY0, botMinY1, color, drawDuration);
                    Debug.DrawLine(botMaxY0, botMaxY1, color, drawDuration);
                    #endregion

                    #region End box
                    color = endColor;

                    Debug.DrawLine(topMinX0, botMinX0, color, drawDuration);
                    Debug.DrawLine(topMaxX0, botMaxX0, color, drawDuration);
                    Debug.DrawLine(topMinY0, botMinY0, color, drawDuration);
                    Debug.DrawLine(topMaxY0, botMaxY0, color, drawDuration);

                    Debug.DrawLine(topMinX0, topMaxX0, color, drawDuration);
                    Debug.DrawLine(topMinX0, topMinY0, color, drawDuration);
                    Debug.DrawLine(topMinY0, topMaxY0, color, drawDuration);
                    Debug.DrawLine(topMaxY0, topMaxX0, color, drawDuration);

                    Debug.DrawLine(botMinX0, botMaxX0, color, drawDuration);
                    Debug.DrawLine(botMinX0, botMinY0, color, drawDuration);
                    Debug.DrawLine(botMinY0, botMaxY0, color, drawDuration);
                    Debug.DrawLine(botMaxY0, botMaxX0, color, drawDuration);
                    #endregion
                    break;

                case PreviewCondition.Game:
                    #region Origin box
                    if (drawBase)
                    {
                        GLDebug.DrawLine(topMinX1, botMinX1, color, drawDuration, true);
                        GLDebug.DrawLine(topMaxX1, botMaxX1, color, drawDuration, true);
                        GLDebug.DrawLine(topMinY1, botMinY1, color, drawDuration, true);
                        GLDebug.DrawLine(topMaxY1, botMaxY1, color, drawDuration, true);

                        GLDebug.DrawLine(topMinX1, topMaxX1, color, drawDuration, true);
                        GLDebug.DrawLine(topMinX1, topMinY1, color, drawDuration, true);
                        GLDebug.DrawLine(topMinY1, topMaxY1, color, drawDuration, true);
                        GLDebug.DrawLine(topMaxY1, topMaxX1, color, drawDuration, true);

                        GLDebug.DrawLine(botMinX1, botMaxX1, color, drawDuration, true);
                        GLDebug.DrawLine(botMinX1, botMinY1, color, drawDuration, true);
                        GLDebug.DrawLine(botMinY1, botMaxY1, color, drawDuration, true);
                        GLDebug.DrawLine(botMaxY1, botMaxX1, color, drawDuration, true);
                    }
                    #endregion

                    #region Connection between boxes
                    GLDebug.DrawLine(topMinX0, topMinX1, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxX0, topMaxX1, color, drawDuration, true);
                    GLDebug.DrawLine(topMinY0, topMinY1, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxY0, topMaxY1, color, drawDuration, true);

                    GLDebug.DrawLine(botMinX0, botMinX1, color, drawDuration, true);
                    GLDebug.DrawLine(botMinX0, botMinX1, color, drawDuration, true);
                    GLDebug.DrawLine(botMinY0, botMinY1, color, drawDuration, true);
                    GLDebug.DrawLine(botMaxY0, botMaxY1, color, drawDuration, true);
                    #endregion

                    #region End box
                    color = endColor;

                    GLDebug.DrawLine(topMinX0, botMinX0, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxX0, botMaxX0, color, drawDuration, true);
                    GLDebug.DrawLine(topMinY0, botMinY0, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxY0, botMaxY0, color, drawDuration, true);

                    GLDebug.DrawLine(topMinX0, topMaxX0, color, drawDuration, true);
                    GLDebug.DrawLine(topMinX0, topMinY0, color, drawDuration, true);
                    GLDebug.DrawLine(topMinY0, topMaxY0, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxY0, topMaxX0, color, drawDuration, true);

                    GLDebug.DrawLine(botMinX0, botMaxX0, color, drawDuration, true);
                    GLDebug.DrawLine(botMinX0, botMinY0, color, drawDuration, true);
                    GLDebug.DrawLine(botMinY0, botMaxY0, color, drawDuration, true);
                    GLDebug.DrawLine(botMaxY0, botMaxX0, color, drawDuration, true);
                    #endregion
                    break;
            }
            #endregion
        }

        public static void DebugBox(Vector3 origin, Vector3 halfExtents, Color color, Quaternion orientation, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            Vector3 _forward = orientation * Vector3.forward;
            Vector3 _up = orientation * Vector3.up;
            Vector3 _right = orientation * Vector3.right;

            Vector3 topMinX1 = origin + (_right * halfExtents.x) + (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 topMaxX1 = origin - (_right * halfExtents.x) + (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 topMinY1 = origin + (_right * halfExtents.x) + (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 topMaxY1 = origin - (_right * halfExtents.x) + (_up * halfExtents.y) + (_forward * halfExtents.z);

            Vector3 botMinX1 = origin + (_right * halfExtents.x) - (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 botMaxX1 = origin - (_right * halfExtents.x) - (_up * halfExtents.y) - (_forward * halfExtents.z);
            Vector3 botMinY1 = origin + (_right * halfExtents.x) - (_up * halfExtents.y) + (_forward * halfExtents.z);
            Vector3 botMaxY1 = origin - (_right * halfExtents.x) - (_up * halfExtents.y) + (_forward * halfExtents.z);

            switch (preview)
            {
                case PreviewCondition.Editor:
                    Debug.DrawLine(topMinX1, botMinX1, color, drawDuration);
                    Debug.DrawLine(topMaxX1, botMaxX1, color, drawDuration);
                    Debug.DrawLine(topMinY1, botMinY1, color, drawDuration);
                    Debug.DrawLine(topMaxY1, botMaxY1, color, drawDuration);

                    Debug.DrawLine(topMinX1, topMaxX1, color, drawDuration);
                    Debug.DrawLine(topMinX1, topMinY1, color, drawDuration);
                    Debug.DrawLine(topMinY1, topMaxY1, color, drawDuration);
                    Debug.DrawLine(topMaxY1, topMaxX1, color, drawDuration);

                    Debug.DrawLine(botMinX1, botMaxX1, color, drawDuration);
                    Debug.DrawLine(botMinX1, botMinY1, color, drawDuration);
                    Debug.DrawLine(botMinY1, botMaxY1, color, drawDuration);
                    Debug.DrawLine(botMaxY1, botMaxX1, color, drawDuration);
                    break;

                case PreviewCondition.Game:
                    GLDebug.DrawLine(topMinX1, botMinX1, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxX1, botMaxX1, color, drawDuration, true);
                    GLDebug.DrawLine(topMinY1, botMinY1, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxY1, botMaxY1, color, drawDuration, true);

                    GLDebug.DrawLine(topMinX1, topMaxX1, color, drawDuration, true);
                    GLDebug.DrawLine(topMinX1, topMinY1, color, drawDuration, true);
                    GLDebug.DrawLine(topMinY1, topMaxY1, color, drawDuration, true);
                    GLDebug.DrawLine(topMaxY1, topMaxX1, color, drawDuration, true);

                    GLDebug.DrawLine(botMinX1, botMaxX1, color, drawDuration, true);
                    GLDebug.DrawLine(botMinX1, botMinY1, color, drawDuration, true);
                    GLDebug.DrawLine(botMinY1, botMaxY1, color, drawDuration, true);
                    GLDebug.DrawLine(botMaxY1, botMaxX1, color, drawDuration, true);
                    break;
            }
        }

        public static void DebugOneSidedCapsule(Vector3 baseSphere, Vector3 endSphere, Color color, float radius = 1, bool colorizeBase = false, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            Vector3 up = (endSphere - baseSphere).normalized * radius;
            Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
            Vector3 right = Vector3.Cross(up, forward).normalized * radius;

            //Side lines
            switch (preview)
            {
                case PreviewCondition.Editor:
                    Debug.DrawLine(baseSphere + right, endSphere + right, color, drawDuration);
                    Debug.DrawLine(baseSphere - right, endSphere - right, color, drawDuration);

                    //Draw end caps
                    for (int i = 1; i < 26; i++)
                    {
                        //Start endcap
                        Debug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + baseSphere, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration);
                        Debug.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + baseSphere, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration);

                        //End endcap
                        Debug.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + endSphere, Vector3.Slerp(right, up, (i - 1) / 25.0f) + endSphere, color, drawDuration);
                        Debug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + endSphere, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + endSphere, color, drawDuration);
                    }
                    break;

                case PreviewCondition.Game:
                    GLDebug.DrawLine(baseSphere + right, endSphere + right, color, drawDuration, true);
                    GLDebug.DrawLine(baseSphere - right, endSphere - right, color, drawDuration, true);

                    //Draw end caps
                    for (int i = 1; i < 26; i++)
                    {
                        //Start endcap
                        GLDebug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + baseSphere, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration, true);
                        GLDebug.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + baseSphere, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration, true);

                        //End endcap
                        GLDebug.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + endSphere, Vector3.Slerp(right, up, (i - 1) / 25.0f) + endSphere, color, drawDuration, true);
                        GLDebug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + endSphere, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + endSphere, color, drawDuration, true);
                    }
                    break;
            }
        }

        public static void DebugCapsule(Vector3 baseSphere, Vector3 endSphere, Color color, float radius = 1, bool colorizeBase = true, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            Vector3 up = (endSphere - baseSphere).normalized * radius;
            Vector3 forward = Vector3.Slerp(up, -up, 0.5f);
            Vector3 right = Vector3.Cross(up, forward).normalized * radius;

            //Radial circles
            DebugCircle(baseSphere, up, colorizeBase ? color : Color.red, radius, drawDuration, preview);
            DebugCircle(endSphere, -up, color, radius, drawDuration, preview);

            switch (preview)
            {
                case PreviewCondition.Editor:
                    //Side lines
                    Debug.DrawLine(baseSphere + right, endSphere + right, color, drawDuration);
                    Debug.DrawLine(baseSphere - right, endSphere - right, color, drawDuration);

                    Debug.DrawLine(baseSphere + forward, endSphere + forward, color, drawDuration);
                    Debug.DrawLine(baseSphere - forward, endSphere - forward, color, drawDuration);

                    //Draw end caps
                    for (int i = 1; i < 26; i++)
                    {
                        //End endcap
                        Debug.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + endSphere, Vector3.Slerp(right, up, (i - 1) / 25.0f) + endSphere, color, drawDuration);
                        Debug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + endSphere, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + endSphere, color, drawDuration);
                        Debug.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + endSphere, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + endSphere, color, drawDuration);
                        Debug.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + endSphere, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + endSphere, color, drawDuration);

                        //Start endcap
                        Debug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + baseSphere, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration);
                        Debug.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + baseSphere, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration);
                        Debug.DrawLine(Vector3.Slerp(forward, -up, i / 25.0f) + baseSphere, Vector3.Slerp(forward, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration);
                        Debug.DrawLine(Vector3.Slerp(-forward, -up, i / 25.0f) + baseSphere, Vector3.Slerp(-forward, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration);
                    }
                    break;
                case PreviewCondition.Game:
                    //Side lines
                    GLDebug.DrawLine(baseSphere + right, endSphere + right, color, drawDuration, true);
                    GLDebug.DrawLine(baseSphere - right, endSphere - right, color, drawDuration, true);

                    GLDebug.DrawLine(baseSphere + forward, endSphere + forward, color, drawDuration, true);
                    GLDebug.DrawLine(baseSphere - forward, endSphere - forward, color, drawDuration, true);

                    //Draw end caps
                    for (int i = 1; i < 26; i++)
                    {
                        //End endcap
                        GLDebug.DrawLine(Vector3.Slerp(right, up, i / 25.0f) + endSphere, Vector3.Slerp(right, up, (i - 1) / 25.0f) + endSphere, color, drawDuration, true);
                        GLDebug.DrawLine(Vector3.Slerp(-right, up, i / 25.0f) + endSphere, Vector3.Slerp(-right, up, (i - 1) / 25.0f) + endSphere, color, drawDuration, true);
                        GLDebug.DrawLine(Vector3.Slerp(forward, up, i / 25.0f) + endSphere, Vector3.Slerp(forward, up, (i - 1) / 25.0f) + endSphere, color, drawDuration, true);
                        GLDebug.DrawLine(Vector3.Slerp(-forward, up, i / 25.0f) + endSphere, Vector3.Slerp(-forward, up, (i - 1) / 25.0f) + endSphere, color, drawDuration, true);

                        //Start endcap
                        GLDebug.DrawLine(Vector3.Slerp(right, -up, i / 25.0f) + baseSphere, Vector3.Slerp(right, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration, true);
                        GLDebug.DrawLine(Vector3.Slerp(-right, -up, i / 25.0f) + baseSphere, Vector3.Slerp(-right, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration, true);
                        GLDebug.DrawLine(Vector3.Slerp(forward, -up, i / 25.0f) + baseSphere, Vector3.Slerp(forward, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration, true);
                        GLDebug.DrawLine(Vector3.Slerp(-forward, -up, i / 25.0f) + baseSphere, Vector3.Slerp(-forward, -up, (i - 1) / 25.0f) + baseSphere, colorizeBase ? color : Color.red, drawDuration, true);
                    }
                    break;
            }
        }

        public static void DebugCircle(Vector3 position, Vector3 up, Color color, float radius = 1.0f, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            Vector3 _up = up.normalized * radius;
            Vector3 _forward = Vector3.Slerp(_up, -_up, 0.5f);
            Vector3 _right = Vector3.Cross(_up, _forward).normalized * radius;

            Matrix4x4 matrix = new Matrix4x4();

            matrix[0] = _right.x;
            matrix[1] = _right.y;
            matrix[2] = _right.z;

            matrix[4] = _up.x;
            matrix[5] = _up.y;
            matrix[6] = _up.z;

            matrix[8] = _forward.x;
            matrix[9] = _forward.y;
            matrix[10] = _forward.z;

            Vector3 _lastPoint = position + matrix.MultiplyPoint3x4(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)));
            Vector3 _nextPoint = Vector3.zero;

            color = (color == default(Color)) ? Color.white : color;

            for (var i = 0; i < 91; i++)
            {
                _nextPoint.x = Mathf.Cos((i * 4) * Mathf.Deg2Rad);
                _nextPoint.z = Mathf.Sin((i * 4) * Mathf.Deg2Rad);
                _nextPoint.y = 0;

                _nextPoint = position + matrix.MultiplyPoint3x4(_nextPoint);

                switch (preview)
                {
                    case PreviewCondition.Editor:
                        Debug.DrawLine(_lastPoint, _nextPoint, color, drawDuration);
                        break;

                    case PreviewCondition.Game:
                        GLDebug.DrawLine(_lastPoint, _nextPoint, color, drawDuration, true);
                        break;
                }
                _lastPoint = _nextPoint;
            }
        }

        public static void DebugPoint(Vector3 position, Color color, float scale = 0.5f, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            color = (color == default(Color)) ? Color.white : color;

            switch (preview)
            {
                case PreviewCondition.Editor:
                    Debug.DrawRay(position + (Vector3.up * (scale * 0.5f)), -Vector3.up * scale, color, drawDuration);
                    Debug.DrawRay(position + (Vector3.right * (scale * 0.5f)), -Vector3.right * scale, color, drawDuration);
                    Debug.DrawRay(position + (Vector3.forward * (scale * 0.5f)), -Vector3.forward * scale, color, drawDuration);
                    break;

                case PreviewCondition.Game:
                    GLDebug.DrawRay(position + (Vector3.up * (scale * 0.5f)), -Vector3.up * scale, color, drawDuration, true);
                    GLDebug.DrawRay(position + (Vector3.right * (scale * 0.5f)), -Vector3.right * scale, color, drawDuration, true);
                    GLDebug.DrawRay(position + (Vector3.forward * (scale * 0.5f)), -Vector3.forward * scale, color, drawDuration, true);
                    break;
            }
        }

        public static void DebugWireSphere(Vector3 position, Color color, float radius = 1.0f, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            float angle = 10.0f;

            Vector3 x = new Vector3(position.x, position.y + radius * Mathf.Sin(0), position.z + radius * Mathf.Cos(0));
            Vector3 y = new Vector3(position.x + radius * Mathf.Cos(0), position.y, position.z + radius * Mathf.Sin(0));
            Vector3 z = new Vector3(position.x + radius * Mathf.Cos(0), position.y + radius * Mathf.Sin(0), position.z);

            Vector3 new_x;
            Vector3 new_y;
            Vector3 new_z;

            for (int i = 1; i < 37; i++)
            {
                new_x = new Vector3(position.x, position.y + radius * Mathf.Sin(angle * i * Mathf.Deg2Rad), position.z + radius * Mathf.Cos(angle * i * Mathf.Deg2Rad));
                new_y = new Vector3(position.x + radius * Mathf.Cos(angle * i * Mathf.Deg2Rad), position.y, position.z + radius * Mathf.Sin(angle * i * Mathf.Deg2Rad));
                new_z = new Vector3(position.x + radius * Mathf.Cos(angle * i * Mathf.Deg2Rad), position.y + radius * Mathf.Sin(angle * i * Mathf.Deg2Rad), position.z);

                switch (preview)
                {
                    case PreviewCondition.Editor:
                        Debug.DrawLine(x, new_x, color, drawDuration);
                        Debug.DrawLine(y, new_y, color, drawDuration);
                        Debug.DrawLine(z, new_z, color, drawDuration);
                        break;

                    case PreviewCondition.Game:
                        GLDebug.DrawLine(x, new_x, color, drawDuration, true);
                        GLDebug.DrawLine(y, new_y, color, drawDuration, true);
                        GLDebug.DrawLine(z, new_z, color, drawDuration, true);
                        break;
                }

                x = new_x;
                y = new_y;
                z = new_z;
            }
        }

        public static void DebugConeSight(Vector3 position, Vector3 direction, float length, Color color, float angle = 45, float drawDuration = 0, PreviewCondition preview = PreviewCondition.Editor)
        {
            if (angle > 0)
                angle = Mathf.Min(angle, 360);
            else
                angle = Mathf.Max(angle, -360);

            direction.Normalize();
            Vector3 _forward = direction * length;
            Vector3 _up = Vector3.Slerp(_forward, -_forward, 0.5f);
            Vector3 _right = Vector3.Cross(_forward, _up).normalized * length;

            Vector3 up;
            Vector3 rightUp;
            Vector3 right;
            Vector3 rightDown;
            Vector3 down;
            Vector3 leftDown;
            Vector3 left;
            Vector3 leftUp;

            if (angle <= 180)
            {
                float percentage = angle / 180f;

                up = position + Vector3.Slerp(_forward, -_right, percentage).normalized * length;
                rightUp = position + Vector3.Slerp(_forward, -_up - _right, percentage).normalized * length;
                right = position + Vector3.Slerp(_forward, -_up, percentage).normalized * length;
                rightDown = position + Vector3.Slerp(_forward, -_up + _right, percentage).normalized * length;
                down = position + Vector3.Slerp(_forward, _right, percentage).normalized * length;
                leftDown = position + Vector3.Slerp(_forward, _up + _right, percentage).normalized * length;
                left = position + Vector3.Slerp(_forward, _up, percentage).normalized * length;
                leftUp = position + Vector3.Slerp(_forward, _up - _right, percentage).normalized * length;
            }
            else
            {
                float percentage = (angle - 180) / 180f;

                up = position + Vector3.Slerp(-_right, -_forward, percentage).normalized * length;
                rightUp = position + Vector3.Slerp(-_up - _right, -_forward, percentage).normalized * length;
                right = position + Vector3.Slerp(-_up, -_forward, percentage).normalized * length;
                rightDown = position + Vector3.Slerp(-_up + _right, -_forward, percentage).normalized * length;
                down = position + Vector3.Slerp(_right, -_forward, percentage).normalized * length;
                leftDown = position + Vector3.Slerp(_up + _right, -_forward, percentage).normalized * length;
                left = position + Vector3.Slerp(_up, -_forward, percentage).normalized * length;
                leftUp = position + Vector3.Slerp(_up - _right, -_forward, percentage).normalized * length;
            }

            #region Rays Logic
            switch (preview)
            {
                case PreviewCondition.Editor:
                    //Forward
                    Debug.DrawRay(position, _forward, color, drawDuration);

                    //Left Down
                    Debug.DrawLine(position, leftDown, color, drawDuration);
                    //Left Up
                    Debug.DrawLine(position, leftUp, color, drawDuration);
                    //Right Down
                    Debug.DrawLine(position, rightDown, color, drawDuration);
                    //Right Up
                    Debug.DrawLine(position, rightUp, color, drawDuration);

                    //Left
                    Debug.DrawLine(position, left, color, drawDuration);
                    //Right
                    Debug.DrawLine(position, right, color, drawDuration);
                    //Down
                    Debug.DrawLine(position, down, color, drawDuration);
                    //Up
                    Debug.DrawLine(position, up, color, drawDuration);
                    break;

                case PreviewCondition.Game:
                    //Forward
                    GLDebug.DrawRay(position, _forward, color, drawDuration, true);

                    //Left Down
                    GLDebug.DrawLine(position, leftDown, color, drawDuration, true);
                    //Left Up
                    GLDebug.DrawLine(position, leftUp, color, drawDuration, true);
                    //Right Down
                    GLDebug.DrawLine(position, rightDown, color, drawDuration, true);
                    //Right Up
                    GLDebug.DrawLine(position, rightUp, color, drawDuration, true);

                    //Left
                    GLDebug.DrawLine(position, left, color, drawDuration, true);
                    //Right
                    GLDebug.DrawLine(position, right, color, drawDuration, true);
                    //Down
                    GLDebug.DrawLine(position, down, color, drawDuration, true);
                    //Up
                    GLDebug.DrawLine(position, up, color, drawDuration, true);
                    break;
            }
            #endregion

            #region Circles
            Vector3 midUp = (up + position) / 2;
            Vector3 midDown = (down + position) / 2;

            Vector3 endPoint = (up + down) / 2;
            Vector3 midPoint = (midUp + midDown) / 2;

            DebugCircle(endPoint, direction, color, Vector3.Distance(up, down) / 2, drawDuration, preview);
            DebugCircle(midPoint, direction, color, Vector3.Distance(midUp, midDown) / 2, drawDuration, preview);
            #endregion

            #region Cone base sphere logic
            Vector3 _lastLDPosition = leftDown;
            Vector3 _lastLUPosition = leftUp;
            Vector3 _lastRDPosition = rightDown;
            Vector3 _lastRUPosition = rightUp;

            Vector3 _lastLPosition = left;
            Vector3 _lastRPosition = right;
            Vector3 _lastDPosition = down;
            Vector3 _lastUPosition = up;

            Vector3 _nextLDPosition;
            Vector3 _nextLUPosition;
            Vector3 _nextRDPosition;
            Vector3 _nextRUPosition;

            Vector3 _nextLPosition;
            Vector3 _nextRPosition;
            Vector3 _nextDPosition;
            Vector3 _nextUPosition;

            int index = 1;

            for (var i = 0; i < 7; i++)
            {
                float tempAngle = angle - index;

                if (tempAngle <= 180)
                {
                    float percentage = tempAngle / 180f;

                    _nextLDPosition = position + Vector3.Slerp(_forward, _up + _right, percentage).normalized * length;
                    _nextLUPosition = position + Vector3.Slerp(_forward, _up - _right, percentage).normalized * length;
                    _nextRDPosition = position + Vector3.Slerp(_forward, -_up + _right, percentage).normalized * length;
                    _nextRUPosition = position + Vector3.Slerp(_forward, -_up - _right, percentage).normalized * length;

                    _nextDPosition = position + Vector3.Slerp(_forward, _right, percentage).normalized * length;
                    _nextUPosition = position + Vector3.Slerp(_forward, -_right, percentage).normalized * length;
                    _nextRPosition = position + Vector3.Slerp(_forward, -_up, percentage).normalized * length;
                    _nextLPosition = position + Vector3.Slerp(_forward, _up, percentage).normalized * length;
                }
                else
                {
                    float percentage = (tempAngle - 180) / 180f;

                    _nextLDPosition = position + Vector3.Slerp(_up + _right, -_forward, percentage).normalized * length;
                    _nextLUPosition = position + Vector3.Slerp(_up - _right, -_forward, percentage).normalized * length;
                    _nextRDPosition = position + Vector3.Slerp(-_up + _right, -_forward, percentage).normalized * length;
                    _nextRUPosition = position + Vector3.Slerp(-_up - _right, -_forward, percentage).normalized * length;

                    _nextDPosition = position + Vector3.Slerp(_right, -_forward, percentage).normalized * length;
                    _nextUPosition = position + Vector3.Slerp(-_right, -_forward, percentage).normalized * length;
                    _nextRPosition = position + Vector3.Slerp(-_up, -_forward, percentage).normalized * length;
                    _nextLPosition = position + Vector3.Slerp(_up, -_forward, percentage).normalized * length;
                }

                switch (preview)
                {
                    case PreviewCondition.Editor:
                        Debug.DrawLine(_lastLDPosition, _nextLDPosition, color, drawDuration);
                        Debug.DrawLine(_lastLUPosition, _nextLUPosition, color, drawDuration);
                        Debug.DrawLine(_lastRDPosition, _nextRDPosition, color, drawDuration);
                        Debug.DrawLine(_lastRUPosition, _nextRUPosition, color, drawDuration);

                        Debug.DrawLine(_lastDPosition, _nextDPosition, color, drawDuration);
                        Debug.DrawLine(_lastUPosition, _nextUPosition, color, drawDuration);
                        Debug.DrawLine(_lastRPosition, _nextRPosition, color, drawDuration);
                        Debug.DrawLine(_lastLPosition, _nextLPosition, color, drawDuration);
                        break;

                    case PreviewCondition.Game:
                        GLDebug.DrawLine(_lastLDPosition, _nextLDPosition, color, drawDuration, true);
                        GLDebug.DrawLine(_lastLUPosition, _nextLUPosition, color, drawDuration, true);
                        GLDebug.DrawLine(_lastRDPosition, _nextRDPosition, color, drawDuration, true);
                        GLDebug.DrawLine(_lastRUPosition, _nextRUPosition, color, drawDuration, true);

                        GLDebug.DrawLine(_lastDPosition, _nextDPosition, color, drawDuration, true);
                        GLDebug.DrawLine(_lastUPosition, _nextUPosition, color, drawDuration, true);
                        GLDebug.DrawLine(_lastRPosition, _nextRPosition, color, drawDuration, true);
                        GLDebug.DrawLine(_lastLPosition, _nextLPosition, color, drawDuration, true);
                        break;
                }

                _lastLDPosition = _nextLDPosition;
                _lastLUPosition = _nextLUPosition;
                _lastRDPosition = _nextRDPosition;
                _lastRUPosition = _nextRUPosition;

                _lastDPosition = _nextDPosition;
                _lastUPosition = _nextUPosition;
                _lastRPosition = _nextRPosition;
                _lastLPosition = _nextLPosition;
                index += 60;
            }
            #endregion
        }
    }
}