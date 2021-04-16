using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectEnum", menuName = "Data/Enum/Object")]
public class ObjectEnumData : EnumData
{
	public enum eValue
	{
		Circle,
		Box
	}
	public eValue value;

	public override int index { get => (int)value; set => this.value = (eValue)value; }
	public override string[] names => Enum.GetNames(typeof(eValue));
}