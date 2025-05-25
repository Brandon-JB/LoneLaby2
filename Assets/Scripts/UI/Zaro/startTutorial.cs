using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class startTutorial : MonoBehaviour
{
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private Transform[] positions;
    [SerializeField] private Transform leora;
    [SerializeField] private GameObject yesbtn;
    public bool firstEntry = false;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (mainDialogueManager.GLOBALcurrentlyRunningText == "introducingSuspects" && SceneManager.GetActiveScene().name == "NoCombatAreas")
        {
            firstEntry = true;
            bg.alpha = 0f;
            positions[0].DOMove(positions[1].transform.position, 2f).SetUpdate(true);
            return;
        }
        bg.alpha = 0f;
        bg.DOFade(1, 1).SetUpdate(true);
        EventSystem.current.SetSelectedGameObject(null);
        positions[0].DOMove(positions[1].transform.position, 1f).SetUpdate(true).OnComplete(() =>
        {
            EventSystem.current.SetSelectedGameObject(yesbtn);
        });
    }

    public void yes()
    {
        //transition out to tutorial
        firstEntry = false;
        EventSystem.current.SetSelectedGameObject(null);
        SpawnManager.SpawnNumber = 1;
        GameObject.FindObjectOfType<CityPortalManager>().LoadCityArea("TrainingEntry");
        bg.DOFade(0, 1).SetUpdate(true);
        positions[0].DOMove(positions[2].transform.position, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }

    public void no()
    {
        EventSystem.current.SetSelectedGameObject(null);
        bg.DOFade(0, 1).SetUpdate(true);
        //Teleport Leora; looks jarring rn but it's fine
        if (!firstEntry)
        {
            leora.position = positions[3].transform.position;
        }
        firstEntry = false;
        positions[0].DOMove(positions[2].transform.position, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
            OpenPauseMenu.GLOBALcanOpenPause = true;
            OpenPauseMenu.canOpenPause = true;
            OpenPauseMenu.pauseOpened = false;
            NonCombatPlayerMovement.canMove = true;
            this.gameObject.SetActive(false);
        });
    }
}
