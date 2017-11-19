using System;
using UnityEngine;

public interface IDialog
{
    IDialog SetText(string text);

    IDialog SetTitle(string text);

    IDialog AddButton(string text, Action action);

    IDialog AddCancelButton(string text);

    IDialog SetImage(Sprite sprite);

    IDialog Show();
}
