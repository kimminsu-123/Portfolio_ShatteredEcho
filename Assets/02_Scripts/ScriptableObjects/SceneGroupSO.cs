using System.Linq;
using ShEcho.Utils;
using UnityEngine;

namespace ShEcho.SO
{
    [CreateAssetMenu(fileName = "SceneGroupSO", menuName = "SO/SceneGroupSO")]
    public class SceneGroupSO : BaseSO
    {
        public SceneProfile[] profiles;

        public float Progress => profiles.Sum(x => x.Progress) / profiles.Length;
        public bool IsAllDone => profiles.All(x => x.IsDone);
        public int SceneCount => profiles.Length;
    }
}