using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.StudentVMs
{
    public partial class StudentListVM : BasePagedListVM<Student_View, StudentSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Student", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800)
                .SetShowDialog(false).SetIsRedirect(true),
                this.MakeStandardAction("Student", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("Student", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("Student", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("Student", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("Student", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("Student", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("Student", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<Student_View>> InitGridHeader()
        {
            return new List<GridColumn<Student_View>>{
                this.MakeGridHeader(x => x.StudentCode),
                this.MakeGridHeader(x => x.StudentName).SetSort().SetFormat((a,b)=>{
                //string rv=$"<a href='Home/PIndex#/Student/Details/{a.ID}' target='_blank'>{a.StudentName}</a>";
                string rv=$"<a href='Home/Index#/Student/Details/{a.ID}' target='_blank'>{a.StudentName}</a>";
                    return rv;
                }),
                this.MakeGridHeader(x => x.StudentType),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeader(x => x.PhotoId).SetFormat(PhotoIdFormat),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> PhotoIdFormat(Student_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.PhotoId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.PhotoId,640,480),
            };
        }


        public override IOrderedQueryable<Student_View> GetSearchQuery()
        {
            var query = DC.Set<Student>()
                .CheckContain(Searcher.StudentCode, x=>x.StudentCode)
                .CheckContain(Searcher.StudentName, x=>x.StudentName)
                .CheckEqual(Searcher.StudentType, x=>x.StudentType)
                .Select(x => new Student_View
                {
				    ID = x.ID,
                    StudentCode = x.StudentCode,
                    StudentName = x.StudentName,
                    StudentType = x.StudentType,
                    Remark = x.Remark,
                    PhotoId = x.PhotoId,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Student_View : Student{

    }
}
