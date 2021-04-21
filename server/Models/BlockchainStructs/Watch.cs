using System;
using System.Numerics;

namespace server.Models.BlockchainStructs {
    public class Watch {
        public string VendorAddress { get; set; }
        public string ClientAddress { get; set; }
        public string WatchType { get; set; }
        public string CreationAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public BigInteger GUID { get; set; }
        public DateTimeOffset MaterialsReceivedTimeStamp { get; set; }
        public DateTimeOffset AssembledTimeStamp { get; set; }
        public DateTimeOffset SentTimeStamp { get; set; }
        public DateTimeOffset DeliveredTimeStamp { get; set; }
    }
}