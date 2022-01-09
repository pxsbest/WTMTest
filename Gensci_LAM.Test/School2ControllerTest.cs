using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Gensci_LAM.Controllers;
using Gensci_LAM.ViewModel.School2VMs;
using Gensci_LAM.Model;
using Gensci_LAM.DataAccess;


namespace Gensci_LAM.Test
{
    [TestClass]
    public class School2ControllerTest
    {
        private School2Controller _controller;
        private string _seed;

        public School2ControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<School2Controller>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as School2ListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(School2VM));

            School2VM vm = rv.Model as School2VM;
            School2 v = new School2();
			
            v.SchoolName = "SOAwdloN0LQ3DZC";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<School2>().Find(v.ID);
				
                Assert.AreEqual(data.SchoolName, "SOAwdloN0LQ3DZC");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            School2 v = new School2();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.SchoolName = "SOAwdloN0LQ3DZC";
                context.Set<School2>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(School2VM));

            School2VM vm = rv.Model as School2VM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new School2();
            v.ID = vm.Entity.ID;
       		
            v.SchoolName = "r6hY";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.SchoolName", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<School2>().Find(v.ID);
 				
                Assert.AreEqual(data.SchoolName, "r6hY");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            School2 v = new School2();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.SchoolName = "SOAwdloN0LQ3DZC";
                context.Set<School2>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(School2VM));

            School2VM vm = rv.Model as School2VM;
            v = new School2();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<School2>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            School2 v = new School2();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.SchoolName = "SOAwdloN0LQ3DZC";
                context.Set<School2>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            School2 v1 = new School2();
            School2 v2 = new School2();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SchoolName = "SOAwdloN0LQ3DZC";
                v2.SchoolName = "r6hY";
                context.Set<School2>().Add(v1);
                context.Set<School2>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(School2BatchVM));

            School2BatchVM vm = rv.Model as School2BatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.LinkedVM.SchoolName = "IQgTnyaAOrA9u";
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("LinkedVM.SchoolName", "");
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<School2>().Find(v1.ID);
                var data2 = context.Set<School2>().Find(v2.ID);
 				
                Assert.AreEqual(data1.SchoolName, "IQgTnyaAOrA9u");
                Assert.AreEqual(data2.SchoolName, "IQgTnyaAOrA9u");
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            School2 v1 = new School2();
            School2 v2 = new School2();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SchoolName = "SOAwdloN0LQ3DZC";
                v2.SchoolName = "r6hY";
                context.Set<School2>().Add(v1);
                context.Set<School2>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(School2BatchVM));

            School2BatchVM vm = rv.Model as School2BatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<School2>().Find(v1.ID);
                var data2 = context.Set<School2>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as School2ListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
