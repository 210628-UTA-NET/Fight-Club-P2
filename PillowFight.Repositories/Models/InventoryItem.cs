﻿namespace PillowFight.Repositories.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        public int Quantity { get; set; }
    }
}
