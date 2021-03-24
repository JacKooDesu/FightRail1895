using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacDev.TimeUtil
{
    [System.Serializable]
    public class Time
    {
        public int hour = 0;
        public int min = 0;
        public int sec = 0;

        public void SetTime(Time t)
        {
            this.hour = t.hour;
            this.min = t.min;
            this.sec = t.sec;
        }

        public Time FloatToTime(float t)
        {
            int h, m, s;
            s = System.Convert.ToInt32(t % 60);
            m = System.Convert.ToInt32(t / 60f);
            h = m / 60;

            Time time = new Time();
            time.hour = h;
            time.min = m;
            time.sec = s;

            return time;
        }

        public void AddTime(Time t)
        {
            this.hour += t.hour;
            this.min += t.min;
            this.sec += t.sec;

            if (this.sec >= 60)
            {
                this.min++;
                this.sec -= 60;
            }

            if (this.min >= 60)
            {
                this.hour++;
                this.min -= 60;
            }

        }
    }
}

