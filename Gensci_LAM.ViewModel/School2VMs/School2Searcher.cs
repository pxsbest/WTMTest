using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.School2VMs
{
    public partial class School2Searcher : BaseSearcher
    {
        [Display(Name = "学校名称")]
        public String SchoolName { get; set; }

        protected override void InitVM()
        {
        }

    }
}
