using UnityEngine;
public class DataController : MonoBehaviour
{

    public static DataController instance;
    public Perguntas[] dataPerguntas;

    private void Awake()
    {
        MakeInstance();
    }
    void MakeInstance()
    {

        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    public Color GetCorObjeto(int id)
    {
        switch (dataPerguntas[id].corobjeto)
        {
            case "green":
                 return Color.green;

            case "blue":
                return Color.blue;

            case "red":
                return Color.red;

            case "yellow":
                return Color.yellow;

            default:
                return Color.black;
        }
    }

    public Color GetCorTexto(int id)
    {
        switch (dataPerguntas[id].cortexto)
        {
            case "green":
                return Color.green;

            case "blue":
                return Color.blue;

            case "red":
                return Color.red;

            case "yellow":
                return Color.yellow;

            default:
                return Color.black;
        }
    }

    public string GetPergunta(int id)
    {
        return dataPerguntas[id].pergunta;
    }

    public bool GetResposta(int id)
    {
        return dataPerguntas[id].resposta;
    }

    public int GetHighScorePlayer()
    {
            return PlayerPrefs.GetInt("HighScore");     
    }
    public void SetHighScorePlayer(int score)
    {
        PlayerPrefs.SetInt("HighScore", score );
    }

}

[System.Serializable]
public class Perguntas
{
    public string pergunta;
    public bool resposta;
    public string corobjeto;
    public string cortexto;
}


