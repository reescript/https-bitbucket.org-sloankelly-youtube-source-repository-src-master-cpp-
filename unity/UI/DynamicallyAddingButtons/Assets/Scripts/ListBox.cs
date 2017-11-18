using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListBox : MonoBehaviour
{
    private int selectedIndex = -1;

    public GameObject itemTemplate;

    public GameObject content;

    public Dropdown.DropdownEvent onValueChanged;

    public Color selectedColor = Color.blue;

    public Color selectedTextColor = Color.white;

    public Color normalColor = Color.white;

    public Color normalTextColor = Color.black;

    public List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

    public void RemoveSelected()
    {
        RemoveAt(selectedIndex);
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= options.Count) return;

        // Remove UI component
        Transform t = content.transform.GetChild(index);
        Destroy(t.gameObject);

        // Remove logical component
        options.RemoveAt(index);
    }

    public void AddOptions(List<Dropdown.OptionData> optionData)
    {
        foreach (var option in optionData)
        {
            // Construct our UI elements
            var copy = Instantiate(itemTemplate);
            copy.transform.SetParent(content.transform);
            copy.transform.localPosition = Vector3.zero;
            copy.transform.localScale = Vector3.one;

            copy.GetComponentInChildren<Text>().text = option.text;

            // Add the event handler
            copy.GetComponent<Button>().onClick.AddListener(
                () => { OnItemSelected(FindIndex(copy)); }
            );

            // Add the option to the list
            options.Add(option);
        }
    }

    public void ClearOptions()
    {
        // Remove the UI objects
        foreach (Transform t in content.transform)
        {
            Destroy(t.gameObject);
        }

        // Remove the underlying data
        options.Clear();
    }

    private void OnItemSelected(int index)
    {
        print(index);

        ClearItem(selectedIndex);
        selectedIndex = index;
        SetItem(index);

        onValueChanged.Invoke(index);
    }

    private void SetItem(int index)
    {
        if (index < 0) return;
        SetButtonColor(index, selectedColor, selectedTextColor);
    }

    private void ClearItem(int index)
    {
        if (index < 0) return;
        SetButtonColor(index, normalColor, normalTextColor);
    }

    private void SetButtonColor(int index, Color background, Color foreground)
    {
        var button = content.transform.GetChild(index).GetComponent<Button>();

        ColorBlock cb = new ColorBlock()
        {
            normalColor = background,
            colorMultiplier = button.colors.colorMultiplier,
            disabledColor = button.colors.disabledColor,
            fadeDuration = button.colors.fadeDuration,
            highlightedColor = background,
            pressedColor = background
        };

        button.colors = cb;
        button.GetComponentInChildren<Text>().color = foreground;
    }

    private int FindIndex(GameObject copy)
    {
        for (int i =0; i < content.transform.childCount;i++)
        {
            Transform t = content.transform.GetChild(i);
            if (t.gameObject == copy)
            {
                return i;
            }
        }

        return -1;
    }
}
