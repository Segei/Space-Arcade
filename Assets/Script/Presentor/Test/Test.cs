using Assets.Script.Model;
using Assets.Script.Model.Borders;
using Assets.Script.Model.UpdateSystem;
using TMPro;
using UnityEngine;

namespace Assets.Script.Presentor.Test
{
    public class Test : MonoBehaviour
    {
        private StartUpdate update;
        private Ship ship;
        [SerializeField] private float maxSpeed, acceleration, turnAcceleration;
        [SerializeField] private RectTransform viewShip, canvas;
        [SerializeField] private TMP_Text position, rotation, velocity;
        [SerializeField] private int FramePerSecond = 1;
        private void Start()
        {

            Portals portals = new(new(canvas.sizeDelta.x, canvas.sizeDelta.y));
            update = new StartUpdate
            {
                FramePerSecond = FramePerSecond
            };
            ship = new Ship();
            portals.AddActors(ship);
            ship.Position = (new(canvas.sizeDelta.x / 2, canvas.sizeDelta.y / 2));
            ship.MaxSpeed = maxSpeed;
            ship.Acceleration = acceleration;
            ship.TurnAcceleration = turnAcceleration;
            ship.SecondsToStop = 30;


            // update.OnUpdate += UpdateFromModel;
            update.AddListener(ship);
            update.AddListener(portals);
            update.Start();
        }

        private void OnDisable()
        {
            if (update != null)
            {
                update.Stop();
            }
        }

        private void Update()
        {
            ship.SetTurn(-Input.GetAxis("Horizontal"));
            if (Input.GetKey(KeyCode.W))
            {
                ship.MoveForward();
            }
            position.text = ship.Position.ToString();
            rotation.text = ship.Turn.ToString();
            velocity.text = Mathf.RoundToInt(ship.Velocity.Length()).ToString();
            viewShip.rotation = Quaternion.Euler(0, 0, ship.Turn);
            viewShip.position = new Vector3(ship.Position.X, ship.Position.Y, 0);
        }
    }
}
