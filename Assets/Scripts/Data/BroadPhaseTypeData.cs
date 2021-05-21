using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BroadPhaseEnum", menuName = "Data/Enum/BroadPhase")]
public class BroadPhaseTypeData : EnumData
{
    public enum eType
    {
        None,
        QuadTree,
        BVH
    }

    public eType value;


    public override int index { get => (int)value; set => this.value = (eType)value; }
    public override string[] names => Enum.GetNames(typeof(eType));
}
