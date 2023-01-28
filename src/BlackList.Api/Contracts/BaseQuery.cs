using System.ComponentModel.DataAnnotations;

namespace BlackList.Api.Contracts
{
    public class BaseQuery
    {
        [Required]
        public string PlayerNickname { get; set; } = null!;

        [Required]
        public Guid UserFaceitId { get; set; }
    }
}
