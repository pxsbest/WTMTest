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
    public partial class StudentMdlBatchVM : BaseBatchVM<StudentMdl, StudentMdl_BatchEdit>
    {
        public StudentMdlBatchVM()
        {
            ListVM = new StudentMdlListVM();
            LinkedVM = new StudentMdl_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class StudentMdl_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
