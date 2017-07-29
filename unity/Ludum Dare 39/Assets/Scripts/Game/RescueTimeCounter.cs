using UnityEngine;
using System.Collections;

public class RescueTimeCounter : PropertyChangedBehaviour
{
    bool running = true;
    int hours = 23;
    int minutes = 59;

    public bool Running { get { return running; } }

    public int Hours { get { return hours; } }

    public int Minutes { get { return minutes; } }
    
    IEnumerator Start()
    {
        while (running)
        {
            yield return new WaitForSeconds(1);

            minutes--;
            if (minutes == 0)
            {
                hours--;
                if (hours == -1)
                {
                    running = false;
                }
                else
                {
                    minutes = 59;
                }
            }

            OnPropertyChanged("Hours");
            OnPropertyChanged("Minutes");
        }
    }
}
