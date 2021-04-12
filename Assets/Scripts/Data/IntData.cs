using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Int", menuName = "Data/Int")]
public class IntData : ScriptableObject
{
	public int value;

	public static implicit operator int(IntData data) { return data.value; }
}
