using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ErsaCase.Core.Model
{
    
        public class BaseEntity
        {
            [Key]
            [Required]

            public int Id { get; set; }

            [JsonIgnore]
            public Guid LogicalRef { get; set; } = Guid.NewGuid();

            [JsonIgnore]
            public DateTime CreatedDate { get; set; } = DateTime.Now;

            [JsonIgnore]
            public DateTime EditTime { get; set; }

            [JsonIgnore]
            public int CreatedById { get; set; }

            [JsonIgnore]
            public int EditById { get; set; }

            [JsonIgnore]
            public bool IsDeleted { get; set; }
        }
}
