using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Attributes;

namespace Gensci_LAM.Model
{
    //多对多,中间表
    [MiddleTable]
    public class StudentMajor : BasePoco
    {
        //一个学生可有多个专业,一个专业可有多个学生

  
        public StudentMdl StudentMdl { get; set; }

        [Display(Name = "学生ID")]
        public Guid StudentMdlId { get; set; }


        public Major Major { get; set; }


        [Display(Name = "专业ID")]
        public Guid MajorId { get; set; }

    }
}
