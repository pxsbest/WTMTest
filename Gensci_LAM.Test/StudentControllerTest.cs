using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Gensci_LAM.Controllers;
using Gensci_LAM.ViewModel.StudentVMs;
using Gensci_LAM.Model;
using Gensci_LAM.DataAccess;


namespace Gensci_LAM.Test
{
    [TestClass]
    public class StudentControllerTest
    {
        private StudentController _controller;
        private string _seed;

        public StudentControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<StudentController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as StudentListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(StudentVM));

            StudentVM vm = rv.Model as StudentVM;
            Student v = new Student();
			
            v.ID = 91;
            v.StudentCode = "n";
            v.StudentName = "4gT";
            v.StudentType = Gensci_LAM.Model.StudentTypeEnum.Good;
            v.Remark = "l3Ud8Xvks";
            v.PhotoId = AddFileAttachment();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Student>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 91);
                Assert.AreEqual(data.StudentCode, "n");
                Assert.AreEqual(data.StudentName, "4gT");
                Assert.AreEqual(data.StudentType, Gensci_LAM.Model.StudentTypeEnum.Good);
                Assert.AreEqual(data.Remark, "l3Ud8Xvks");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Student v = new Student();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 91;
                v.StudentCode = "n";
                v.StudentName = "4gT";
                v.StudentType = Gensci_LAM.Model.StudentTypeEnum.Good;
                v.Remark = "l3Ud8Xvks";
                v.PhotoId = AddFileAttachment();
                context.Set<Student>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(StudentVM));

            StudentVM vm = rv.Model as StudentVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Student();
            v.ID = vm.Entity.ID;
       		
            v.StudentCode = "K";
            v.StudentName = "g0Z5I0CCiTpbZt0GqrOSzL6x3RzsUYhcbM7Wg6yhPL7E4";
            v.StudentType = Gensci_LAM.Model.StudentTypeEnum.Bad;
            v.Remark = "j6Nvc6EsYaPj";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.StudentCode", "");
            vm.FC.Add("Entity.StudentName", "");
            vm.FC.Add("Entity.StudentType", "");
            vm.FC.Add("Entity.Remark", "");
            vm.FC.Add("Entity.PhotoId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Student>().Find(v.ID);
 				
                Assert.AreEqual(data.StudentCode, "K");
                Assert.AreEqual(data.StudentName, "g0Z5I0CCiTpbZt0GqrOSzL6x3RzsUYhcbM7Wg6yhPL7E4");
                Assert.AreEqual(data.StudentType, Gensci_LAM.Model.StudentTypeEnum.Bad);
                Assert.AreEqual(data.Remark, "j6Nvc6EsYaPj");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Student v = new Student();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 91;
                v.StudentCode = "n";
                v.StudentName = "4gT";
                v.StudentType = Gensci_LAM.Model.StudentTypeEnum.Good;
                v.Remark = "l3Ud8Xvks";
                v.PhotoId = AddFileAttachment();
                context.Set<Student>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(StudentVM));

            StudentVM vm = rv.Model as StudentVM;
            v = new Student();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Student>().Find(v.ID);
                Assert.AreEqual(data.IsValid, false);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Student v = new Student();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 91;
                v.StudentCode = "n";
                v.StudentName = "4gT";
                v.StudentType = Gensci_LAM.Model.StudentTypeEnum.Good;
                v.Remark = "l3Ud8Xvks";
                v.PhotoId = AddFileAttachment();
                context.Set<Student>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Student v1 = new Student();
            Student v2 = new Student();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 91;
                v1.StudentCode = "n";
                v1.StudentName = "4gT";
                v1.StudentType = Gensci_LAM.Model.StudentTypeEnum.Good;
                v1.Remark = "l3Ud8Xvks";
                v1.PhotoId = AddFileAttachment();
                v2.ID = 46;
                v2.StudentCode = "K";
                v2.StudentName = "g0Z5I0CCiTpbZt0GqrOSzL6x3RzsUYhcbM7Wg6yhPL7E4";
                v2.StudentType = Gensci_LAM.Model.StudentTypeEnum.Bad;
                v2.Remark = "j6Nvc6EsYaPj";
                v2.PhotoId = v1.PhotoId; 
                context.Set<Student>().Add(v1);
                context.Set<Student>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(StudentBatchVM));

            StudentBatchVM vm = rv.Model as StudentBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.LinkedVM.StudentType = Gensci_LAM.Model.StudentTypeEnum.Bad;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("LinkedVM.StudentType", "");
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Student>().Find(v1.ID);
                var data2 = context.Set<Student>().Find(v2.ID);
 				
                Assert.AreEqual(data1.StudentType, Gensci_LAM.Model.StudentTypeEnum.Bad);
                Assert.AreEqual(data2.StudentType, Gensci_LAM.Model.StudentTypeEnum.Bad);
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Student v1 = new Student();
            Student v2 = new Student();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 91;
                v1.StudentCode = "n";
                v1.StudentName = "4gT";
                v1.StudentType = Gensci_LAM.Model.StudentTypeEnum.Good;
                v1.Remark = "l3Ud8Xvks";
                v1.PhotoId = AddFileAttachment();
                v2.ID = 46;
                v2.StudentCode = "K";
                v2.StudentName = "g0Z5I0CCiTpbZt0GqrOSzL6x3RzsUYhcbM7Wg6yhPL7E4";
                v2.StudentType = Gensci_LAM.Model.StudentTypeEnum.Bad;
                v2.Remark = "j6Nvc6EsYaPj";
                v2.PhotoId = v1.PhotoId; 
                context.Set<Student>().Add(v1);
                context.Set<Student>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(StudentBatchVM));

            StudentBatchVM vm = rv.Model as StudentBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Student>().Find(v1.ID);
                var data2 = context.Set<Student>().Find(v2.ID);
                Assert.AreEqual(data1.IsValid, false);
            Assert.AreEqual(data2.IsValid, false);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as StudentListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "Ev";
                v.FileExt = "CQin";
                v.Path = "VozHdZ1ovN";
                v.Length = 23;
                v.UploadTime = DateTime.Parse("2021-01-07 14:58:07");
                v.SaveMode = "XezkKKVxXlAE";
                v.ExtraInfo = "aUrDHfQvKbKynaiZF2Z";
                v.HandlerInfo = "ro";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
