﻿using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Button StartOpen;
    [SerializeField] private Button SpeedUpOpening;
    [SerializeField] private Button GetRewardBtn;

    public Chest chest;
    public UserChestData uchest;

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
                break;
            case ChestState.Opening:
                SpeedUpOpening.gameObject.SetActive(true);
                break;
            case ChestState.Opened:
                GetRewardBtn.gameObject.SetActive(true);
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
        uchest.state = ChestState.Opened;
        uchest.TimeWhenOpen = 0;
        Model.SaveChests();
        MainLobbyController.Instance.UpdateChests();
    }

    public void GetReward()
    {
        MainLobbyController.Instance.slots.Remove(this);
        Model.UChests.Remove(uchest);
        Model.SaveChests();
        MainLobbyController.Instance.UpdateChests();
        Destroy(gameObject);
    }
}
