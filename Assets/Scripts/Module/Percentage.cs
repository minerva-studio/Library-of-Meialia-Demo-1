using System;

namespace Minerva.Module
{
    [Serializable]
    public struct Percentage
    {
        public float value;

        public Percentage(float value)
        {
            this.value = value * 100;
        }

        public static implicit operator float(Percentage percentage)
        {
            return percentage.value / 100;
        }

        public static implicit operator Percentage(float percentage)
        {
            return new Percentage(percentage);
        }

        public static implicit operator double(Percentage percentage)
        {
            return percentage.value / 100;
        }

        public static implicit operator Percentage(double percentage)
        {
            return new Percentage((float)percentage);
        }

        public static float operator *(int i, Percentage percentage)
        {
            return i * (float)percentage;
        }

        public static float operator *(Percentage percentage, int i)
        {
            return i * (float)percentage;
        }
    }
}