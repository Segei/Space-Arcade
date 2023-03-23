using Script.Model.Physics;
using TMPro;
using UnityEngine;

namespace Script.View.VIewElement
{
    public class ViewShipPosition : MonoBehaviour
    {
        [SerializeField] private TMP_Text position, rotation, velocity;
        private Transformable target;


        public void SetTarget(Transformable value)
        {
            target = value;
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }


            position.text = string.Format("Позиция {0}, {1}", target.Position.X, target.Position.Y);
            rotation.text = string.Format("Угол поворота {0}", target.Turn);
            velocity.text = string.Format("Скорость {0}", Mathf.RoundToInt(target.Velocity.Length()));
        }
    }
}
