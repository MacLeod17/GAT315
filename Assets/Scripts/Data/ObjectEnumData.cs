using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectEnum", menuName = "Data/Enum/Object")]
public class ObjectEnumData : EnumData
{
	public enum eType
	{
		Circle,
		Box
	}
	public eType type;

	public override string id => type.ToString();
	public override int index { get => (int)type; set => type = (eType)value; }
	public override string[] names => Enum.GetNames(typeof(eType));
}
