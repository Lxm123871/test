using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAS202024131135.Models;

public partial class Employee
{
    /*[DatabaseGenerated(DatabaseGeneratedOption.None)]*/
    [Display(Name = "员工编号")]
    /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/ //指定为自增长属性
    public int EmployeeID{ get; set; }

    [Display(Name = "密码")]
    [Required(ErrorMessage = "必须填写")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "密码必须包含大小写字母和数字并且最低6位数起。")]
    [StringLength(100)]
    public string Password { get; set; } = null!;
    
    [Display(Name = "姓名")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(50, ErrorMessage = "编号必须是2 - 50个字符", MinimumLength = 2)]
    public string EmployeeName { get; set; } = null!;
    
    [Display(Name = "联络电话")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(30, ErrorMessage = "编号必须是2 - 30个字符", MinimumLength = 2)]
    public string Phone { get; set; } = null!;
    
    [Display(Name = "角色")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(20, ErrorMessage = "编号必须是2 - 20个字符", MinimumLength = 2)]
    public string Role { get; set; } = null!;

    
    [EmailAddress(ErrorMessage = "邮箱格式有误!")]
    [Display(Name = "Email")]
    [Required(ErrorMessage = "必须填写并且按照email格式填写")]
    [StringLength(100, ErrorMessage = "编号必须是2 - 100个字符", MinimumLength = 2)]
    public string Email { get; set; } = null!;
    
    [Display(Name = "部门")]
    [Required(ErrorMessage = "必须填写")]
    [StringLength(20, ErrorMessage = "编号必须是2 - 20个字符", MinimumLength = 2)]
    public string Sector { get; set; }

    // 被Assets连接
    //导航属性
    [ValidateNever]
    public ICollection<Asset> Assets { get; set; }


}
