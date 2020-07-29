using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSettingsMonitor : MonoBehaviour
{
    [SerializeReference] public Transform Subject;

    private Player _player;
    private InputProcessor _inputer;

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
        this._inputer = this.Subject.GetComponent<InputProcessor>();
    }

    public static string GetTextActionSettings(IDictionary<int, KeyCode> settings)
    {
        string items = settings.ToString();
        return $"{{" +
            $"    {items}" +
            $"}}";
    }

    private void OnGUI()
    {
        string textSettings = ControlSettingsMonitor.GetTextActionSettings(this._inputer.ActionKeyCodes);
        string msg = $"Interaction:\r\nAnyTrigger: {textSettings}";

        int lines = 1 + this._inputer.ActionKeyCodes.Count;

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
