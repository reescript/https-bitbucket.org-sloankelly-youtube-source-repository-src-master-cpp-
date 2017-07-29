using UnityEngine.UI;

public class ImageFillBinding : BindingBaseBehaviour
{
    Image image;
    IValueConverter converter = new NormalizedIntegerConverter();

    protected override void OnAwake()
    {
        image = GetComponent<Image>();
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        image.fillAmount = GetBoundValue<float>(converter);
    }
}
