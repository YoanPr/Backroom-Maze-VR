using UnityEngine;
using UnityEngine.SceneManagement;

// Switch between level and reset the game state
public class LevelManager: MonoBehaviour
{

    [SerializeField] Animator endGameAnimator;

    private bool hasEndGameAnimationStarted;

    private void OnEnable()
    {
        EventManager.StartGameEvent += ChangeScene;
        Player.EndGameEvent += StartEndAnimation;
    }

    private void OnDisable()
    {
        EventManager.StartGameEvent -= ChangeScene;
        Player.EndGameEvent -= StartEndAnimation;
    }

    private void StartEndAnimation()
    {
        endGameAnimator.SetTrigger("GameFinished");
        Invoke(nameof(ChangeScene), 5.0f);
    }


    public void ChangeScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
