using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject MainScreen;
    public GameObject AchievementsScreen;
    public GameObject SettingsScreen;
    public GameObject ShopScreen;
    public GameObject AreaSelectingScreen;

    public Button MenuBtn;
    public Button AreasBtn;
    public Button AchievBtn;
    public Button ShopBtn;
    public Button SettingBtn;
    public Button BackBtn;

    private Vector2 menuBtnOriginalPosition;
    private Vector2[] buttonOffsets = new Vector2[]
    {
        new Vector2(-30, 400),  // Offset for AreasBtn
        new Vector2(150, 300), // Offset for AchievBtn
        new Vector2(300, 150), // Offset for ShopBtn
        new Vector2(400, -30)  // Offset for SettingBtn
    };

    private RectTransform[] buttonTransforms;
    private bool isMenuExpanded = false;
    private float animationDuration = 0.2f; // Duration of the animation

    void Start()
    {
        buttonTransforms = new RectTransform[]
        {
            AreasBtn.GetComponent<RectTransform>(),
            AchievBtn.GetComponent<RectTransform>(),
            ShopBtn.GetComponent<RectTransform>(),
            SettingBtn.GetComponent<RectTransform>()
        };

        menuBtnOriginalPosition = MenuBtn.GetComponent<RectTransform>().anchoredPosition;

        MenuBtn.onClick.AddListener(() => ToggleMenu());
        AreasBtn.onClick.AddListener(() => OnMenuButtonClick(AreaSelectingScreen));
        AchievBtn.onClick.AddListener(() => OnMenuButtonClick(AchievementsScreen));
        ShopBtn.onClick.AddListener(() => OnMenuButtonClick(ShopScreen));
        SettingBtn.onClick.AddListener(() => OnMenuButtonClick(SettingsScreen));
        BackBtn.onClick.AddListener(() => OnBackButtonClick());
    }

    void ToggleMenu()
    {
        isMenuExpanded = !isMenuExpanded;

        for (int i = 0; i < buttonTransforms.Length; i++)
        {
            if (isMenuExpanded)
            {
                StartCoroutine(MoveButton(buttonTransforms[i], menuBtnOriginalPosition + buttonOffsets[i]));
            }
            else
            {
                StartCoroutine(MoveButton(buttonTransforms[i], menuBtnOriginalPosition));
            }
            buttonTransforms[i].gameObject.SetActive(true);
        }
    }

    IEnumerator MoveButton(RectTransform buttonTransform, Vector2 targetPosition)
    {
        Vector2 startPosition = buttonTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            buttonTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buttonTransform.anchoredPosition = targetPosition;

        if (!isMenuExpanded)
        {
            buttonTransform.gameObject.SetActive(false);
        }
    }

    void OnMenuButtonClick(GameObject screenToShow)
    {
        ShowScreen(screenToShow);
        HideAllButtons();
        BackBtn.gameObject.SetActive(true);
    }

    void OnBackButtonClick()
    {
        ShowScreen(MainScreen);
        MenuBtn.gameObject.SetActive(true);
        BackBtn.gameObject.SetActive(false);
        ResetMenuButtons();
    }

    void ShowScreen(GameObject screenToShow)
    {
        MainScreen.SetActive(screenToShow == MainScreen);
        AchievementsScreen.SetActive(screenToShow == AchievementsScreen);
        SettingsScreen.SetActive(screenToShow == SettingsScreen);
        ShopScreen.SetActive(screenToShow == ShopScreen);
        AreaSelectingScreen.SetActive(screenToShow == AreaSelectingScreen);
    }

    void HideAllButtons()
    {
        MenuBtn.gameObject.SetActive(false);
        for (int i = 0; i < buttonTransforms.Length; i++)
        {
            buttonTransforms[i].gameObject.SetActive(false);
        }
    }

    void ResetMenuButtons()
    {
        isMenuExpanded = false; // Reset the menu expanded state
        for (int i = 0; i < buttonTransforms.Length; i++)
        {
            buttonTransforms[i].anchoredPosition = menuBtnOriginalPosition; // Reset button positions
            buttonTransforms[i].gameObject.SetActive(false); // Ensure buttons are hidden
        }
    }
}
