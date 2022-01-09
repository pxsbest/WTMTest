using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.StudentMdlVMs
{
    public partial class StudentMdlListVM : BasePagedListVM<StudentMdl_View, StudentMdlSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("StudentMdl", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("StudentMdl", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMdl", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMdl", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMdl", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMdl", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMdl", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("StudentMdl", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<StudentMdl_View>> InitGridHeader()
        {
            return new List<GridColumn<StudentMdl_View>>{
                this.MakeGridHeader(x => x.LoginName),
                this.MakeGridHeader(x => x.Password),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.CellPhone),
                this.MakeGridHeader(x => x.ZipCode),
                this.MakeGridHeader(x => x.EnRollDate),
                this.MakeGridHeader(x => x.PhotoId).SetFormat(PhotoIdFormat),
                this.MakeGridHeader(x => x.MajorName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> PhotoIdFormat(StudentMdl_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.PhotoId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.PhotoId,640,480),
            };
        }


        public override IOrderedQueryable<StudentMdl_View> GetSearchQuery()
        {
            var query = DC.Set<StudentMdl>()
                .CheckContain(Searcher.Name, x=>x.Name)
               
                .DPWhere(Wtm,s=>s.ID)

                /*
                 1.并 关系
                    .DPWhere()
                    .DPWhere()
                2.或 关系
                    .DPWhere(Wtm,s=>s.ID,s=>s.Name,多个表达式之间是或有关系)
                 */

                .Select(x => new StudentMdl_View
                {
				    ID = x.ID,
                    LoginName = x.LoginName,
                    Password = x.Password,
                    Name = x.Name,
                    CellPhone = x.CellPhone,
                    ZipCode = x.ZipCode,
                    EnRollDate = x.EnRollDate,
                    PhotoId = x.PhotoId,
                    MajorName_view = x.StudentMajor.Select(y=>y.Major.MajorName).ToSepratedString(null,","), 
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class StudentMdl_View : StudentMdl{
        [Display(Name = "专业名称")]
        public String MajorName_view { get; set; }

    }
}
