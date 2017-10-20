using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text TimeToOpen;
    [SerializeField] private Button StartOpen;
    [SerializeField] private Button SpeedUpOpening;
    [SerializeField] private Button GetRewardBtn;

    public Chest chest;
    public UserChestData uchest;

    private float updateCounter = 0;

    void Update()
    {
        if (uchest.state != ChestState.Opening) return;
        TimeToOpen.text = Utility.GetTimer(uchest.TimeWhenOpen - Utility.TimeInt);
    }

    public void SetData(Chest _static,UserChestData _dynamic)
    {
        chest = _static;
        uchest = _dynamic;
        UpdateUI();
    }

    public void UpdateData(UserChestData _dynamic)
    {
        uchest = _dynamic;
        UpdateUI();
    }

    private void UpdateUI()
    {
        title.text = chest.Title;

        StartOpen.gameObject.SetActive(false);
        SpeedUpOpening.gameObject.SetActive(false);
        GetRewardBtn.gameObject.SetActive(false);

        switch (uchest.state)
        {
            case ChestState.Closed:
                StartOpen.gameObject.SetActive(true);
                TimeToOpen.gameObject.SetActive(false);
                break;
            case ChestState.Opening:
                SpeedUpOpening.gameObject.SetActive(true);
                TimeToOpen.gameObject.SetActive(true);
                TimeToOpen.text = Utility.GetTimer(uchest.TimeWhenOpen - Utility.TimeInt);
                break;
            case ChestState.Opened:
                GetRewardBtn.gameObject.SetActive(true);
                TimeToOpen.gameObject.SetActive(false);
                break;
        }

    }

    public void StartOpenChest()
    {
        uchest.state = ChestState.Opening;
        uchest.TimeWhenOpen = Utility.TimeInt + Model.GetChestData(uchest.Rarity).TimeToOpen;
        Model.SaveChests();
        MainLobbyController.Instance.UpdateChests();
    }

    public void SpeedupOpenChest()
    {
       // uchest.state = ChestState.Opened;
        uchest.TimeWhenOpen = Utility.TimeInt + 10;
        Model.SaveChests();
        MainLobbyController.Instance.UpdateChests();
    }

    public void GetReward()
    {
        MainLobbyController.Instance.slots.Remove(this);
        CountDrop();


        Model.UChests.Remove(uchest);
        Model.SaveChests();
        MainLobbyController.Instance.UpdateChests();
        Destroy(gameObject);


    }

    private void CountDrop()
    {
        List<Card.DropCard> common = new List<Card.DropCard>();
        List<Card.DropCard> silver = new List<Card.DropCard>();
        List<Card.DropCard> gold = new List<Card.DropCard>();
        int cardCount = StaticData.CountCardsByRarity(Enums.CarModifationRarity.Common, uchest.Rarity);
        int totalCards = UnityEngine.Random.Range(chest.CommonFrom, chest.CommonTo);
        //while
        
        //Model.GetUserCard()

        Model.SaveCards();
    }
}
