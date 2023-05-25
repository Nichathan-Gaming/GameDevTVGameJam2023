using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VinShopController : MonoBehaviour
{
    [SerializeField] GameObject Shop;

    [SerializeField] GameObject VatnVinButton;
    [SerializeField] GameObject BarnVinButton;
    [SerializeField] GameObject UngrVinButton;
    [SerializeField] GameObject FullrVinButton;

    [SerializeField] Vin selectedVin;
    [SerializeField] int vinCount;

    [SerializeField] GameObject SliderParent;
    [SerializeField] Slider CountSlider;
    [SerializeField] TMP_Text SliderText;

    [SerializeField] Button DecreaseButton;
    [SerializeField] Button IncreaseButton;

    [SerializeField] Button BuyButton;

    [SerializeField] bool canOpen;
    [SerializeField] bool canClose;

    [SerializeField] int maxAffordable;

    [SerializeField] TMP_Text ShopMessage;
    [SerializeField] Color targetColor;

    [SerializeField] Button CloseButton;

    [SerializeField] Coroutine coroutine;

    private void Start()
    {
        VatnVinButton.GetComponent<Button>().onClick.AddListener(() => SelectVin(PlayerController.instance.player.inventory.vatnvin));
        BarnVinButton.GetComponent<Button>().onClick.AddListener(() => SelectVin(PlayerController.instance.player.inventory.barnvin));
        UngrVinButton.GetComponent<Button>().onClick.AddListener(() => SelectVin(PlayerController.instance.player.inventory.ungrvin));
        FullrVinButton.GetComponent<Button>().onClick.AddListener(() => SelectVin(PlayerController.instance.player.inventory.fullrvin));

        DecreaseButton.GetComponent<Button>().onClick.AddListener(() => { CountSlider.value--; });
        IncreaseButton.GetComponent<Button>().onClick.AddListener(() => { CountSlider.value++; });

        CloseButton.GetComponent<Button>().onClick.AddListener(() => { ShopClose(); });

        Shop.SetActive(false);
        BuyButton.interactable = false;
        SliderParent.SetActive(false);
        canOpen = false;
        canClose = false;

        vinCount = (int)CountSlider.value;
        SliderText.enabled = false;
        DecreaseButton.interactable = false;
        IncreaseButton.interactable = false;
        CountSlider.minValue = 1;

        ShopMessage.enabled = false;
    }

    private void LateUpdate()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.F) && Time.deltaTime != 0)
        {
            BuyButton.interactable = false;
            Shop.SetActive(true);
            ShopMessage.enabled = false;
            canClose = true;
            canOpen = false;
        }
        else if ((canClose) && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape)))
        {
            ShopClose();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canOpen = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canClose = false;
        canOpen = false;
        Shop.SetActive(false);
        SliderParent.SetActive(false);
        CountSlider.value = CountSlider.minValue;
        SliderText.enabled = false;
        HUD.instance.InitSilverDisplay();
        HUD.instance.InitVinDisplay();

        DecreaseButton.interactable = false;
        IncreaseButton.interactable = false;
    }

    public void SelectVin(Vin targetVin)
    {
        HUD.instance.InitVinDisplay();

        selectedVin = targetVin;
        BuyButton.interactable = true;
        SliderParent.SetActive(true);

        ShopMessage.enabled = false;

        CountSlider.value = CountSlider.minValue;

        if (CountSlider.value != 0f)
        {
            DecreaseButton.interactable = true;
            IncreaseButton.interactable = true;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        if (PlayerController.instance.player.inventory.silfr < selectedVin.cost)
        {
            coroutine = StartCoroutine(ShowShopMessage("Not enough silver!", Color.red));
        }
        else if (selectedVin.count == selectedVin.maxVinCount)
        {
            coroutine = StartCoroutine(ShowShopMessage("Inventory is full!", Color.yellow));
        }

        PurchasePrediction();
        CountMaxAffordable();
    }

    public void BuyVin()
    {
        PlayerController.instance.player.inventory.BuyVin(vinCount, selectedVin.type);
        HUD.instance.InitSilverDisplay();
        HUD.instance.InitVinDisplay();

        CountMaxAffordable();
        CountSlider.value = CountSlider.minValue;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ShowShopMessage("Purchased!", Color.green));
    }

    public void PurchasePrediction()
    {
        vinCount = (int)CountSlider.value;
        HUD.instance.silverDisplay.text = HUD.instance.InitSilverDisplay() + $" <color=red>- {selectedVin.cost * vinCount}</color>";

        SliderText.enabled = true;
        SliderText.text = vinCount.ToString();

        CountMaxAffordable();

        if (CountSlider.value != 0f)
        {
            if (selectedVin == PlayerController.instance.player.inventory.vatnvin)
            {
                HUD.instance.vatnVinDisplay.text = $"VatnVin: {PlayerController.instance.player.inventory.vatnvin.count} <color=green>+ {vinCount}</color>";
            }
            else if (selectedVin == PlayerController.instance.player.inventory.barnvin)
            {
                HUD.instance.barnVinDisplay.text = $"BarnVin: {PlayerController.instance.player.inventory.barnvin.count} <color=green>+ {vinCount}</color>";
            }
            else if (selectedVin == PlayerController.instance.player.inventory.ungrvin)
            {
                HUD.instance.ungrVinDisplay.text = $"UngrVin: {PlayerController.instance.player.inventory.ungrvin.count} <color=green>+ {vinCount}</color>";
            }
            else if (selectedVin == PlayerController.instance.player.inventory.fullrvin)
            {
                HUD.instance.fullrVinDisplay.text = $"FullrVin: {PlayerController.instance.player.inventory.fullrvin.count} <color=green>+ {vinCount}</color>";
            }
        }
    }

    public void CountMaxAffordable()
    {
        if (PlayerController.instance.player.inventory.silfr > (selectedVin.maxVinCount - selectedVin.count) * selectedVin.cost)
        {
            maxAffordable = selectedVin.maxVinCount - selectedVin.count;
        }
        else
        {
            maxAffordable = PlayerController.instance.player.inventory.silfr / selectedVin.cost;
        }

        CountSlider.maxValue = maxAffordable;

        if (CountSlider.value == CountSlider.minValue)
        {
            DecreaseButton.interactable = false;
            IncreaseButton.interactable = true;
        }
        else if (CountSlider.value == CountSlider.maxValue)
        {
            DecreaseButton.interactable = true;
            IncreaseButton.interactable = false;
        }
        else
        {
            DecreaseButton.interactable = true;
            IncreaseButton.interactable = true;
        }

        if ((PlayerController.instance.player.inventory.silfr < selectedVin.cost) || (selectedVin.count == selectedVin.maxVinCount))
        {
            SliderParent.SetActive(false);
            BuyButton.interactable = false;

            HUD.instance.InitSilverDisplay();
            HUD.instance.InitVinDisplay();
            SliderText.enabled = false;

            DecreaseButton.interactable = false;
            IncreaseButton.interactable = false;
        }
    }

    public void ShopClose()
    {
        Shop.SetActive(false);
        canClose = false;
        canOpen = true;
        BuyButton.interactable = false;
        SliderParent.SetActive(false);
        CountSlider.value = 1;
        SliderText.enabled = false;
        HUD.instance.InitSilverDisplay();
        HUD.instance.InitVinDisplay();

        DecreaseButton.interactable = false;
        IncreaseButton.interactable = false;
    }

    IEnumerator ShowShopMessage(string messageText, Color messageColor)
    {
        ShopMessage.text = messageText;
        ShopMessage.color = messageColor;
        ShopMessage.enabled = true;
        yield return new WaitForSecondsRealtime(3f);
        ShopMessage.enabled = false;
    }
}