using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Gender Gender { get; set; }
    }
    public enum Gender { Male, Female}
}
