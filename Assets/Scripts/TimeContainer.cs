using System;

[Serializable]
public class TimeContainer
{
    public int Minutes;
    public int Seconds;

    public bool IsEmpty => Minutes == 0 && Seconds == 0;
}