using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayServices : MonoBehaviour
{
    public static GooglePlayServices instance;
    public static PlayGamesPlatform platform;
    private string authCode;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {

            if (success)
            {
              Debug.Log("LOGIN: SUCCESS");
            }
            else
            {
                Debug.Log("LOGIN: FALIDO");
            }
        });
    }

    #region achievement
    public void UnlocokRegularAchievement(string idAchievement) => Social.ReportProgress(idAchievement, 100, (success => { }));
    public void UnlockIncrementalAchievement(string idAchievement, int stepsIncrement) => PlayGamesPlatform.Instance.IncrementAchievement(idAchievement, stepsIncrement, (success => { }));
    #endregion /achievement
    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }
    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public void AddScoreLeaderBoard(string idLeaderBoard ,long score)
    {
        Social.ReportScore(score, idLeaderBoard, (success =>{}));
    }


}
