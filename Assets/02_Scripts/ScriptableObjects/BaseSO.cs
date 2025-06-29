using UnityEngine;

namespace ShEcho.SO
{
	public class BaseSO : ScriptableObject
	{
		[field: SerializeField, TextArea] protected string Description { get; set; }	
	}
}