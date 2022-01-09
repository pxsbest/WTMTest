using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Gensci_LAM.Controllers;
using Gensci_LAM.ViewModel.SchoolVMs;
using Gensci_LAM.Model;
using Gensci_LAM.DataAccess;


namespace Gensci_LAM.Test
{
    [TestClass]
    public class SchoolControllerTest
    {
        private SchoolController _controller;
        private string _seed;

        public SchoolControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<SchoolController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as SchoolListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolVM));

            SchoolVM vm = rv.Model as SchoolVM;
            School v = new School();
			
            v.ID = 93;
            v.SchoolCode = "2";
            v.SchoolName = "SCXR7yNFhpF21MMhTtOzQzpnvW";
            v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PUB;
            v.Remark = "btVKe0U";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<School>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 93);
                Assert.AreEqual(data.SchoolCode, "2");
                Assert.AreEqual(data.SchoolName, "SCXR7yNFhpF21MMhTtOzQzpnvW");
                Assert.AreEqual(data.SchoolType, Gensci_LAM.Model.SchoolTypeEnum.PUB);
                Assert.AreEqual(data.Remark, "btVKe0U");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            School v = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 93;
                v.SchoolCode = "2";
                v.SchoolName = "SCXR7yNFhpF21MMhTtOzQzpnvW";
                v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PUB;
                v.Remark = "btVKe0U";
                context.Set<School>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolVM));

            SchoolVM vm = rv.Model as SchoolVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new School();
            v.ID = vm.Entity.ID;
       		
            v.SchoolCode = "p";
            v.SchoolName = "v1lMqjkbjK";
            v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
            v.Remark = "Ag";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.SchoolCode", "");
            vm.FC.Add("Entity.SchoolName", "");
            vm.FC.Add("Entity.SchoolType", "");
            vm.FC.Add("Entity.Remark", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<School>().Find(v.ID);
 				
                Assert.AreEqual(data.SchoolCode, "p");
                Assert.AreEqual(data.SchoolName, "v1lMqjkbjK");
                Assert.AreEqual(data.SchoolType, Gensci_LAM.Model.SchoolTypeEnum.PRI);
                Assert.AreEqual(data.Remark, "Ag");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            School v = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 93;
                v.SchoolCode = "2";
                v.SchoolName = "SCXR7yNFhpF21MMhTtOzQzpnvW";
                v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PUB;
                v.Remark = "btVKe0U";
                context.Set<School>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolVM));

            SchoolVM vm = rv.Model as SchoolVM;
            v = new School();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<School>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            School v = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 93;
                v.SchoolCode = "2";
                v.SchoolName = "SCXR7yNFhpF21MMhTtOzQzpnvW";
                v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PUB;
                v.Remark = "btVKe0U";
                context.Set<School>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            School v1 = new School();
            School v2 = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 93;
                v1.SchoolCode = "2";
                v1.SchoolName = "SCXR7yNFhpF21MMhTtOzQzpnvW";
                v1.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PUB;
                v1.Remark = "btVKe0U";
                v2.ID = 27;
                v2.SchoolCode = "p";
                v2.SchoolName = "v1lMqjkbjK";
                v2.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v2.Remark = "Ag";
                context.Set<School>().Add(v1);
                context.Set<School>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolBatchVM));

            SchoolBatchVM vm = rv.Model as SchoolBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.LinkedVM.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("LinkedVM.SchoolType", "");
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<School>().Find(v1.ID);
                var data2 = context.Set<School>().Find(v2.ID);
 				
                Assert.AreEqual(data1.SchoolType, Gensci_LAM.Model.SchoolTypeEnum.PRI);
                Assert.AreEqual(data2.SchoolType, Gensci_LAM.Model.SchoolTypeEnum.PRI);
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            School v1 = new School();
            School v2 = new School();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 93;
                v1.SchoolCode = "2";
                v1.SchoolName = "SCXR7yNFhpF21MMhTtOzQzpnvW";
                v1.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PUB;
                v1.Remark = "btVKe0U";
                v2.ID = 27;
                v2.SchoolCode = "p";
                v2.SchoolName = "v1lMqjkbjK";
                v2.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v2.Remark = "Ag";
                context.Set<School>().Add(v1);
                context.Set<School>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(SchoolBatchVM));

            SchoolBatchVM vm = rv.Model as SchoolBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<School>().Find(v1.ID);
                var data2 = context.Set<School>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as SchoolListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
