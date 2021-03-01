using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lean.Touch
{
    public class LeanTouchRotate : MonoBehaviour
    {
        [Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreGuiFingers;
        [Tooltip("Ignore fingers with IsOverGui?")]
        public bool IgnoreIsOverGui;
        [Tooltip("Allows you to force rotation with a specific amount of fingers (0 = any)")]
        public int RequiredFingerCount;

        [Tooltip("Does rotation require an object to be selected?")]
        public LeanSelectable RequiredSelectable;

        [Tooltip("The camera we will be moving (None = MainCamera)")]
        public Camera Camera;


#if UNITY_EDITOR
        protected virtual void Reset()
        {
            Start();
        }
#endif

        protected virtual void Start()
        {
            if (RequiredSelectable == null)
            {
                RequiredSelectable = GetComponent<LeanSelectable>();
            }
        }

        protected virtual void Update()
        {
            // If we require a selectable and it isn't selected, cancel rotation
            if (RequiredSelectable != null && RequiredSelectable.IsSelected == true)
            {
                return;
            }

            // Get the fingers we want to use
            var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, IgnoreIsOverGui, RequiredFingerCount);

            // Calculate the rotation values based on these fingers
            var center = LeanGesture.GetScreenCenter(fingers);
            var degrees = LeanGesture.GetTwistDegrees(fingers);

            var screenDelta = LeanGesture.GetScreenDelta(fingers);

            // Perform the rotation
            Rotate(screenDelta.x, screenDelta.y);
        }

        private void Rotate(float x, float y)
        {
            Vector3 ang = new Vector3(0, -x * Mathf.Deg2Rad * 10f, 0);
            transform.localEulerAngles += ang;
        }

    }
}
