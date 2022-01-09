using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.SchoolMdlVMs
{
    public partial class SchoolMdlTemplateVM : BaseTemplateVM
    {
        [Display(Name = "学校编码")]
        public ExcelPropety SchoolCode_Excel = ExcelPropety.CreateProperty<SchoolMdl>(x => x.SchoolCode);
        [Display(Name = "学校名称")]
        public ExcelPropety SchoolName_Excel = ExcelPropety.CreateProperty<SchoolMdl>(x => x.SchoolName);
        [Display(Name = "学校类型")]
        public ExcelPropety SchoolType_Excel = ExcelPropety.CreateProperty<SchoolMdl>(x => x.SchoolType);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<SchoolMdl>(x => x.Remark);

	    protected override void InitVM()
        {
        }

    }

    public class SchoolMdlImportVM : BaseImportVM<SchoolMdlTemplateVM, SchoolMdl>
    {

    }

}
