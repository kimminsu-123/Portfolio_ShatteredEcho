using UnityEngine;

namespace ShEcho.Core
{
    public abstract class Sensor
    {
        public LayerMask DetectLayer { get; private set; }
        public RaycastHit[] Hits { get; private set; }
        
        protected Sensor(LayerMask layerMask, int detectCount)
        {
            DetectLayer = layerMask;
            Hits = new RaycastHit[detectCount];
        }
        
        public abstract int Detect(Vector3 origin, Vector3 direction);
    }
}