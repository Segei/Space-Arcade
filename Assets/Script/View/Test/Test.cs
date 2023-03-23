using Script.Model.Borders;
using Script.Model.Entities;
using Script.Model.Physics;
using Script.Model.UpdateSystem;
using Script.View.VIewElement;
using UnityEngine;

namespace Script.View.Test
{
    public class Test : MonoBehaviour
    {
        private StartUpdate update;
        private EntityContainer conteiner;
        [SerializeField] private ViewEntity viewShip, viewEnemy;
        [SerializeField] private ViewShipPosition viewShipPosition;
        [SerializeField] private float maxSpeed, acceleration, turnAcceleration;
        [SerializeField] private RectTransform canvas;

        [SerializeField] private int FramePerSecond = 1;
        private void Start()
        {
            conteiner = new EntityContainer();
            Portals portals = new(new(canvas.sizeDelta.x, canvas.sizeDelta.y));
            update = new StartUpdate
            {
                FramePerSecond = FramePerSecond
            };
            conteiner.Ship = new Ship();
            conteiner.Ship.Destroyed += () => Debug.Log("Ship Destroyed!");
            Debug.Log(conteiner.Ship.Transformable.GetType());
            viewShip.SetTarget(conteiner.Ship.Transformable);
            conteiner.Ship.Collision.HalfWidth = viewShip.Width / 2;
            conteiner.Ship.Collision.HalfHeight = viewShip.Height / 2;

            DetectorCollision detectorCollision = new DetectorCollision(conteiner);
            viewShipPosition.SetTarget(conteiner.Ship.Transformable);
            conteiner.Ship.Transformable.Position = (new(canvas.sizeDelta.x / 2, canvas.sizeDelta.y / 2));
            SetSettingsShip(conteiner.Ship.Transformable);
            portals.AddActors(conteiner.Ship.Transformable);
            NLO nLO = new();
            conteiner.NLOs.Add(nLO);
            nLO.Transformable.Target = conteiner.Ship.Transformable;
            viewEnemy.SetTarget(nLO.Transformable);
            nLO.Collision.HalfWidth = viewEnemy.Width;
            nLO.Collision.HalfHeight = viewEnemy.Height;
            SetSettingsShip(nLO.Transformable);
            nLO.Transformable.TurnAcceleration = FramePerSecond;
            nLO.Transformable.Acceleration = FramePerSecond < 40 ? acceleration * (40 / FramePerSecond) : acceleration;
            update.AddListener(conteiner.Ship.Transformable);
            update.AddListener(portals);
            update.AddListener(nLO.Transformable);
            update.AddListener(detectorCollision);
            update.Start();
        }

        private void SetSettingsShip(ShipEngienSettings settings)
        {
            settings.MaxSpeed = maxSpeed;
            settings.Acceleration = acceleration;
            settings.TurnAcceleration = turnAcceleration;
            settings.SecondsToStop = 30;
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
            conteiner.Ship.Transformable.SetTurn(-Input.GetAxis("Horizontal"));
            if (Input.GetKey(KeyCode.W))
            {
                conteiner.Ship.Transformable.MoveForward();
            }
        }
    }
}
