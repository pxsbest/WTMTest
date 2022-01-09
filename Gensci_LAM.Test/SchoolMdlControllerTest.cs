using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Gensci_LAM.Controllers;
using Gensci_LAM.ViewModel.SchoolMdlVMs;
using Gensci_LAM.Model;
using Gensci_LAM.DataAccess;


namespace Gensci_LAM.Test
{
    [TestClass]
    public class SchoolMdlControllerTest
    {
        private SchoolMdlController _controller;
        private string _seed;

        public SchoolMdlControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<SchoolMdlController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as SchoolMdlListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolMdlVM));

            SchoolMdlVM vm = rv.Model as SchoolMdlVM;
            SchoolMdl v = new SchoolMdl();
			
            v.SchoolCode = "tK";
            v.SchoolName = "6wKeIF64kq5XzkSGkwTzPenM3X37H0uFvPw8qG";
            v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
            v.Remark = "jOnlvKhJK2";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<SchoolMdl>().Find(v.ID);
				
                Assert.AreEqual(data.SchoolCode, "tK");
                Assert.AreEqual(data.SchoolName, "6wKeIF64kq5XzkSGkwTzPenM3X37H0uFvPw8qG");
                Assert.AreEqual(data.SchoolType, Gensci_LAM.Model.SchoolTypeEnum.PRI);
                Assert.AreEqual(data.Remark, "jOnlvKhJK2");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            SchoolMdl v = new SchoolMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.SchoolCode = "tK";
                v.SchoolName = "6wKeIF64kq5XzkSGkwTzPenM3X37H0uFvPw8qG";
                v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v.Remark = "jOnlvKhJK2";
                context.Set<SchoolMdl>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolMdlVM));

            SchoolMdlVM vm = rv.Model as SchoolMdlVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new SchoolMdl();
            v.ID = vm.Entity.ID;
       		
            v.SchoolCode = "TI";
            v.SchoolName = "bChV20H4JuNTvklox3dUPbkbd";
            v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
            v.Remark = "RDmjOPsy6DriBioLYDx";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.SchoolCode", "");
            vm.FC.Add("Entity.SchoolName", "");
            vm.FC.Add("Entity.SchoolType", "");
            vm.FC.Add("Entity.Remark", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<SchoolMdl>().Find(v.ID);
 				
                Assert.AreEqual(data.SchoolCode, "TI");
                Assert.AreEqual(data.SchoolName, "bChV20H4JuNTvklox3dUPbkbd");
                Assert.AreEqual(data.SchoolType, Gensci_LAM.Model.SchoolTypeEnum.PRI);
                Assert.AreEqual(data.Remark, "RDmjOPsy6DriBioLYDx");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            SchoolMdl v = new SchoolMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.SchoolCode = "tK";
                v.SchoolName = "6wKeIF64kq5XzkSGkwTzPenM3X37H0uFvPw8qG";
                v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v.Remark = "jOnlvKhJK2";
                context.Set<SchoolMdl>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolMdlVM));

            SchoolMdlVM vm = rv.Model as SchoolMdlVM;
            v = new SchoolMdl();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<SchoolMdl>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            SchoolMdl v = new SchoolMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.SchoolCode = "tK";
                v.SchoolName = "6wKeIF64kq5XzkSGkwTzPenM3X37H0uFvPw8qG";
                v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v.Remark = "jOnlvKhJK2";
                context.Set<SchoolMdl>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            SchoolMdl v1 = new SchoolMdl();
            SchoolMdl v2 = new SchoolMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SchoolCode = "tK";
                v1.SchoolName = "6wKeIF64kq5XzkSGkwTzPenM3X37H0uFvPw8qG";
                v1.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v1.Remark = "jOnlvKhJK2";
                v2.SchoolCode = "TI";
                v2.SchoolName = "bChV20H4JuNTvklox3dUPbkbd";
                v2.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v2.Remark = "RDmjOPsy6DriBioLYDx";
                context.Set<SchoolMdl>().Add(v1);
                context.Set<SchoolMdl>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolMdlBatchVM));

            SchoolMdlBatchVM vm = rv.Model as SchoolMdlBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<SchoolMdl>().Find(v1.ID);
                var data2 = context.Set<SchoolMdl>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            SchoolMdl v1 = new SchoolMdl();
            SchoolMdl v2 = new SchoolMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SchoolCode = "tK";
                v1.SchoolName = "6wKeIF64kq5XzkSGkwTzPenM3X37H0uFvPw8qG";
                v1.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v1.Remark = "jOnlvKhJK2";
                v2.SchoolCode = "TI";
                v2.SchoolName = "bChV20H4JuNTvklox3dUPbkbd";
                v2.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v2.Remark = "RDmjOPsy6DriBioLYDx";
                context.Set<SchoolMdl>().Add(v1);
                context.Set<SchoolMdl>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolMdlBatchVM));

            SchoolMdlBatchVM vm = rv.Model as SchoolMdlBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<SchoolMdl>().Find(v1.ID);
                var data2 = context.Set<SchoolMdl>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as SchoolMdlListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
