using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;
using Microsoft.EntityFrameworkCore;
using Gensci_LAM.ViewModel.SchoolVMs;

namespace Gensci_LAM.ViewModel.StudentVMs
{
    public partial class StudentVM : BaseCRUDVM<Student>
    {

        public SchoolListVM Schools { get; set; }


        public StudentVM()
        {
        }


        public override void Validate()
        {
            //:自定义验证举例
            //var cnt=DC.Set<Student>().AsNoTracking().Where(s => s.ID == Entity.ID).Count();
            //if (cnt == 0)
            //{
            //    MSD.AddModelError("Entity.LocationId", "所选地区没有病人");
            //}

            base.Validate();

        }



        protected override void InitVM()
        {

            Schools = new SchoolListVM();
            Schools.CopyContext(this);




        }

        protected override void ReInitVM()
        {
            base.ReInitVM();
        }



        protected override Student GetById(object Id)
        {
            return base.GetById(Id);
        }


        public override DuplicatedInfo<Student> SetDuplicatedCheck()
        {
            var rv = CreateFieldsInfo(SimpleField(s => s.StudentName));
            //var rv2 = CreateFieldsInfo(SimpleField(s => s.StudentName),SimpleField(s=>s.Remark));  //且关系: 两个字段做为一组,不能重复

            rv.AddGroup(SimpleField(s => s.StudentCode));

            return base.SetDuplicatedCheck();
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
