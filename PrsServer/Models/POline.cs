﻿namespace PrsServer.Models
{
    public class POline{

        public string Product { get; set; } =string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal LineTotal { get; set; }

    }
}
