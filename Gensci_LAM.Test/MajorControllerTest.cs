using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Gensci_LAM.Controllers;
using Gensci_LAM.ViewModel.MajorVMs;
using Gensci_LAM.Model;
using Gensci_LAM.DataAccess;


namespace Gensci_LAM.Test
{
    [TestClass]
    public class MajorControllerTest
    {
        private MajorController _controller;
        private string _seed;

        public MajorControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<MajorController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as MajorListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(MajorVM));

            MajorVM vm = rv.Model as MajorVM;
            Major v = new Major();
			
            v.MajorCode = "16WUEXVV3GBNa5LiQ";
            v.MajorName = "G7pbyc5ZdRTveupBB";
            v.Remark = "0fO5dnb";
            v.SchoolMdlId = AddSchoolMdl();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Major>().Find(v.ID);
				
                Assert.AreEqual(data.MajorCode, "16WUEXVV3GBNa5LiQ");
                Assert.AreEqual(data.MajorName, "G7pbyc5ZdRTveupBB");
                Assert.AreEqual(data.Remark, "0fO5dnb");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Major v = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.MajorCode = "16WUEXVV3GBNa5LiQ";
                v.MajorName = "G7pbyc5ZdRTveupBB";
                v.Remark = "0fO5dnb";
                v.SchoolMdlId = AddSchoolMdl();
                context.Set<Major>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(MajorVM));

            MajorVM vm = rv.Model as MajorVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Major();
            v.ID = vm.Entity.ID;
       		
            v.MajorCode = "DX5AGwRy17erzm53";
            v.MajorName = "RCcCcFyWRJfDokpeZjruW84SUchPXZvrteVbIgfz525";
            v.Remark = "yOg9I6aXsb5Ht8C";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.MajorCode", "");
            vm.FC.Add("Entity.MajorName", "");
            vm.FC.Add("Entity.Remark", "");
            vm.FC.Add("Entity.SchoolMdlId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Major>().Find(v.ID);
 				
                Assert.AreEqual(data.MajorCode, "DX5AGwRy17erzm53");
                Assert.AreEqual(data.MajorName, "RCcCcFyWRJfDokpeZjruW84SUchPXZvrteVbIgfz525");
                Assert.AreEqual(data.Remark, "yOg9I6aXsb5Ht8C");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Major v = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.MajorCode = "16WUEXVV3GBNa5LiQ";
                v.MajorName = "G7pbyc5ZdRTveupBB";
                v.Remark = "0fO5dnb";
                v.SchoolMdlId = AddSchoolMdl();
                context.Set<Major>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(MajorVM));

            MajorVM vm = rv.Model as MajorVM;
            v = new Major();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Major>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Major v = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.MajorCode = "16WUEXVV3GBNa5LiQ";
                v.MajorName = "G7pbyc5ZdRTveupBB";
                v.Remark = "0fO5dnb";
                v.SchoolMdlId = AddSchoolMdl();
                context.Set<Major>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Major v1 = new Major();
            Major v2 = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.MajorCode = "16WUEXVV3GBNa5LiQ";
                v1.MajorName = "G7pbyc5ZdRTveupBB";
                v1.Remark = "0fO5dnb";
                v1.SchoolMdlId = AddSchoolMdl();
                v2.MajorCode = "DX5AGwRy17erzm53";
                v2.MajorName = "RCcCcFyWRJfDokpeZjruW84SUchPXZvrteVbIgfz525";
                v2.Remark = "yOg9I6aXsb5Ht8C";
                v2.SchoolMdlId = v1.SchoolMdlId; 
                context.Set<Major>().Add(v1);
                context.Set<Major>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(MajorBatchVM));

            MajorBatchVM vm = rv.Model as MajorBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Major>().Find(v1.ID);
                var data2 = context.Set<Major>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Major v1 = new Major();
            Major v2 = new Major();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.MajorCode = "16WUEXVV3GBNa5LiQ";
                v1.MajorName = "G7pbyc5ZdRTveupBB";
                v1.Remark = "0fO5dnb";
                v1.SchoolMdlId = AddSchoolMdl();
                v2.MajorCode = "DX5AGwRy17erzm53";
                v2.MajorName = "RCcCcFyWRJfDokpeZjruW84SUchPXZvrteVbIgfz525";
                v2.Remark = "yOg9I6aXsb5Ht8C";
                v2.SchoolMdlId = v1.SchoolMdlId; 
                context.Set<Major>().Add(v1);
                context.Set<Major>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(MajorBatchVM));

            MajorBatchVM vm = rv.Model as MajorBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Major>().Find(v1.ID);
                var data2 = context.Set<Major>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as MajorListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddSchoolMdl()
        {
            SchoolMdl v = new SchoolMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.SchoolCode = "x4";
                v.SchoolName = "jtLzdRIz1B4GbIQt";
                v.SchoolType = Gensci_LAM.Model.SchoolTypeEnum.PRI;
                v.Remark = "9DG73RH";
                context.Set<SchoolMdl>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
