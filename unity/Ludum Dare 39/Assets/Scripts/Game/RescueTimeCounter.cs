using UnityEngine;
using System.Collections;

public class RescueTimeCounter : PropertyChangedBehaviour
{
    bool running = true;
    bool paused = false;
    int hours = Constants.Time.Hours;
    int minutes = Constants.Time.Minutes;

    public bool Running { get { return running; } }

    public int Hours { get { return hours; } }

    public int Minutes { get { return minutes; } }
    
    IEnumerator Start()
    {
        while (running)
        {
            if (paused)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(Constants.Time.Ticker);

                minutes--;
                if (minutes == -1)
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

    internal void Pause(bool isPaused)
    {
        paused = isPaused;
    }
}
