using NaughtyAttributes;
using UnityEngine;

namespace ShEcho.SO.Properties
{
	[CreateAssetMenu(fileName = "Vector3SO", menuName = "SO/Properties/Vector3", order = 1)]
	public class Vector3SO : BaseSO
	{
		[field: SerializeField, ReadOnly] public Vector3 Value { get; set; }
	}
}