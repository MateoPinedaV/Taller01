using System;

public class Time
{
    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    public int Hour => _hour;
    public int Minute => _minute;
    public int Second => _second;
    public int Millisecond => _millisecond;

    public Time() : this(0, 0, 0, 0) { }
    public Time(int hour) : this(hour, 0, 0, 0) { }
    public Time(int hour, int minute) : this(hour, minute, 0, 0) { }
    public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }
    public Time(int hour, int minute, int second, int millisecond)
    {
        if (!IsValidHour(hour) || !IsValidMinute(minute) || !IsValidSecond(second) || !IsValidMillisecond(millisecond))
        {
            throw new ArgumentOutOfRangeException("Valores de tiempo fuera de rango.");
        }
        _hour = hour;
        _minute = minute;
        _second = second;
        _millisecond = millisecond;
    }

    public override string ToString()
    {
        int displayHour = _hour % 12;
        if (displayHour == 0) displayHour = 12;
        string period = _hour < 12 ? "AM" : "PM";
        return $"{displayHour:D2}:{_minute:D2}:{_second:D2}.{_millisecond:D3} {period}";
    }

    public int ToMilliseconds() => (_hour * 3600 + _minute * 60 + _second) * 1000 + _millisecond;
    public int ToSeconds() => _hour * 3600 + _minute * 60 + _second;
    public int ToMinutes() => _hour * 60 + _minute;

    public bool IsOtherDay(Time other)
    {
        return (this.ToMilliseconds() + other.ToMilliseconds()) >= 86400000;
    }

    public Time Add(Time other)
    {
        int totalMilliseconds = this.ToMilliseconds() + other.ToMilliseconds();

        int newMillisecond = totalMilliseconds % 1000;
        int totalSeconds = totalMilliseconds / 1000;
        int newSecond = totalSeconds % 60;
        int totalMinutes = totalSeconds / 60;
        int newMinute = totalMinutes % 60;
        int totalHours = totalMinutes / 60;
        int newHour = totalHours % 24;

        return new Time(newHour, newMinute, newSecond, newMillisecond);
    }

    private static bool IsValidHour(int hour) => hour >= 0 && hour <= 23;
    private static bool IsValidMinute(int minute) => minute >= 0 && minute <= 59;
    private static bool IsValidSecond(int second) => second >= 0 && second <= 59;
    private static bool IsValidMillisecond(int millisecond) => millisecond >= 0 && millisecond <= 999;
}

