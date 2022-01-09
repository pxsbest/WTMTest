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
    public partial class School2BatchVM : BaseBatchVM<School2, School2_BatchEdit>
    {
        public School2BatchVM()
        {
            ListVM = new School2ListVM();
            LinkedVM = new School2_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class School2_BatchEdit : BaseVM
    {
        [Display(Name = "学校名称")]
        public String SchoolName { get; set; }

        protected override void InitVM()
        {
        }

    }

}
