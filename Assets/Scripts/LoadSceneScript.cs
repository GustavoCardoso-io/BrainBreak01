using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim = null;
    public string SceneLoading = "";
    void Start()
    {
        if (SceneLoading == "MainMenu")
        {   
            anim = GetComponent<Animator>();
            anim.SetBool("IsLoading", true);
        }
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneLoading);
    }

}
