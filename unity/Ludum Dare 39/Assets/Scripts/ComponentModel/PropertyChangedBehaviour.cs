using UnityEngine;
using System.ComponentModel;

public class PropertyChangedBehaviour : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string nameOfProperty)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(nameOfProperty));
        }
    }
    
}
