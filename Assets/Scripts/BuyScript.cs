using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
    public GameObject VerticalScroll;
    public GameObject TestBtn;
    public GameObject BuyingPanel;
    public Button CentalBtn;
    public Button FabrichBtn;
    public Button ZavoljskBtn;
    public Button ExitBtn;
    public Button BuyBtn;

    void Start()
    {
        CentalBtn.onClick.AddListener(() => OnCityButtonClick());
        FabrichBtn.onClick.AddListener(() => OnCityButtonClick());
        ZavoljskBtn.onClick.AddListener(() => OnCityButtonClick());
        ExitBtn.onClick.AddListener(() => OnExitButtonClick());
        BuyBtn.onClick.AddListener(() => OnBuyButtonClick());

        BuyingPanel.SetActive(false); // Ensure BuyingPanel is hidden at the start
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
}
