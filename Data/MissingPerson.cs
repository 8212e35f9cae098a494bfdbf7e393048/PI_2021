using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMissingPersonAppValid.Data
{

    [Table("People")]
    public class MissingPerson
    {
        [Key]
        public int PersonID { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public char Gender { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        [MaxLength(32)]
        public string Picture { get; set; }
        [MaxLength(50)]
        public string City { get; set; }

    }
}
