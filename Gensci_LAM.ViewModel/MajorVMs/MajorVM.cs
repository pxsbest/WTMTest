using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Gensci_LAM.Model;


namespace Gensci_LAM.ViewModel.MajorVMs
{
    public partial class MajorVM : BaseCRUDVM<Major>
    {
        public List<ComboSelectListItem> AllSchoolMdls { get; set; }
        public List<ComboSelectListItem> AllStudentMajorss { get; set; }
        [Display(Name = "学生")]
        public List<string> SelectedStudentMajorsIDs { get; set; }

        public MajorVM()
        {
            SetInclude(x => x.SchoolMdl);
            SetInclude(x => x.StudentMajors);
        }

        protected override void InitVM()
        {
            AllSchoolMdls = DC.Set<SchoolMdl>().GetSelectListItems(Wtm, y => y.SchoolName);
            AllStudentMajorss = DC.Set<StudentMdl>().GetSelectListItems(Wtm, y => y.Name);
            SelectedStudentMajorsIDs = Entity.StudentMajors?.Select(x => x.StudentMdlId.ToString()).ToList();
        }

        public override void DoAdd()
        {
            Entity.StudentMajors = new List<StudentMajor>();
            if (SelectedStudentMajorsIDs != null)
            {
                foreach (var id in SelectedStudentMajorsIDs)
                {
                     StudentMajor middle = new StudentMajor();
                    middle.SetPropertyValue("StudentMdlId", id);
                    Entity.StudentMajors.Add(middle);
                }
            }
           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            Entity.StudentMajors = new List<StudentMajor>();
            if(SelectedStudentMajorsIDs != null )
            {
                 foreach (var item in SelectedStudentMajorsIDs)
                {
                    StudentMajor middle = new StudentMajor();
                    middle.SetPropertyValue("StudentMdlId", item);
                    Entity.StudentMajors.Add(middle);
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
