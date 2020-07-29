using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour, IEffect
{
    public abstract void Affect(BasicAffectable other);
}
