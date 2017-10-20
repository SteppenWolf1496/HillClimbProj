using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyController : SingletonePattern<MainLobbyController>
{
    [SerializeField] private Text tfName;
    [SerializeField] private Text tfLvl;
    [SerializeField] private Text tfMetal;
    [SerializeField] private Text tfMoney;

    [SerializeField] protected ChestSlot metalChest;
    [SerializeField] protected ChestSlot silverChest;
    [SerializeField] protected ChestSlot goldChest;
    [SerializeField] protected ChestSlot freeChest;
    [SerializeField] protected HorizontalLayoutGroup chestsLayout;

    public List<ChestSlot> slots = new List<ChestSlot>();


    void Awake()
    {
        Model.LoadUcars();
        Model.LoadMetal();
        Model.LoadMoney();
        Model.LoadChests();
        Model.LoadUCards();
    }
	// Use this for initialization
	void Start ()
	{
	    UpdateChests();

	}

    
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateChests()
    {
        metalChest.gameObject.SetActive(false);
        silverChest.gameObject.SetActive(false);
        goldChest.gameObject.SetActive(false);
        freeChest.gameObject.SetActive(false);


        foreach (var chest in Model.UChests)
        {
            ChestSlot find = slots.Find(chestSlot => chestSlot.uchest.TimeWhenAdd == chest.TimeWhenAdd);
            if (find)
            {
               
                find.UpdateData(chest);
                continue;
            }

            ChestSlot slot = null;
            switch (chest.Rarity)
            {
                case ChestRarity.Common:
                    slot = Instantiate(metalChest, chestsLayout.transform).GetComponent<ChestSlot>();
                    break;
                case ChestRarity.Rare:
                    slot = Instantiate(silverChest, chestsLayout.transform).GetComponent<ChestSlot>();
                    break;
                case ChestRarity.Epic:
                    slot = Instantiate(goldChest, chestsLayout.transform).GetComponent<ChestSlot>();
                    break;
                case ChestRarity.free:
                    slot = Instantiate(freeChest, chestsLayout.transform).GetComponent<ChestSlot>();
                    break;
                default:
                    slot = null;
                    break;
            }
            if (slot == null) continue;
            slot.gameObject.SetActive(true);
            slot.SetData(Model.GetChestData(chest.Rarity), chest);
            slots.Add(slot);
            //chestsLayout.
        }
    }

    public void GoToOfflineRacing()
    {
        ScreenManager.showScreen(ScreenManager.Screens.CHOOSE_CAR_MENU);
    }

    public void GoToOnlineRacing()
    {
        NotifyData.AddNew("Looking for updates");
    }
#if CHEATS
    public void AddChest()
    {
        UserChestData chest = new UserChestData();
        chest.Lvl = 1;
        chest.Rarity = ChestRarity.Common;
        chest.TimeWhenAdd = Utility.TimeInt;

        Model.UChests.Add(chest);
        Model.SaveChests();
        UpdateChests();
    }
#endif


}
