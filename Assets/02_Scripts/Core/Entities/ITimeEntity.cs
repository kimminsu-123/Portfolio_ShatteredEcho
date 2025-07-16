using ShEcho.Utils;
using UnityEngine;

namespace ShEcho.Core
{
    public interface ITimeEntity
    {
        public void Initialize();
        public void OnEnabled();
        public void OnDisabled();
        public void OnUpdate();
        public void OnFixedUpdate();
    }   
}
