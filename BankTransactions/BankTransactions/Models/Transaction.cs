using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankTransactions.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId  { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        [DisplayName("Numero da Conta")]
        [Required(ErrorMessage ="Esse campo é obrigatório")]
        [MaxLength(12, ErrorMessage="Número máximo de caracteres: 12")]
        public string AccountNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Nome do Beneficiário")]
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string BeneficiaryName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Banco")]
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string BanckName { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        [DisplayName("SWIFT")]
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(11, ErrorMessage = "Número máximo de caracteres: 11")]
        public string SWIFTCode { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public int Amount { get; set; }

        [DisplayName("Data")]
        [DisplayFormat(DataFormatString ="{0:dd-MMM-yyyy}")]
        public DateTime Date { get; set; }
    }
}
