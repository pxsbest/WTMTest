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

    public enum BookTypeEnum
    {
        [Display(Name = "历史")]
        History,
        [Display(Name = "数学")]
        Math,
        [Display(Name = "计算机")]
        IT
    }


    public class Book : PersistPoco
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int ID { get; set; }

        [Display(Name = "书名")]
        [Required(ErrorMessage = "{0}是必填项")]
        //[RegularExpression("^[0-9]{3,3}$", ErrorMessage = "{0}必须是3位数字")]
        [StringLength(8,ErrorMessage="Validate.{0}stringmax{1}")]

        public string BookName { get; set; }


        [Display(Name ="书目分类")]
        [Required(ErrorMessage="{0}是必填项")]
        public BookTypeEnum BookType { get; set; }

        [Display(Name ="备注")]
        public string Remark { get; set; }


        //定义附件表
        [Display(Name ="照片")]
        public List<BookPhoto> Photos { get; set; }

    }


    public class BookPhoto : TopBasePoco, ISubFile
    {
        public int BookID { get; set; }
        public Book Book { get; set; }

        public Guid FileId { get; set; }

        public FileAttachment File { get; set; }
        public int Order { get; set; }


    }
}
