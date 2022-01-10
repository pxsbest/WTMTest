using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.StudentMdlVMs
{
    public partial class StudentMdlVM : BaseCRUDVM<StudentMdl>
    {
        //页面中的下拉选项,在InitVM中做 初始化
        public List<ComboSelectListItem> AllStudentMajors { get; set; }

        [Display(Name = "专业")]
        public List<string> SelectedStudentMajorIDs { get; set; }

        public StudentMdlVM()
        {
            SetInclude(x => x.StudentMajor);
        }

        protected override void InitVM()
        {
            AllStudentMajors = DC.Set<Major>().GetSelectListItems(Wtm, y => y.MajorName);
            SelectedStudentMajorIDs = Entity.StudentMajor?.Select(x => x.MajorId.ToString()).ToList();
        }

        public override void DoAdd()
        {
            Entity.StudentMajor = new List<StudentMajor>();
            if (SelectedStudentMajorIDs != null)
            {
                foreach (var id in SelectedStudentMajorIDs)
                {
                     StudentMajor middle = new StudentMajor();
                    middle.SetPropertyValue("MajorId", id);
                    Entity.StudentMajor.Add(middle);
                }
            }
           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            Entity.StudentMajor = new List<StudentMajor>();
            if(SelectedStudentMajorIDs != null )
            {
                 foreach (var item in SelectedStudentMajorIDs)
                {
                    StudentMajor middle = new StudentMajor();
                    middle.SetPropertyValue("MajorId", item);
                    Entity.StudentMajor.Add(middle);
                }
            }

            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
