using MoreMountains.NiceVibrations;

public class VibrationManager : SingletonMonoBehaviour<VibrationManager>
{
    private GameData gameData;

    public void Initialize()
    {
        gameData = DataManager.Instance.gameData;
        ToggleVibration();
    }

    public void ToggleVibration()
    {
        MMVibrationManager.SetHapticsActive(gameData.isVibrationEnabled);
    }

    public void Vibrate(HapticTypes type = HapticTypes.MediumImpact)
    {
        MMVibrationManager.Haptic(type);
    }
}
