using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vovinam.Common.Enums
{
    public enum Tuition:int
    {
        [Display(Name = "Tháng 1")]
        Tuition1 = 1,
        [Display(Name = "Tháng 2")]
        Tuition2 = 2,
        [Display(Name = "Tháng 3")]
        Tuition3 = 3,
        [Display(Name = "Tháng 4")]
        Tuition4 = 4,
        [Display(Name = "Tháng 5")]
        Tuition5 = 5,
        [Display(Name = "Tháng 6")]
        Tuition6 = 6,
        [Display(Name = "Tháng 7")]
        Tuition7 = 7,
        [Display(Name = "Tháng 8")]
        Tuition8 = 8,
        [Display(Name = "Tháng 9")]
        Tuition9 = 9,
        [Display(Name = "Tháng 10")]
        Tuition10 = 10,
        [Display(Name = "Tháng 11")]
        Tuition11 = 11,
        [Display(Name = "Tháng 12")]
        Tuition12 = 12
    }
}
