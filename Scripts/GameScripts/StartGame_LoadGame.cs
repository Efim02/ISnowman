using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartGame_LoadGame : MonoBehaviour
{

    int idScene = 1;
    [SerializeField]
    Text T_Load;
    [SerializeField]
    Image I_Battery;
    bool oneWork = true;
    void Awake()
    {
        SaveLoad.Load();
    }
    void Start()
    {
        if(oneWork == true)
            if (SceneManager.GetActiveScene().buildIndex == 0)
                idScene = SaveLoad.save.idSceneLastOpen;
        /*else
            idScene = SceneManager.GetActiveScene()*/
    }
    private void Update()
    {
        if(oneWork == true)
            if(SceneManager.GetActiveScene().buildIndex == 0)
                StartCoroutine(LoadScene(idScene));
    }
    public IEnumerator LoadScene(int idScene)
    {
        oneWork = false;
        yield return new WaitForSeconds(0.1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(idScene);
        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            I_Battery.fillAmount = progress;
            T_Load.text = string.Format( "{0:0}%" , progress * 100);
            yield return null;
        }
    }
}
