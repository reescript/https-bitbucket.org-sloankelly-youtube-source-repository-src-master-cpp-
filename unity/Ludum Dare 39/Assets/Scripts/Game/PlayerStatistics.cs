public class PlayerStatistics : PropertyChangedBehaviour
{
    private int steps = 0;
    private int oxygen = 100;
    private int batteries = 0;
    private int radio = 0;
    private bool isDead = false;

    public bool IsDead { get { return isDead; } set { isDead = value; OnPropertyChanged("IsDead"); } }

    public int Batteries { get { return batteries; } set { batteries = value; OnPropertyChanged("Batteries"); } }

    public int Steps { get { return steps; } set { steps = value; OnPropertyChanged("Steps"); } }

    public int Radio
    {
        get { return radio; }
        set
        {
            radio = value;
            if (radio > 100)
            {
                radio = 100;
            }
            else if (radio < 0)
            {
                radio = 0;
            }
            OnPropertyChanged("Radio");
        }
    }

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
