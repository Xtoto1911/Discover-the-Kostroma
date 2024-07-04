using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VisibilityControl : MonoBehaviour
{
    // UI Elements
    public GameObject MainScreen;
    public GameObject AchievementsScreen;
    public GameObject SettingsScreen;
    public GameObject ShopScreen;
    public GameObject AreaSelectingScreen;
    public GameObject VerticalScroll;
    public GameObject BuyingPanel;

    public Button TestBtn;
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
    public Button FirstAchievBtn;

    private Vector2 menuBtnOriginalPosition;
    private Vector2[] buttonOffsetsVertical = new Vector2[]
    {
        new Vector2(0, 800),  // Main
        new Vector2(0, 640), // Areas
        new Vector2(0, 480), // Achiev
        new Vector2(0, 320),  // Shop
        new Vector2(0, 160)  // Settings
    };

    private Vector2[] buttonOffsetsHorizontal = new Vector2[]
    {
        new Vector2(800, 0),  // Main
        new Vector2(640, 0), // Areas
        new Vector2(480, 0), // Achiev
        new Vector2(320, 0),  // Shop
        new Vector2(160, 0)  // Settings
    };

    private RectTransform[] buttonTransforms;
    private bool isMenuExpanded = false;
    private float animationDuration = 0.2f;

    void Start()
    {
        // Initialize button transforms
        buttonTransforms = new RectTransform[]
        {
            MainBtn.GetComponent<RectTransform>(),
            AreasBtn.GetComponent<RectTransform>(),
            AchievBtn.GetComponent<RectTransform>(),
            ShopBtn.GetComponent<RectTransform>(),
            SettingBtn.GetComponent<RectTransform>()
        };

        // Store original position of the MenuBtn
        menuBtnOriginalPosition = MenuBtn.GetComponent<RectTransform>().anchoredPosition;

        // Set up button listeners
        MenuBtn.onClick.AddListener(() => ToggleMenu());
        MainBtn.onClick.AddListener(() => OnMenuButtonClick(MainScreen));
        AreasBtn.onClick.AddListener(() => OnMenuButtonClick(AreaSelectingScreen));
        AchievBtn.onClick.AddListener(() => OnMenuButtonClick(AchievementsScreen));
        ShopBtn.onClick.AddListener(() => OnMenuButtonClick(ShopScreen));
        SettingBtn.onClick.AddListener(() => OnMenuButtonClick(SettingsScreen));
        FirstAchievBtn.onClick.AddListener(() => OnFirstAchievButtonClick());

        CentralBtn.onClick.AddListener(() => OnCentralButtonClick());
        FabrichBtn.onClick.AddListener(() => OnCityButtonClick());
        ZavoljskBtn.onClick.AddListener(() => OnCityButtonClick());
        TestBtn.onClick.AddListener(() => OnTestButtonClick());

        ExitBtn.onClick.AddListener(() => OnExitButtonClick());
        BuyBtn.onClick.AddListener(() => OnBuyButtonClick());

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
    }

    void OnCityButtonClick()
    {
        VerticalScroll.SetActive(false);
        TestBtn.gameObject.SetActive(false);
        HideMenuButtons();
        BuyingPanel.SetActive(true);
    }

    void OnExitButtonClick()
    {
        BuyingPanel.SetActive(false);
        VerticalScroll.SetActive(true);
        TestBtn.gameObject.SetActive(true);
        ShowMenuButtons();
    }

    void OnBuyButtonClick()
    {
        Debug.Log("Buy button clicked");
    }

    void OnCentralButtonClick()
    {
        SceneManager.LoadScene("TsentralnyyScene");
    }

    void OnFabrichnyyButtonClick()
    {
        SceneManager.LoadScene("FabrichnyyScene");
    }

    void OnZavolzhskyButtonClick()
    {
        SceneManager.LoadScene("ZavolzhskyScene");
    }

    void OnFirstAchievButtonClick()
    {
        SceneManager.LoadScene("SceneForAchievments");
    }

    void OnTestButtonClick()
    {
        SceneManager.LoadScene("test");
    }

    void ShowScreen(GameObject screenToShow)
    {
        MainScreen.SetActive(screenToShow == MainScreen);
        AchievementsScreen.SetActive(screenToShow == AchievementsScreen);
        SettingsScreen.SetActive(screenToShow == SettingsScreen);
        ShopScreen.SetActive(screenToShow == ShopScreen);
        AreaSelectingScreen.SetActive(screenToShow == AreaSelectingScreen);
    }

    void HideMenuButtons()
    {
        MenuBtn.gameObject.SetActive(false);
        foreach (RectTransform btnTransform in buttonTransforms)
        {
            btnTransform.gameObject.SetActive(false);
        }
    }

    void ShowMenuButtons()
    {
        MenuBtn.gameObject.SetActive(true);
        foreach (RectTransform btnTransform in buttonTransforms)
        {
            btnTransform.gameObject.SetActive(true);
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

    // This method is called whenever the RectTransform's dimensions change (including screen orientation change)
    void OnRectTransformDimensionsChange()
    {
        if (isMenuExpanded)
        {
            ToggleMenu(); // Collapse the menu
        }
    }
}
