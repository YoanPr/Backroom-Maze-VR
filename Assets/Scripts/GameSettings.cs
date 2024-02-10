// Singleton that store the settings of the game
public class GameSettings
{
    public float SoundVolume { get; set; } = 0.5f;

    public float MouseSensitivity { get; set; } = 1f;

    private static GameSettings instance = null;
    // lock for concurent access to the Instance method
    private static readonly object concurentLock = new object();

    private GameSettings()
    {
    }

    // Get singleton instance
    public static GameSettings Instance
    {
        get
        {
            lock (concurentLock)
            {
                if (instance == null)
                {
                    instance = new GameSettings();
                }
                return instance;
            }
        }
    }
}
