using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;
using System.Text.Json.Serialization;


namespace ClientesAPI.Models
{
    public class Cliente : BaseModel
    {
        [PrimaryKey("id", false)]
        [JsonIgnore]
        public int Id { get; set; }

        [Column("Nombre")]
        public  string Nombre { get; set; } = string.Empty; 

        [Column("Apellido")]
        public  string Apellido { get; set; } = string.Empty; 

        [Column("Direccion")]
        public  string Direccion { get; set; } = string.Empty; 
       
    }
}