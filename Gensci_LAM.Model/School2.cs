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
    public class School2 : BasePoco
    {
        [Display(Name = "学校名称")]
        public string SchoolName { get; set; }

        /// <summary>
        /// 附件表
        /// </summary>
        public List<School2Photo> Photos { get; set; }
    }
    //继承ISubFile的附件子表
    public class School2Photo : TopBasePoco, ISubFile
    {
        public Guid School2Id { get; set; }
        public School2 School2 { get; set; }
        //ISubFile定义的字段
        public Guid FileId { get; set; }
        public FileAttachment File { get; set; }
        public int Order { get; set; }
    }

}
