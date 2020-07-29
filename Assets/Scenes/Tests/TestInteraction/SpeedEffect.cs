using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : BaseEffect
{
    [SerializeField] public float SpeedMultiplier = 2f;

    public override void Affect(BasicAffectable other)
    {
        other.GetComponent<Mover>().Speed *= this.SpeedMultiplier;
    }
}
