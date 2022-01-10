using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace Gensci_LAM.Model
{

    public enum StudentTypeEnum
    {
        [Display(Name ="好学生")]
        Good,
        [Display(Name ="坏学生")]
        Bad
    }

    public class Student: PersistPoco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int ID { get; set; }

        [Display(Name ="学生编码")]
        [Required(ErrorMessage="{0}是必填项")]
        [RegularExpression("^[0-9]{3,3}$", ErrorMessage = "{0}必须是3位数字")]
        [StringLength(3)]
        public string StudentCode { get; set; }

        [Display(Name = "学生姓名")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string StudentName { get; set; }

        [Display(Name="学生类型")]
        [Required(ErrorMessage="{0}是必填项")]
        public StudentTypeEnum StudentType { get; set; }


        [Display(Name ="备注")]
        public string Remark { get; set; }



        public School School { get; set; }
        public int SchoolId { get; set; }




        //附件
        [Display(Name = "照片")]
        public Guid? PhotoId { get; set; }
        public FileAttachment Photo { get; set; }
    }
}
