using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    internal static HUD instance;

    public TMP_Text silverDisplay;

    public TMP_Text vatnVinDisplay;
    public TMP_Text barnVinDisplay;
    public TMP_Text ungrVinDisplay;
    public TMP_Text fullrVinDisplay;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PlayerController.instance.player.inventory.vatnvin.count = 1;
        PlayerController.instance.player.inventory.barnvin.count = 2;
        PlayerController.instance.player.inventory.ungrvin.count = 3;
        PlayerController.instance.player.inventory.fullrvin.count = 4;

        InitSilverDisplay();
        InitVinDisplay();
    }

    public string InitSilverDisplay()
    {
        silverDisplay.text = $"Silver: {PlayerController.instance.player.inventory.silfr}";
        return silverDisplay.text;
    }

    public void InitVinDisplay()
    {
        vatnVinDisplay.text = $"VatnVin: {PlayerController.instance.player.inventory.vatnvin.count}";
        barnVinDisplay.text = $"BarnVin: {PlayerController.instance.player.inventory.barnvin.count}";
        ungrVinDisplay.text = $"UngrVin: {PlayerController.instance.player.inventory.ungrvin.count}";
        fullrVinDisplay.text = $"FullrVin: {PlayerController.instance.player.inventory.fullrvin.count}";
    }
}
