using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAS202024131135.Models;

public partial class Category
{
   /* [DatabaseGenerated(DatabaseGeneratedOption.None)]*/
    [Display(Name = "类别代号")]
    public int CategoryID { get; set; }

    [Display(Name = "类别名称")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(50, ErrorMessage = "编号必须是2 - 50个字符", MinimumLength = 2)]
    public string CategoryName { get; set; } = null!;
    
    [Display(Name = "类别说明")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(400, ErrorMessage = "编号必须是2 - 400个字符", MinimumLength = 2)]
    public string? CategoryDescription { get; set; }

    
    //导航属性
    [ValidateNever]
    public ICollection<Asset> Assets { get; set; }
}
