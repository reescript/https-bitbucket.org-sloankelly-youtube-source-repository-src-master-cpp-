using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour, IDialog
{
    #region Private class to hold the data structures for the dialog box

    private class ButtonData
    {
        public string Text { get; set; }
        public Action Action { get; set; }
    }

    private class DialogData
    {
        public string Header { get; set; }
        public string BodyText { get; set; }
        public List<ButtonData> Buttons { get; private set; }
        public Sprite Sprite { get; set; }

        public DialogData()
        {
            Buttons = new List<ButtonData>();
        }
    }

    #endregion

    private DialogData dialogData = new DialogData();

    public Text headerText;
    public Text bodyText;
    public Image icon;
    public Transform buttonHolder;
    public GameObject dialogBox;
    public GameObject buttonPrefab;

    #region This is _kind of_ a singleton pattern

    private static IDialog _instance;

    public static IDialog Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogBox>();
            }

            return _instance;
        }
    }

    #endregion

    public IDialog SetImage(Sprite sprite)
    {
        dialogData.Sprite = sprite;
        return this;
    }

    public IDialog AddButton(string text, Action action)
    {
        dialogData.Buttons.Add(new ButtonData() { Text = text, Action = action });
        return this;
    }

    public IDialog AddCancelButton(string text)
    {
        dialogData.Buttons.Add(new ButtonData() { Text = text, Action = () => { } });
        return this;
    }

    public IDialog SetText(string text)
    {
        dialogData.BodyText = text;
        return this;
    }

    public IDialog SetTitle(string text)
    {
        dialogData.Header = text;
        return this;
    }

    public IDialog Show()
    {
        // Setting the text
        headerText.text = dialogData.Header;
        bodyText.text = dialogData.BodyText;

        // Remove the existing buttons from the dialog box
        foreach (Transform t in buttonHolder.transform)
        {
            Destroy(t.gameObject);
        }

        // Add the new buttons to the dialog box
        dialogData.Buttons.ForEach(CreateButton);

        // If we have a sprite, show it, otherwise clear the image
        icon.sprite = dialogData.Sprite;

        // Show the dialog box
        dialogBox.SetActive(true);

        // Set up the dialog system for a new dialog
        dialogData = new DialogData();

        // Keep the fluent design!
        return this;
    }

    private void CreateButton(ButtonData buttonData)
    {
        GameObject buttonCopy = Instantiate(buttonPrefab);
        buttonCopy.transform.SetParent(buttonHolder);
        buttonCopy.transform.localScale = Vector3.one;

        var txt = buttonCopy.GetComponentInChildren<Text>();
        var button = buttonCopy.GetComponent<Button>();

        Action fireAndClose = () =>
        {
            buttonData.Action();
            dialogBox.SetActive(false);
        };

        txt.text = buttonData.Text;
        button.onClick.AddListener(new UnityEngine.Events.UnityAction(fireAndClose));
    }
}
