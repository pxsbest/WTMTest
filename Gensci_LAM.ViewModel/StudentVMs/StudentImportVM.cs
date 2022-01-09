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
    public partial class StudentTemplateVM : BaseTemplateVM
    {
        [Display(Name = "学生编码")]
        public ExcelPropety StudentCode_Excel = ExcelPropety.CreateProperty<Student>(x => x.StudentCode);
        [Display(Name = "学生姓名")]
        public ExcelPropety StudentName_Excel = ExcelPropety.CreateProperty<Student>(x => x.StudentName);
        [Display(Name = "学生类型")]
        public ExcelPropety StudentType_Excel = ExcelPropety.CreateProperty<Student>(x => x.StudentType);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Student>(x => x.Remark);

	    protected override void InitVM()
        {
        }

    }

    public class StudentImportVM : BaseImportVM<StudentTemplateVM, Student>
    {

    }

}
