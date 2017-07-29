public class PlayerStatistics : PropertyChangedBehaviour
{
    private int steps = 0;
    private int oxygen = 100;

    public int Steps { get { return steps; } set { steps = value; OnPropertyChanged("Steps"); } }

    public int Oxygen
    {
        get { return oxygen; }
        set
        {
            oxygen = value;
            if (oxygen < 0)
            {
                oxygen = 0;
            }
            else if (oxygen > 100)
            {
                oxygen = 100;
            }

            OnPropertyChanged("Oxygen");
        }
    }

    private void Awake()
    {
        PropertyChanged += This_PropertyChanged;
    }

    private void This_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Steps"))
        {
            DecrementOxygen();
        }
    }

    private void DecrementOxygen()
    {
        if (Steps % Constants.Energy.MovementCost == 0)
        {
            Oxygen--;
        }
    }
}
