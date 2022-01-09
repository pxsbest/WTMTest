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
    public partial class School2TemplateVM : BaseTemplateVM
    {
        [Display(Name = "学校名称")]
        public ExcelPropety SchoolName_Excel = ExcelPropety.CreateProperty<School2>(x => x.SchoolName);

	    protected override void InitVM()
        {
        }

    }

    public class School2ImportVM : BaseImportVM<School2TemplateVM, School2>
    {

    }

}
