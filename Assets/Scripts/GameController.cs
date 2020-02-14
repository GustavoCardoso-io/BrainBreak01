using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //UI items in game
    [SerializeField]
    private Text perguntaText = null;
    [SerializeField]
    private Material objetoPergunta = null;
    [SerializeField]
    private Text gameHighScore = null;
    [SerializeField]
    private Text textTime = null;
    [SerializeField]
    private Button verdeiro = null;
    [SerializeField]
    private Button falso = null;
    [SerializeField]
    private GameObject objectGame = null;
    [SerializeField]
    private Button musicButton = null;

    //UI GAME OVER PANEL
    [SerializeField]
    private GameObject GameOverPanel = null;
    [SerializeField]
    private Text GameOverTextBestScore = null;
    [SerializeField]
    private Text GameOverTextScoreActuaPlayer = null;

    //Pontos Player
    private int scorePlayer = 0;

    //Dados Pergunta
    private bool respostaPerguntaAtual = false;
    private string pergunta = "";
    private Color corObjeto = Color.black;
    private Color corTextoPergunta = Color.black;

    //Tempo Para as respostas...
    private int tempoParaResponde = 3;
    private bool tocandoEfeitodeSom = false;

    //Audio Games
    [SerializeField]
    private AudioSource audioRespostaCorreta = null;

    [SerializeField]
    private AudioSource audioRespostaErrada = null;

    // Start is called before the first frame update
    void Start()
    {
        SetUIInfo();
        StartCoroutine(DeclementarTempoParaResponde());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tempoParaResponde >= 0)
        {
            textTime.text = tempoParaResponde.ToString();
        }
    }
    void GetPerguntas()
    {
        pergunta = " ";
        respostaPerguntaAtual = false;
        corObjeto = Color.black;
        corTextoPergunta = Color.black;

        int id = Random.Range(0, DataController.instance.dataPerguntas.Length);

        pergunta = DataController.instance.GetPergunta(id);
        respostaPerguntaAtual = DataController.instance.GetResposta(id);
        corObjeto = DataController.instance.GetCorObjeto(id);
        corTextoPergunta = DataController.instance.GetCorTexto(id);
        textTime.text = tempoParaResponde.ToString();

        tocandoEfeitodeSom = false;
    }

    //Seta todas as informações do UI Em Game;
    void SetUIInfo()
    {
        GetPerguntas();
        gameHighScore.text = scorePlayer.ToString();
        perguntaText.text = pergunta;
        perguntaText.color = corTextoPergunta;
        objetoPergunta.SetColor("_EmissionColor", corObjeto);

        verdeiro.enabled = true;
        falso.enabled = true;
    }

    //Checa quando o botão de resposta falso for precionado
    public void ButtonFalsePress()
    {
        if (respostaPerguntaAtual == false)
        {
            scorePlayer += 10;
            tempoParaResponde = 3;

            if (tocandoEfeitodeSom == false)
            {
                InativeButton();
                audioRespostaCorreta.Play();
                tocandoEfeitodeSom = true;
            }
            SetUIInfo();
        }
        else
        {
            GameOver();
        }
    }

    //Declementa o tempo a cada 1 SEGUNDO EM JOGO
    IEnumerator DeclementarTempoParaResponde()
    {
        yield return new WaitForSeconds(1f);

        if (tempoParaResponde > 0)
        {
            StartCoroutine(DeclementarTempoParaResponde());
        }
        else
        {
            GameOver();
        }
        tempoParaResponde--;
    }

    //Checa quando o botão de resposta verdadeiro for precionado
    public void ButtonVerdadeiroPress()
    {
        if (respostaPerguntaAtual == true)
        {
            scorePlayer += 10;
            tempoParaResponde = 3;

            if (tocandoEfeitodeSom == false)
            {
                InativeButton();
                audioRespostaCorreta.Play();
                tocandoEfeitodeSom = true;
            }
            SetUIInfo();
        }
        else
        {
            GameOver();
        }
    }
    //Executa a função de perda de jogo.
    private void GameOver()
    {
        UIGameOver();
        ControllerADSInGame();

        if (scorePlayer > 40)
        {
            GooglePlayServices.instance.UnlockIncrementalAchievement(GPGSIds.achievement_love_the_brain_break, 1);
        }
        
        GameOverTextBestScore.text = DataController.instance.GetHighScorePlayer().ToString();
        GameOverTextScoreActuaPlayer.text = scorePlayer.ToString();

        UnlcokArchimentesScore(scorePlayer);
        GooglePlayServices.instance.AddScoreLeaderBoard(GPGSIds.leaderboard_highscores, scorePlayer);
        GooglePlayServices.instance.UnlocokRegularAchievement(GPGSIds.achievement_i_lost);

        if (DataController.instance.GetHighScorePlayer() < scorePlayer)
        {
            DataController.instance.SetHighScorePlayer(scorePlayer);
        }

        StopCoroutine(DeclementarTempoParaResponde());

        if (tocandoEfeitodeSom == false)
        {
            InativeButton();
            audioRespostaErrada.Play();
            tocandoEfeitodeSom = true;
        }
    }
    public void InativeButton()
    {
        verdeiro.enabled = false;
        falso.enabled = false;
    }
    private void UIGameOver()
    {
        perguntaText.gameObject.SetActive(false);
        gameHighScore.gameObject.SetActive(false);
        musicButton.gameObject.SetActive(false);
        textTime.gameObject.SetActive(false);
        verdeiro.gameObject.SetActive(false);
        falso.gameObject.SetActive(false);
        GameOverPanel.gameObject.SetActive(true);
        objectGame.gameObject.SetActive(false);
    }
    public void ResumeGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void HomeGame()
    {
        SceneManager.LoadScene("MenuGame");
    }
    public void ControllerADSInGame()
    {
        int playerHaveDied = PlayerPrefs.GetInt("TimeToPlayerDie") + 1;
        PlayerPrefs.SetInt("TimeToPlayerDie", playerHaveDied);
    }
    public void UnlcokArchimentesScore(int scorePlayer)
    {
        if (scorePlayer >= 100)
        {
            GooglePlayServices.instance.UnlocokRegularAchievement(GPGSIds.achievement_little_genius);
        }

        if (scorePlayer >= 1000)
        {
            GooglePlayServices.instance.UnlocokRegularAchievement(GPGSIds.achievement_great_genius);
        }

        if (scorePlayer >= 2000)
        {
            GooglePlayServices.instance.UnlocokRegularAchievement(GPGSIds.achievement_2k_d);
        }

        if (scorePlayer >= 5000)
        {
            GooglePlayServices.instance.UnlocokRegularAchievement(GPGSIds.achievement_grand_master_brain_break);
        }

    }
}
