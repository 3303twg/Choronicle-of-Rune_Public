using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using _02_Scripts.UI;

public class Title : MonoBehaviour
{
    [Space(5)]
    [Header("오브젝트 할당")]
    public GameObject saveList;

    private void Start()
    {
        UIManager.Instance.gameObject.SetActive(false);
        PlayerController.Instance.gameObject.SetActive(false);
    }
    void OnExitGame()
    {
#if UNITY_EDITOR
        // 에디터 모드에서 플레이 중지
        EditorApplication.isPlaying = false;
#else
        // 빌드된 게임 종료
        Application.Quit();
#endif
    }
}
