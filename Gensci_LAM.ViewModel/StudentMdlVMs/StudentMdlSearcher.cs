using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.StudentMdlVMs
{
    public partial class StudentMdlSearcher : BaseSearcher
    {
        [Display(Name = "姓名")]
        public String Name { get; set; }

        protected override void InitVM()
        {
        }

    }
}
