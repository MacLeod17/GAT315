using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnumData : ScriptableObject
{
	public abstract string id { get; }
	public abstract int index { get; set; }
	public abstract string[] names { get; }
}
