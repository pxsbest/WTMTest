using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.BookVMs
{
    public partial class BookTemplateVM : BaseTemplateVM
    {
        [Display(Name = "书名")]
        public ExcelPropety BookName_Excel = ExcelPropety.CreateProperty<Book>(x => x.BookName);
        [Display(Name = "书目分类")]
        public ExcelPropety BookType_Excel = ExcelPropety.CreateProperty<Book>(x => x.BookType);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Book>(x => x.Remark);

	    protected override void InitVM()
        {
        }

    }

    public class BookImportVM : BaseImportVM<BookTemplateVM, Book>
    {

    }

}
