using Script.Model.Physics;
using UnityEngine;

namespace Script.View.VIewElement
{
    [RequireComponent(typeof(RectTransform))]
    public class ViewEntity : MonoBehaviour
    {
        public RectTransform RectTransform;
        private Transformable tartget;
        public float Width => RectTransform.sizeDelta.x;
        public float Height => RectTransform.sizeDelta.y;

        public void SetTarget(Transformable value)
        {
            tartget = value;
        }

        private void OnValidate()
        {
            if (RectTransform == null)
            {
                RectTransform = GetComponent<RectTransform>();
            }
        }

        private void Update()
        {
            if (tartget == null)
            {
                return;
            }

            RectTransform.rotation = Quaternion.Euler(0, 0, tartget.Turn);
            RectTransform.position = new Vector3(tartget.Position.X, tartget.Position.Y, 0);
        }
    }
}