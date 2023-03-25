using Script.Model.Entities;
using UnityEngine;
using Zenject;

namespace Assets.Script.View
{
    public class InputKeyboard : MonoBehaviour
    {
        [Inject] private Ship ship;
        private ShipMovable shipMovable;


        private void Start()
        {
            shipMovable = (ShipMovable)ship.Transformable;
        }

        private void Update()
        {
            shipMovable.SetTurn(-Input.GetAxis("Horizontal"));
            if (Input.GetKey(KeyCode.W))
            {
                shipMovable.MoveForward();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                ship.MainWeapon.Attack();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                ship.SecondWeapon.Attack();
            }
        }
    }
}
