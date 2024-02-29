using System;

[Serializable]
public class GameData
{
    public bool isVibrationEnabled;

    public CurrencyData currency;
    public AudioData audio;

    public GameData()
    {
        currency = new CurrencyData();
        audio = new AudioData();
    }
}
