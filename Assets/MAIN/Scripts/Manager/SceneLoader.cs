using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject UI;
    public GameObject Player;

    public static SceneLoader instance = null;

    [SerializeField]
    private CanvasGroup sceneLoaderCanvasGroup;

    [SerializeField]
    private Image progressBar;

    private string loadSceneName;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }



        DontDestroyOnLoad(UI);
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(this.gameObject);

        gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += LoadSceneEnd;
        SceneManager.sceneLoaded += SetPosition;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetPosition(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == loadSceneName)
        {
            //PlayerManager.instance.FindPlayer();

            if(loadSceneName == "Field")
            {
                //PlayerManager.instance.playerCombat.transform.localPosition = new Vector3(10f, 9.5f, -150f);
                Player.transform.localPosition = new Vector3(10f, 9.5f, -150f);
            }

            else if(loadSceneName == "Boss")
            {
                //PlayerManager.instance.playerCombat.transform.localPosition = new Vector3(2.5f, 0.1f, 19f);
                Player.transform.localPosition = new Vector3(2.5f, 0.1f, 19f);
            }

            else if(loadSceneName == "Gate")
            {
                //PlayerManager.instance.playerCombat.transform.localPosition = new Vector3(44f, 11f, 7f);
                //PlayerManager.instance.playerCombat.transform.rotation = Quaternion.Euler(0, 180f, 0);

                Player.transform.localPosition = new Vector3(44f, 11f, 7f);
                Player.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            
            SceneManager.sceneLoaded -= SetPosition;
            GameManager.instance.GameSet();
        }
    }
}
