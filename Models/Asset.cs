using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAS202024131135.Models;


public partial class Asset
{
   /* [DatabaseGenerated(DatabaseGeneratedOption.None)]*/
    [Display(Name = "资产编号")] 
    public int AssetID { get; set; }

    [Display(Name = "资产名称")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(50, ErrorMessage = "编号必须是2 - 50个字符", MinimumLength = 2)]
    public string AssetName { get; set; } = null!;
   
    [Display(Name = "资产规格")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(50, ErrorMessage = "编号必须是2 - 50个字符", MinimumLength = 2)]
    public string AssetSize { get; set; } = null!;
    
    [Display(Name = "资产照片")]
    [StringLength(50, ErrorMessage = "编号必须是2 - 50个字符", MinimumLength = 2)]
    public string? CategoryPhoto { get; set; }
    
    [Display(Name = "价格")]
    [Required(ErrorMessage = "必须填写")]
    [Range(0, double.MaxValue, ErrorMessage = "价格必须大于等于0")]
    public double Price { get; set; }
    
    [Display(Name = "购入日期")]
    [Required(ErrorMessage = "必须填写")]
    [DataType(DataType.Date)]
    public DateTime Data { get; set; }
    
    [Display(Name = "存放位置")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(300, ErrorMessage = "编号必须是2 - 300个字符", MinimumLength = 2)]
    public string StorageLocation { get; set; } = null!;
    
    [Display(Name = "资产类别")]
    public int CategoryID { get; set; }
    
    [Display(Name = "资产保管人")]
    public int EmployeeID { get; set; }


    //本表外键的关联表
    //导航属性
    [ValidateNever]
    public Category Category { get; set; }
    //导航属性
    [ValidateNever]
    public Employee Employee { get; set; }

   
}
