using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText = null;
    public int highScorePontos = 0;
    [SerializeField]
    private AudioSource tapPlay = null;

    // Start is called before the first frame update
    void Start()
    {
        highScorePontos = DataController.instance.GetHighScorePlayer();
        highScoreText.text = highScorePontos.ToString();

        if (highScorePontos < DataController.instance.GetHighScorePlayer())
        {
            highScorePontos = DataController.instance.GetHighScorePlayer();
            highScoreText.text = highScorePontos.ToString();
        }
    }
    public void TapOnScreen()
    {
        //GPGS Arquivements unlock
        GooglePlayServices.instance.UnlocokRegularAchievement(GPGSIds.achievement_my_first_game);

        tapPlay.Play();
        SceneManager.LoadScene("Game");
    }
    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void ShowAchievementUI()
    {
        GooglePlayServices.instance.ShowAchievement();
    }
    public void ShowLeadboard()
    {
        GooglePlayServices.instance.ShowLeaderboard();
    }
}
