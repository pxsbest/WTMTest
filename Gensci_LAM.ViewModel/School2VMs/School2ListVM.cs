using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.School2VMs
{
    public partial class School2ListVM : BasePagedListVM<School2_View, School2Searcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("School2", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("School2", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("School2", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("School2", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("School2", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("School2", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("School2", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("School2", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<School2_View>> InitGridHeader()
        {
            return new List<GridColumn<School2_View>>{
                this.MakeGridHeader(x => x.SchoolName),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<School2_View> GetSearchQuery()
        {
            var query = DC.Set<School2>()
                .CheckContain(Searcher.SchoolName, x=>x.SchoolName)
                .Select(x => new School2_View
                {
				    ID = x.ID,
                    SchoolName = x.SchoolName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class School2_View : School2{

    }
}
