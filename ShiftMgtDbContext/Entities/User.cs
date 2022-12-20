using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftMgtDbContext.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
            
        public string Email { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("UserID")]
        public virtual ProjectDetail ProjectDetail { get; set; }

        [ForeignKey("UserID")]
        public virtual Comment CommentDetail { get; set; }

        [ForeignKey("UserID")]
        public virtual UserCredential UserCredential { get; set; }

        
    }
    
}
