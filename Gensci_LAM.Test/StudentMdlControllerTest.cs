using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using Gensci_LAM.Controllers;
using Gensci_LAM.ViewModel.StudentMdlVMs;
using Gensci_LAM.Model;
using Gensci_LAM.DataAccess;


namespace Gensci_LAM.Test
{
    [TestClass]
    public class StudentMdlControllerTest
    {
        private StudentMdlController _controller;
        private string _seed;

        public StudentMdlControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<StudentMdlController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as StudentMdlListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(StudentMdlVM));

            StudentMdlVM vm = rv.Model as StudentMdlVM;
            StudentMdl v = new StudentMdl();
			
            v.LoginName = "ShebcoyNNJqR0TLDgUj4U2Begx";
            v.Password = "MDRGLcq5th3dJshNSkyvfVI";
            v.Name = "CLDef3WSO53cbkyT264kEzhaxLnsDzjyZ859sXGRuG3fk";
            v.CellPhone = "Eu7LICFW65q";
            v.ZipCode = "ebcXnLAg4BRFaK";
            v.EnRollDate = DateTime.Parse("2021-03-23 11:11:14");
            v.PhotoId = AddFileAttachment();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<StudentMdl>().Find(v.ID);
				
                Assert.AreEqual(data.LoginName, "ShebcoyNNJqR0TLDgUj4U2Begx");
                Assert.AreEqual(data.Password, "MDRGLcq5th3dJshNSkyvfVI");
                Assert.AreEqual(data.Name, "CLDef3WSO53cbkyT264kEzhaxLnsDzjyZ859sXGRuG3fk");
                Assert.AreEqual(data.CellPhone, "Eu7LICFW65q");
                Assert.AreEqual(data.ZipCode, "ebcXnLAg4BRFaK");
                Assert.AreEqual(data.EnRollDate, DateTime.Parse("2021-03-23 11:11:14"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            StudentMdl v = new StudentMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.LoginName = "ShebcoyNNJqR0TLDgUj4U2Begx";
                v.Password = "MDRGLcq5th3dJshNSkyvfVI";
                v.Name = "CLDef3WSO53cbkyT264kEzhaxLnsDzjyZ859sXGRuG3fk";
                v.CellPhone = "Eu7LICFW65q";
                v.ZipCode = "ebcXnLAg4BRFaK";
                v.EnRollDate = DateTime.Parse("2021-03-23 11:11:14");
                v.PhotoId = AddFileAttachment();
                context.Set<StudentMdl>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(StudentMdlVM));

            StudentMdlVM vm = rv.Model as StudentMdlVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new StudentMdl();
            v.ID = vm.Entity.ID;
       		
            v.LoginName = "CAljhllLWR";
            v.Password = "FTPWkq8Vv";
            v.Name = "Di19dUlGwbmHWZAOPhkM3ryhED";
            v.CellPhone = "6kHmx3GU8RNJK9nK";
            v.ZipCode = "dfSUBZ";
            v.EnRollDate = DateTime.Parse("2021-08-15 11:11:14");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LoginName", "");
            vm.FC.Add("Entity.Password", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.CellPhone", "");
            vm.FC.Add("Entity.ZipCode", "");
            vm.FC.Add("Entity.EnRollDate", "");
            vm.FC.Add("Entity.PhotoId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<StudentMdl>().Find(v.ID);
 				
                Assert.AreEqual(data.LoginName, "CAljhllLWR");
                Assert.AreEqual(data.Password, "FTPWkq8Vv");
                Assert.AreEqual(data.Name, "Di19dUlGwbmHWZAOPhkM3ryhED");
                Assert.AreEqual(data.CellPhone, "6kHmx3GU8RNJK9nK");
                Assert.AreEqual(data.ZipCode, "dfSUBZ");
                Assert.AreEqual(data.EnRollDate, DateTime.Parse("2021-08-15 11:11:14"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            StudentMdl v = new StudentMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.LoginName = "ShebcoyNNJqR0TLDgUj4U2Begx";
                v.Password = "MDRGLcq5th3dJshNSkyvfVI";
                v.Name = "CLDef3WSO53cbkyT264kEzhaxLnsDzjyZ859sXGRuG3fk";
                v.CellPhone = "Eu7LICFW65q";
                v.ZipCode = "ebcXnLAg4BRFaK";
                v.EnRollDate = DateTime.Parse("2021-03-23 11:11:14");
                v.PhotoId = AddFileAttachment();
                context.Set<StudentMdl>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(StudentMdlVM));

            StudentMdlVM vm = rv.Model as StudentMdlVM;
            v = new StudentMdl();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<StudentMdl>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            StudentMdl v = new StudentMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.LoginName = "ShebcoyNNJqR0TLDgUj4U2Begx";
                v.Password = "MDRGLcq5th3dJshNSkyvfVI";
                v.Name = "CLDef3WSO53cbkyT264kEzhaxLnsDzjyZ859sXGRuG3fk";
                v.CellPhone = "Eu7LICFW65q";
                v.ZipCode = "ebcXnLAg4BRFaK";
                v.EnRollDate = DateTime.Parse("2021-03-23 11:11:14");
                v.PhotoId = AddFileAttachment();
                context.Set<StudentMdl>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            StudentMdl v1 = new StudentMdl();
            StudentMdl v2 = new StudentMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LoginName = "ShebcoyNNJqR0TLDgUj4U2Begx";
                v1.Password = "MDRGLcq5th3dJshNSkyvfVI";
                v1.Name = "CLDef3WSO53cbkyT264kEzhaxLnsDzjyZ859sXGRuG3fk";
                v1.CellPhone = "Eu7LICFW65q";
                v1.ZipCode = "ebcXnLAg4BRFaK";
                v1.EnRollDate = DateTime.Parse("2021-03-23 11:11:14");
                v1.PhotoId = AddFileAttachment();
                v2.LoginName = "CAljhllLWR";
                v2.Password = "FTPWkq8Vv";
                v2.Name = "Di19dUlGwbmHWZAOPhkM3ryhED";
                v2.CellPhone = "6kHmx3GU8RNJK9nK";
                v2.ZipCode = "dfSUBZ";
                v2.EnRollDate = DateTime.Parse("2021-08-15 11:11:14");
                v2.PhotoId = v1.PhotoId; 
                context.Set<StudentMdl>().Add(v1);
                context.Set<StudentMdl>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(StudentMdlBatchVM));

            StudentMdlBatchVM vm = rv.Model as StudentMdlBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<StudentMdl>().Find(v1.ID);
                var data2 = context.Set<StudentMdl>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            StudentMdl v1 = new StudentMdl();
            StudentMdl v2 = new StudentMdl();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LoginName = "ShebcoyNNJqR0TLDgUj4U2Begx";
                v1.Password = "MDRGLcq5th3dJshNSkyvfVI";
                v1.Name = "CLDef3WSO53cbkyT264kEzhaxLnsDzjyZ859sXGRuG3fk";
                v1.CellPhone = "Eu7LICFW65q";
                v1.ZipCode = "ebcXnLAg4BRFaK";
                v1.EnRollDate = DateTime.Parse("2021-03-23 11:11:14");
                v1.PhotoId = AddFileAttachment();
                v2.LoginName = "CAljhllLWR";
                v2.Password = "FTPWkq8Vv";
                v2.Name = "Di19dUlGwbmHWZAOPhkM3ryhED";
                v2.CellPhone = "6kHmx3GU8RNJK9nK";
                v2.ZipCode = "dfSUBZ";
                v2.EnRollDate = DateTime.Parse("2021-08-15 11:11:14");
                v2.PhotoId = v1.PhotoId; 
                context.Set<StudentMdl>().Add(v1);
                context.Set<StudentMdl>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(StudentMdlBatchVM));

            StudentMdlBatchVM vm = rv.Model as StudentMdlBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<StudentMdl>().Find(v1.ID);
                var data2 = context.Set<StudentMdl>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as StudentMdlListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "7rOcXv1tB8g";
                v.FileExt = "VXO";
                v.Path = "9H4dtyN";
                v.Length = 26;
                v.UploadTime = DateTime.Parse("2022-04-10 11:11:14");
                v.SaveMode = "cpt1";
                v.ExtraInfo = "pKeyfmGUGJFRnYbm";
                v.HandlerInfo = "tEBZaGJumeR7UIh0EL";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
