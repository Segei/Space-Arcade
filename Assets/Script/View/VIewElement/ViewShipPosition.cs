using Script.Model.Entities;
using Script.Model.Physics;
using TMPro;
using UnityEngine;
using Zenject;

namespace Script.View.VIewElement
{
    public class ViewShipPosition : MonoBehaviour
    {
        [Inject] private Ship ship;
        [SerializeField] private TMP_Text position, rotation, velocity;
        private Transformable target;

        private void Start()
        {
            target = ship.Transformable;
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }

            position.text = string.Format("Позиция {0}, {1}", target.Position.X, target.Position.Y);
            rotation.text = string.Format("Угол поворота {0}", target.Turn);
            velocity.text = string.Format("Скорость {0}", target.Speed);
        }
    }
}
