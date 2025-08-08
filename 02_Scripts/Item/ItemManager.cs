using _02_Scripts.Inventory;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager> 
{
    [SerializeField]
    private ItemTable itemTable;
    [SerializeField]
    private InventoryManager inventoryManager;
    
    public MonsterDropTable monsterDropTable;
    public StageRewardTable stageRewardTable;
    
    public ItemData GetItem(int id)
    {
        ItemData item = itemTable.GetItem(id);
        return item;
    }

    private void Start()
    {
        InventoryManager.Instance.addGold(50);
    }

    [ContextMenu("Add Item")]
    private void AddPotion()
    {
        InventoryManager.Instance.AddItem(1);
        InventoryManager.Instance.AddItem(2);
        InventoryManager.Instance.AddItem(3);
        InventoryManager.Instance.AddItem(4);
        InventoryManager.Instance.AddItem(11);
        InventoryManager.Instance.AddItem(11);
        InventoryManager.Instance.AddItem(11);
        InventoryManager.Instance.AddItem(11);
        InventoryManager.Instance.AddItem(11);
        InventoryManager.Instance.AddItem(11);
        InventoryManager.Instance.AddItem(12);
        InventoryManager.Instance.AddItem(21);
        InventoryManager.Instance.AddItem(21);
        InventoryManager.Instance.AddItem(21);
        InventoryManager.Instance.AddItem(23);
        InventoryManager.Instance.AddItem(23);
        InventoryManager.Instance.AddItem(23);
        InventoryManager.Instance.AddItem(21);
        InventoryManager.Instance.AddItem(12);
        InventoryManager.Instance.AddItem(31);
        InventoryManager.Instance.AddItem(31);
        InventoryManager.Instance.AddItem(31);
        InventoryManager.Instance.AddItem(31);
        InventoryManager.Instance.AddItem(31);
        InventoryManager.Instance.AddItem(31);

    }

    public class DropSystem
    {
        private MonsterDropTable dropTable;

        public DropSystem(MonsterDropTable table)
        {
            this.dropTable = table;
        }
        
        public List<int> GetDropItems(string monsterName, out int dropGold)
        {
            dropGold = 0;

            // 몬스터 정보 찾기
            MonsterDrop monster = dropTable.MonsterDropList.Find(m => m.MonsterName == monsterName);

            if (monster == null)
            {
                return new List<int>();
            }

            dropGold = monster.DropGold;
            List<int> droppedItems = new();

            foreach (var drop in monster.ItemDropList)
            {
                float roll = Random.value;
                if (roll <= drop.chance)
                {
                    droppedItems.Add(drop.itemId);
                }
            }

            return droppedItems;
        }
    }
}