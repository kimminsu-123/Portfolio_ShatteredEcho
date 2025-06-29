using NaughtyAttributes;
using UnityEngine;

namespace ShEcho.SO.Properties
{
	[CreateAssetMenu(fileName = "Vector2SO", menuName = "SO/Properties/Vector2", order = 0)]
	public class Vector2SO : BaseSO
	{
		[field: SerializeField, ReadOnly] public Vector2 Value { get; set; }
	}
}