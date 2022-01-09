using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.StudentVMs
{
    public partial class StudentSearcher : BaseSearcher
    {
        [Display(Name = "学生编码")]
        public String StudentCode { get; set; }
        [Display(Name = "学生姓名")]
        public String StudentName { get; set; }
        [Display(Name = "学生类型")]
        public StudentTypeEnum? StudentType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
