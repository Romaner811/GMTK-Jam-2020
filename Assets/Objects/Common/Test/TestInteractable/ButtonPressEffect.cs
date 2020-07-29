using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressEffect : BaseEffect
{
    [SerializeReference] protected Animator _amin;

    public override void Affect(BasicAffectable other)
    {
        this._amin.SetTrigger("Press");
    }
}
