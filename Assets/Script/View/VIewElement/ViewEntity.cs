using Script.Model.Physics;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.View.VIewElement
{
    [RequireComponent(typeof(RectTransform))]
    public class ViewEntity : MonoBehaviour
    {
        private RectTransform rectTransform;
        private Transformable tartget;
        public float Width => rectTransform.sizeDelta.x;
        public float Height => rectTransform.sizeDelta.y;

        public void SetTarget(Transformable value)
        {
            tartget = value;
        }

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (tartget == null)
            {
                return;
            }

            rectTransform.rotation = Quaternion.Euler(0, 0, tartget.Turn);
            rectTransform.position = new Vector3(tartget.Position.X, tartget.Position.Y, 0);
        }
    }
}