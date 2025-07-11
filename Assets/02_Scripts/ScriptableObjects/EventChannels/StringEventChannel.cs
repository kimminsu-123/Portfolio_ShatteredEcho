using UnityEngine;

namespace ShEcho.SO.EventChannels
{
    [CreateAssetMenu(fileName = "StringEventChannel", menuName = "SO/EventChannel/String", order = 4)]
    public class StringEventChannel : GenericEventChannel<string>
    {
        
    }
}