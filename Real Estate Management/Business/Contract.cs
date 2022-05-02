using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management.Business
{
    [Serializable]
    public class Contract
    {
        public Contract() { }
        public Contract(string id, string sellerId, string buyerId, DateTime contractDate, double amount, string agentId, string houseId)
        {
            Id = id;
            SellerId = sellerId;
            BuyerId = buyerId;
            HouseId = houseId;
            ContractDate = contractDate;
            Amount = amount;
            AgentId = agentId;
        }

        public string Id { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
        public string HouseId { get; set; }
        public DateTime ContractDate { get; set; }
        public double Amount { get; set; }
        public string AgentId { get; set; }
        public string BuyerName { get { return GetBuyer(); } }
        public string SellerName { get { return GetSeller(); } }
        public string HouseName { get { return GetHouse(); } }
        public string AgentName { get { return GetAgent(); } }

        private string GetBuyer()
        {
            return Store.ReadAllSellers().Find(x => x.Id == BuyerId).ToString();
        }
        private string GetSeller()
        {
            return Store.ReadAllSellers().Find(x => x.Id == SellerId).ToString();
        }
        private string GetHouse()
        {
            return Store.ReadAllHouses().Find(x => x.Id == HouseId).ToString();
        }
        private string GetAgent()
        {
            return Store.ReadAllUsers().Find(x => x.Id == AgentId).ToString();
        }

    }
}
