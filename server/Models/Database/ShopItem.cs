using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models.Database {
    public class ShopItem {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string PictureUrl { get; set; }
        public string ImgAltText { get; set; }
        public string Title { get; set; }
        public string shortDescription { get; set; }
        public string longDescription { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        public bool isAvailable { get; set; }
        public bool isSold { get; set; }

        public UserInfo PurchasedBy { get; set; }

        public string BlockAddressOfPurchase { get; set; }

    }
    // HACK: maybe the purchased by and block address don't belong here?

}