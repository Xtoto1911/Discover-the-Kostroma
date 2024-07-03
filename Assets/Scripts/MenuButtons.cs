using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject MainScreen;
    public GameObject AchievementsScreen;
    public GameObject SettingsScreen;
    public GameObject ShopScreen;
    public GameObject AreaSelectingScreen;
    public GameObject VerticalScroll;
    public GameObject TestBtn;
    public GameObject BuyingPanel;

    public Button MenuBtn;
    public Button AreasBtn;
    public Button AchievBtn;
    public Button ShopBtn;
    public Button SettingBtn;
    public Button MainBtn;
    public Button CentralBtn;
    public Button FabrichBtn;
    public Button ZavoljskBtn;
    public Button ExitBtn;
    public Button BuyBtn;

    private Vector2 menuBtnOriginalPosition;
    private Vector2[] buttonOffsetsVertical = new Vector2[]
    {
        new Vector2(0, 800),  // Main
        new Vector2(0, 640), // Areas
        new Vector2(0, 480), // Acviev
        new Vector2(0, 320),  // Shop
        new Vector2(0, 160)  // Settings
    };

    private Vector2[] buttonOffsetsHorizontal = new Vector2[]
    {
        new Vector2(800, 0),  // Main
        new Vector2(640, 0), // Areas
        new Vector2(480, 0), // Acviev
        new Vector2(320, 0),  // Shop
        new Vector2(160, 0)  // Settings
    };

    private RectTransform[] buttonTransforms;
    private bool isMenuExpanded = false;
    private float animationDuration = 0.2f;

    void Start()
    {
        buttonTransforms = new RectTransform[]
        {
            MainBtn.GetComponent<RectTransform>(),
            AreasBtn.GetComponent<RectTransform>(),
            AchievBtn.GetComponent<RectTransform>(),
            ShopBtn.GetComponent<RectTransform>(),
            SettingBtn.GetComponent<RectTransform>()
        };

        menuBtnOriginalPosition = MenuBtn.GetComponent<RectTransform>().anchoredPosition;

        MenuBtn.onClick.AddListener(() => ToggleMenu());
        MainBtn.onClick.AddListener(() => OnMenuButtonClick(MainScreen));
        AreasBtn.onClick.AddListener(() => OnMenuButtonClick(AreaSelectingScreen));
        AchievBtn.onClick.AddListener(() => OnMenuButtonClick(AchievementsScreen));
        ShopBtn.onClick.AddListener(() => OnMenuButtonClick(ShopScreen));
        SettingBtn.onClick.AddListener(() => OnMenuButtonClick(SettingsScreen));

        CentralBtn.onClick.AddListener(() => OnCentralButtonClick());
        FabrichBtn.onClick.AddListener(() => OnCityButtonClick());
        ZavoljskBtn.onClick.AddListener(() => OnCityButtonClick());
        ExitBtn.onClick.AddListener(() => OnExitButtonClick());
        BuyBtn.onClick.AddListener(() => OnBuyButtonClick());

        TestBtn.GetComponent<Button>().onClick.AddListener(() => OnTestButtonClick());

        BuyingPanel.SetActive(false); // Ensure BuyingPanel is hidden at the start
    }

    void ToggleMenu()
    {
        isMenuExpanded = !isMenuExpanded;

        Vector2[] buttonOffsets = Screen.orientation == ScreenOrientation.Portrait ? buttonOffsetsVertical : buttonOffsetsHorizontal;

        for (int i = 0; i < buttonTransforms.Length; i++)
        {
            if (buttonTransforms[i] == null)
            {
                Debug.LogWarning("Button transform is null at index: " + i);
                continue;
            }

            if (i < buttonOffsets.Length) // Check if index is within bounds
            {
                if (isMenuExpanded)
                {
                    StartCoroutine(MoveButton(buttonTransforms[i], menuBtnOriginalPosition + buttonOffsets[i]));
                    buttonTransforms[i].gameObject.SetActive(true); // Ensure buttons are visible when menu expands
                }
                else
                {
                    int index = i; // Capturing index for the closure
                    StartCoroutine(MoveButton(buttonTransforms[i], menuBtnOriginalPosition, () => buttonTransforms[index].gameObject.SetActive(false)));
                }
            }
            else
            {
                Debug.LogWarning("Index out of range: buttonOffsets array is smaller than buttonTransforms array.");
            }
        }
    }

    IEnumerator MoveButton(RectTransform buttonTransform, Vector2 targetPosition, System.Action onComplete = null)
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
        onComplete?.Invoke();
    }

    void OnMenuButtonClick(GameObject screenToShow)
    {
        ShowScreen(screenToShow);
        // Do not hide buttons here, they should remain visible
    }

    void OnCityButtonClick()
    {
        VerticalScroll.SetActive(false);
        TestBtn.SetActive(false);
        BuyingPanel.SetActive(true);
    }

    void OnExitButtonClick()
    {
        BuyingPanel.SetActive(false);
        VerticalScroll.SetActive(true);
        TestBtn.SetActive(true);
    }

    void OnBuyButtonClick()
    {
        // Placeholder for Buy button functionality
        Debug.Log("Buy button clicked");
    }

    void OnTestButtonClick()
    {
        SceneManager.LoadScene("test");
    }

    void OnCentralButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
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
        for (int i = 0; i < buttonTransforms.Length; i++)
        {
            if (buttonTransforms[i] != null)
            {
                buttonTransforms[i].gameObject.SetActive(false);
            }
        }
    }

    void ResetMenuButtons()
    {
        isMenuExpanded = false; // Reset the menu expanded state
        for (int i = 0; i < buttonTransforms.Length; i++)
        {
            if (buttonTransforms[i] != null)
            {
                buttonTransforms[i].anchoredPosition = menuBtnOriginalPosition; // Reset button positions
                buttonTransforms[i].gameObject.SetActive(false); // Ensure buttons are hidden
            }
        }
    }
}
