using System;
using ShEcho.Core;
using ShEcho.UIs.Player;
using ShEcho.Utils;
using UnityEngine;

namespace ShEcho.Controller
{
    public class InteractController : MonoBehaviour
    {
        public LayerMask targetLayer;
        public Vector3 detectDirection = Vector3.forward;
        public Vector3 detectOffset;
        public float detectLenght;
        public float cooldownTime;

        private Sensor _detectSensor;
        private CooldownTimer _interactCooldownTimer;

        private void Start()
        {
            _detectSensor = new RaycastSensor(targetLayer, 1, detectLenght);
            _interactCooldownTimer = new CooldownTimer(cooldownTime);
        }

        public void Interact()
        {
            Vector3 origin = transform.position + detectOffset;
            Vector3 direction = transform.TransformDirection(detectDirection);

            int count = _detectSensor.Detect(origin, direction);
            if (count > 0)
            {
                IInteractable interactable = _detectSensor.Hits[0].collider.GetComponent<IInteractable>();

                interactable?.Interact(gameObject);
            }

            _interactCooldownTimer.SetTime(cooldownTime);            
            _interactCooldownTimer.Start();
        }

        private void Update()
        {
            _interactCooldownTimer.Tick(Time.deltaTime);
            
            UIManager.Instance.Get<MobileInputUI>().SetInteractCooldownValue(_interactCooldownTimer.CurrentTime / _interactCooldownTimer.Time);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(detectOffset, detectOffset + detectDirection * detectLenght);
        }
    }
}