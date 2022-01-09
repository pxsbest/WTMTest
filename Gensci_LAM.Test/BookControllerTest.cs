using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Gensci_LAM.Controllers;
using Gensci_LAM.ViewModel.BookVMs;
using Gensci_LAM.Model;
using Gensci_LAM.DataAccess;


namespace Gensci_LAM.Test
{
    [TestClass]
    public class BookControllerTest
    {
        private BookController _controller;
        private string _seed;

        public BookControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<BookController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as BookListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(BookVM));

            BookVM vm = rv.Model as BookVM;
            Book v = new Book();
			
            v.ID = 75;
            v.BookName = "OsRPotIwuqQjnIL5u";
            v.BookType = Gensci_LAM.Model.BookTypeEnum.IT;
            v.Remark = "a3";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Book>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 75);
                Assert.AreEqual(data.BookName, "OsRPotIwuqQjnIL5u");
                Assert.AreEqual(data.BookType, Gensci_LAM.Model.BookTypeEnum.IT);
                Assert.AreEqual(data.Remark, "a3");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Book v = new Book();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 75;
                v.BookName = "OsRPotIwuqQjnIL5u";
                v.BookType = Gensci_LAM.Model.BookTypeEnum.IT;
                v.Remark = "a3";
                context.Set<Book>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(BookVM));

            BookVM vm = rv.Model as BookVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Book();
            v.ID = vm.Entity.ID;
       		
            v.BookName = "fb0fdsdaB7v";
            v.BookType = Gensci_LAM.Model.BookTypeEnum.Math;
            v.Remark = "Bn5YGE5RK9HeHW";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.BookName", "");
            vm.FC.Add("Entity.BookType", "");
            vm.FC.Add("Entity.Remark", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Book>().Find(v.ID);
 				
                Assert.AreEqual(data.BookName, "fb0fdsdaB7v");
                Assert.AreEqual(data.BookType, Gensci_LAM.Model.BookTypeEnum.Math);
                Assert.AreEqual(data.Remark, "Bn5YGE5RK9HeHW");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Book v = new Book();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 75;
                v.BookName = "OsRPotIwuqQjnIL5u";
                v.BookType = Gensci_LAM.Model.BookTypeEnum.IT;
                v.Remark = "a3";
                context.Set<Book>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(BookVM));

            BookVM vm = rv.Model as BookVM;
            v = new Book();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Book>().Find(v.ID);
                Assert.AreEqual(data.IsValid, false);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Book v = new Book();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 75;
                v.BookName = "OsRPotIwuqQjnIL5u";
                v.BookType = Gensci_LAM.Model.BookTypeEnum.IT;
                v.Remark = "a3";
                context.Set<Book>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Book v1 = new Book();
            Book v2 = new Book();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 75;
                v1.BookName = "OsRPotIwuqQjnIL5u";
                v1.BookType = Gensci_LAM.Model.BookTypeEnum.IT;
                v1.Remark = "a3";
                v2.ID = 46;
                v2.BookName = "fb0fdsdaB7v";
                v2.BookType = Gensci_LAM.Model.BookTypeEnum.Math;
                v2.Remark = "Bn5YGE5RK9HeHW";
                context.Set<Book>().Add(v1);
                context.Set<Book>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(BookBatchVM));

            BookBatchVM vm = rv.Model as BookBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.LinkedVM.BookName = "zx9cdbIqNb";
            vm.LinkedVM.BookType = Gensci_LAM.Model.BookTypeEnum.IT;
            vm.LinkedVM.Remark = "HK1zN9ZRjweFJC";
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("LinkedVM.BookName", "");
            vm.FC.Add("LinkedVM.BookType", "");
            vm.FC.Add("LinkedVM.Remark", "");
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Book>().Find(v1.ID);
                var data2 = context.Set<Book>().Find(v2.ID);
 				
                Assert.AreEqual(data1.BookName, "zx9cdbIqNb");
                Assert.AreEqual(data2.BookName, "zx9cdbIqNb");
                Assert.AreEqual(data1.BookType, Gensci_LAM.Model.BookTypeEnum.IT);
                Assert.AreEqual(data2.BookType, Gensci_LAM.Model.BookTypeEnum.IT);
                Assert.AreEqual(data1.Remark, "HK1zN9ZRjweFJC");
                Assert.AreEqual(data2.Remark, "HK1zN9ZRjweFJC");
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Book v1 = new Book();
            Book v2 = new Book();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 75;
                v1.BookName = "OsRPotIwuqQjnIL5u";
                v1.BookType = Gensci_LAM.Model.BookTypeEnum.IT;
                v1.Remark = "a3";
                v2.ID = 46;
                v2.BookName = "fb0fdsdaB7v";
                v2.BookType = Gensci_LAM.Model.BookTypeEnum.Math;
                v2.Remark = "Bn5YGE5RK9HeHW";
                context.Set<Book>().Add(v1);
                context.Set<Book>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(BookBatchVM));

            BookBatchVM vm = rv.Model as BookBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Book>().Find(v1.ID);
                var data2 = context.Set<Book>().Find(v2.ID);
                Assert.AreEqual(data1.IsValid, false);
            Assert.AreEqual(data2.IsValid, false);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as BookListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
