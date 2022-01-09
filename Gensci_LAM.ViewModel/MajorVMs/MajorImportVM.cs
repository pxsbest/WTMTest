using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.MajorVMs
{
    public partial class MajorTemplateVM : BaseTemplateVM
    {
        [Display(Name = "专业编码")]
        public ExcelPropety MajorCode_Excel = ExcelPropety.CreateProperty<Major>(x => x.MajorCode);
        [Display(Name = "专业名称")]
        public ExcelPropety MajorName_Excel = ExcelPropety.CreateProperty<Major>(x => x.MajorName);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Major>(x => x.Remark);
        public ExcelPropety SchoolMdl_Excel = ExcelPropety.CreateProperty<Major>(x => x.SchoolMdlId);

	    protected override void InitVM()
        {
            SchoolMdl_Excel.DataType = ColumnDataType.ComboBox;
            SchoolMdl_Excel.ListItems = DC.Set<SchoolMdl>().GetSelectListItems(Wtm, y => y.SchoolName);
        }

    }

    public class MajorImportVM : BaseImportVM<MajorTemplateVM, Major>
    {

    }

}
