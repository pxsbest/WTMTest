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
    public partial class BookBatchVM : BaseBatchVM<Book, Book_BatchEdit>
    {
        public BookBatchVM()
        {
            ListVM = new BookListVM();
            LinkedVM = new Book_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Book_BatchEdit : BaseVM
    {
        [Display(Name = "书名")]
        public String BookName { get; set; }
        [Display(Name = "书目分类")]
        public BookTypeEnum? BookType { get; set; }
        [Display(Name = "备注")]
        public String Remark { get; set; }

        protected override void InitVM()
        {
        }

    }

}
