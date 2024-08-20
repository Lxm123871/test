using FAS202024131135.Models;

namespace FAS202024131135.Data
{
    public class DbInitializer
    {

        public static void Initialize(FAS202024131135Context context)
        {
            //若数据库不存在则创建
            context.Database.EnsureCreated();

            // 找Assets是否已有数据.若已有则不运行下面的添加数据了.
            if (context.Assets.Any())
            {
                return;   //结束下面的程序
            }

            //初始[资产类别]数据
            var categories = new Category[]
            {
            new Category{CategoryName="电脑类",CategoryDescription="电脑类设备"},
            new Category{CategoryName="网络类",CategoryDescription="网络类设备"},
            new Category{CategoryName="空调类",CategoryDescription="空调类设备"},
            new Category{CategoryName="教学类",CategoryDescription="教学类类设备"} 
            };

            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();



            //初始[员工]数据
            var employees = new Employee[]
            {
           
            new Employee{Password="AAaa12345",EmployeeName="张三",Phone="13214217543",Role="Admin",Email="admin@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="李四",Phone="13422217543",Role="Admin",Email="lisi@qq.com",Sector="销售部"},
            new Employee{Password="AAaa12345",EmployeeName="王明",Phone="13214432543",Role="Admin",Email="4234241@qq.com",Sector="技术部"},
            new Employee{Password="AAaa12345",EmployeeName="李五",Phone="13215327543",Role="Admin",Email="53242@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="张伟",Phone="13764317543",Role="Employee",Email="zhangwei@qq.com",Sector="技术部"},
            new Employee{Password="AAaa12345",EmployeeName="梁思",Phone="13216521543",Role="Admin",Email="liangsi@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="李六",Phone="13214297643",Role="Employee",Email="liliu@qq.com",Sector="销售部"},
            new Employee{Password="AAaa12345",EmployeeName="梁溪",Phone="13232317543",Role="Employee",Email="liangxi@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="李青",Phone="13214242432",Role="Employee",Email="liqing@qq.com",Sector="销售部"},
            new Employee{Password="AAaa12345",EmployeeName="牛那",Phone="14324317543",Role="Employee",Email="niuna@qq.com",Sector="技术部"},
            new Employee{Password="AAaa12345",EmployeeName="张西",Phone="13243528543",Role="Employee",Email="zhangxi@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="张帅",Phone="13214454543",Role="Employee",Email="zhangshuai@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="张万",Phone="13878797543",Role="Employee",Email="zhangwan@qq.com",Sector="销售部"},
            new Employee{Password="AAaa12345",EmployeeName="张图",Phone="13218980743",Role="Employee",Email="zhangtu@qq.com",Sector="技术部"},
            new Employee{Password="AAaa12345",EmployeeName="张山",Phone="15364478943",Role="Employee",Email="zhangsan@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="张稳",Phone="13274563532",Role="Employee",Email="zhangwen@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="张表",Phone="13523645756",Role="Employee",Email="zhangbiao@qq.com",Sector="销售部"},
            new Employee{Password="AAaa12345",EmployeeName="张三",Phone="16457584553",Role="Employee",Email="zhangsan@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="刘三",Phone="13543536544",Role="Employee",Email="liusan@qq.com",Sector="技术部"},
            new Employee{Password="AAaa12345",EmployeeName="陈三",Phone="13214245364",Role="Employee",Email="chensan@qq.com",Sector="财务部"},
            new Employee{Password="AAaa12345",EmployeeName="李班",Phone="13234563443",Role="Employee",Email="liban@qq.com",Sector="销售部"},
            new Employee{Password="1234AAaa1234556",EmployeeName="吴笔",Phone="13214647473",Role="Employee",Email="wubi@qq.com",Sector="财务部"}

            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();



            //初始[资产]数据
            var assets = new Asset[]
            {
                new Asset{CategoryID =1,EmployeeID=3,AssetName="笔记本电脑",AssetSize="联想小新pro15",CategoryPhoto="photo1.png",Price=5452.9,Data=DateTime.Parse("2022-09-17"),StorageLocation="中巴软件楼322"},
                new Asset{CategoryID =1,EmployeeID=9,AssetName="平板",AssetSize="小米",CategoryPhoto="photo2.png",Price=5312.9,Data=DateTime.Parse("2022-04-17"),StorageLocation="实训中心331"},
                new Asset{CategoryID =1,EmployeeID=8,AssetName="数模计算机",AssetSize="银色",CategoryPhoto="photo3.png",Price=5452.9,Data=DateTime.Parse("2021-09-07"),StorageLocation="实验楼442"},
                new Asset{CategoryID =2,EmployeeID=3,AssetName="计算机绘图系统",AssetSize="绘图系统",CategoryPhoto="photo4.png",Price=4352.9,Data=DateTime.Parse("2022-12-17"),StorageLocation="中巴软件楼221"},
                new Asset{CategoryID =2,EmployeeID=4,AssetName="中继器",AssetSize="MWE485-Y/YG/YGM/YGS",CategoryPhoto="photo5.png",Price=5432.1,Data=DateTime.Parse("2020-09-19"),StorageLocation="实训中心214"},
                new Asset{CategoryID =2,EmployeeID=5,AssetName="网卡",AssetSize="WY576-F2intel82576芯片",CategoryPhoto="photo6.png",Price=5542.9,Data=DateTime.Parse("2022-08-17"),StorageLocation="实训中心422"},
                new Asset{CategoryID =2,EmployeeID=6,AssetName="交换机",AssetSize="LC-SG1082P",CategoryPhoto="photo7.png",Price=54212.9,Data=DateTime.Parse("2022-09-12"),StorageLocation="实训中心531"},
                new Asset{CategoryID =3,EmployeeID=3,AssetName="空调",AssetSize="格力",CategoryPhoto="photo8.png",Price=5312.9,Data=DateTime.Parse("2022-02-17"),StorageLocation="实验楼211"},
                new Asset{CategoryID =3,EmployeeID=4,AssetName="美的空调",AssetSize="海尔",CategoryPhoto="photo9.png",Price=1152.9,Data=DateTime.Parse("2022-04-17"),StorageLocation="第二教学楼122"},
                new Asset{CategoryID =4,EmployeeID=6,AssetName="黑板",AssetSize="黑色",CategoryPhoto="photo10.png",Price=3252.9,Data=DateTime.Parse("2022-03-20"),StorageLocation="第二教学楼322"}
            };
            foreach (Asset a in assets)
            {
                context.Assets.Add(a);
            }
            context.SaveChanges();


        }


    }
}
