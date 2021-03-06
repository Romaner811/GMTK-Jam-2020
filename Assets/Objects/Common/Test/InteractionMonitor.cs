﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMonitor : MonoBehaviour
{
    [SerializeReference] public Transform Subject;

    private Player _player;
    private BasicAffectable _affectable;

    private void OnEnable()
    {
        if (this.Subject == null)
        {
            if (this.TryGetComponent(out Transform subject))
            {
                Debug.LogWarning($"{this.name}: monitor subject missing!");
                this.Subject = subject;
            }
            else
            {
                Debug.LogError($"{this.name}: can't find monitor subject!");
            }
        }

        this._player = this.Subject.GetComponent<Player>();
        this._affectable = this.Subject.GetComponent<BasicAffectable>();
    }

    private void OnGUI()
    {
        string msg = $"Interaction:\r\nAnyTrigger: {this._affectable.CanTriggersAny}";

        int lines = 2;

        int lineH = 15;
        int p = 15;

        int w = 200;
        int h = lines * lineH + p;

        int x = Screen.width - w - p;
        int y = p;

        GUI.Box(
            new Rect(x, y, w, h),
            msg
            );
    }
}
