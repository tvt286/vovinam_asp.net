using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vovinam.Areas.Api.Models
{
    public class LevelUpModel
    {
        public int id { get; set; }
        public int stt { get; set; }
        public Nullable<int> birthday { get; set; }
        public string gender { get; set; }
        public Nullable<int> weight { get; set; }
        public double total { get; set; }
        public string ketqua { get; set; }
        public string name { get; set; }

        public virtual ClubModel club { get; set; }
        public virtual CoBanModel co_ban { get; set; }
        public virtual DoiKhangModel doi_khang { get; set; }
        public virtual ExaminationModel examination { get; set; }
        public virtual LevelModel level { get; set; }
        public virtual QuyenModel quyen { get; set; }
        public virtual SongLuyenModel song_luyen { get; set; }
        public virtual TheLucModel the_luc { get; set; }
        public virtual VoDaoModel vo_dao { get; set; }
      }


   
   

  

   


}