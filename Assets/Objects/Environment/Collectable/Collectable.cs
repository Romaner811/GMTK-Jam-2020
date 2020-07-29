using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]
public class Collectable : Interactable
{
    public override void Affect(BasicAffectable other)
    {
        this.gameObject.SetActive(false);
        base.Affect(other);
    }
}
