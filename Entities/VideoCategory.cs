using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    
    public class VideoCategory
    {
        public int Id { get; set; }     
        
        [Display (Name="Ad")]
        public string Name { get; set; }
    }

}
