using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.BookVMs
{
    public partial class BookVM : BaseCRUDVM<Book>
    {

        public BookVM()
        {
            SetInclude(x => x.Photos);
        }

        protected override void InitVM()
        {
            Console.WriteLine($"Setting:    {ConfigInfo.AppSettings["key1"]}");
            Console.WriteLine($"CookiePre:  {ConfigInfo.CookiePre}");
            




        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
