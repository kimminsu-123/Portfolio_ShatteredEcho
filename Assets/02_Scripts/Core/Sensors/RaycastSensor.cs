using UnityEngine;

namespace ShEcho.Core
{
    public class RaycastSensor : Sensor
    {
        public float Length { get; private set; }
        
        public RaycastSensor(LayerMask layerMask, int detectCount, float length) : base(layerMask, detectCount)
        {
            Length = length;
        }

        public override int Detect(Vector3 origin, Vector3 direction)
        {
            Ray ray = new Ray(origin, direction);

            int hit = Physics.RaycastNonAlloc(ray, Hits, Length, DetectLayer);

            return hit;
        }
    }
}