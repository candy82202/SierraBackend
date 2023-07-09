主頁面

[V] _Layout & Index 頁面完成 (新增Logo)
	--更改 content的css位址
	--更改 Script 的 JS位址 bootstrap位址
    - 首頁預覽頁面，icon小圖示 (刪除message icon)

[V] - search bar 使用 autocomplete 可以連結到子頁面
    - 連結呈現的樣式修改至下方呈現
[working on] - search bar icon 位置調整

[V] HomeController
	-- 新增頁面  FormEdit / FormLayout / UserProfile / DataTable
    -- 修改頁面 Contact / 

[working on] 創建EFModels
			 --新增 各自的Controller (View Models)			
			 --在自己的Index.cshtml @{ViewBag.Title = "Index";}下面加上
					<main id="main" class="main">
                        <div class="pagetitle">
                            <h1>Form Elements</h1>
                            <nav>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="@Url.Action("Index","Home")">首頁</a></li>
                                    <li class="breadcrumb-item">   這裡是____管理  </li>
                                    <li class="breadcrumb-item active">  這裡是____頁面  </li>
                                </ol>
                            </nav>
                        </div>
                        <!-- End Page Title -->
                             <section class="section">
                                    這裡是頁面的內容
                             </section>
                        </main>
            -- table 部分想要調整可以加上這幾個class <table class="table table-striped table-hover">
            -- 如果table再加上class="datatable"就可以變成datatable版本

[V] - 修改DataTable 的模板樣式，更改成中文版本

[V] - 加裝sweet alert delete的部分。
    - 版本 4.7.2 

[V]- 資料庫使用Sierras 0629 
        - (0704 更新資料庫)
        - 資料庫 Members 新增兩個表格
        - (0706 更新資料庫)
        - 資料庫新增欄位 Desserts ScheduledPublishDate && Delete MemberCoupon Status
        - 

[V] - 首頁 新上架的5筆甜點呈現 熱銷 3 款甜點呈現

[V] - 針對404錯誤訊息畫面 修改web.config畫面，圖片更新完成
    - 跟其他錯誤頁面的圖片更新畫面
=========================================================
前台頁面 部分
[V] - 新增前台商品上架的頁面 LayoutFront , Sierras.cshtml , NewDesserts.cshtml
    - 新增師資下拉清單
    - 新增Logo
    - 字體顏色調整
    - 顯示CurrentTime去比對
    - 首頁輪播照片
    - 3款 熱門銷售甜點
=========================================================
Desserts 部分

[V] - 全選 checkbox 的變更事件監聽器 (Desserts & Categories 的 Index 頁面 抓到分頁數量的checkbox )
    - AutoComplete 甜點清單頁面

[V] - Index 更改成 Tags 分頁方式切換 上架5筆 && 全部下架

[V] - 單筆上,下架按鈕
    - Checkbox Dessert Index 一鍵上,下架
    - 上,下架通知新增SweetAlert方式

[V] - DessertTags Index , Edit 頁面 (更換成Cart元件)

[V] - Add DessertIndexPartVM & DessertExts
    - HomeController Add RecentDesserts method
    - Partial View RecentUpDesserts
    - Partial View All DownDesserts

[V] - Desserts Index Create Update use card style

[V] - 新增多張圖顯示
    - 編輯照片更改成圖示 <img>

[V] - AJAX 甜點清單頁面

[working on] - 編輯照片新增欄位(簡易編輯)
             - 


==========================================================
Member 部分

[V] - 因Permission表拆成4個表，
      刪除EFModels裡所有檔案
      刪除連線字串
      刪除相關控制器和檢視(Employees和Permissions)

[V] - 重建EFModels
      重建相關控制器和檢視

[V] - 新增VM
[V] - 修改檢視
[V] - 完成 EmployeesController 中的 Index(), Create()
      新增 EmployeeIndexVM, EmployeeCreateVM, EmployeeExts
      新增 Views/Employees/Create.cshtml和Index.cshtml

[V] - 完成Employees/Edit(含view page)
      (修改複合主鍵時遇到錯誤：System.InvalidOperationException: 'The property 'RoleId' is part of the object's key information and cannot be modified. '
      解決方法：先用變數保存原本資料，再刪除原本資料，最後再新增一筆新資料。)

[V] - 修改資料表結構(員工刪除關聯)
[V] - complete Employee/Delete(including view page)

[V] - create & complete MemberIndexVM.cs
    - create & complete MemberExts.cs
    - modify Index()
    - modify Index.cshtml
[V] - modify Members/Index（修改MemberIndexVM）
[V] - complete Employee CRUD
[V] - bug fix(Employee/Delete取消也會刪除)
    - bug fix(帳號唯一不會跳錯)
[V] - create & complete Login() and Logout() in EmployeesController
[V] - create & complete LoginVM.cs and Login.cshtml
[V] - create /Models/Infra/Result.cs & HashUtility.cs
      add salt to Web.config 
[V] - 角色&權限
    - create "Filters" folder & CustomAuthorizeAttribute.cs
    - create _Unauthorized.cshtml
    - modify Global.asax
    - create Shared/_Unauthorized.cshtml
[V] - fix little bug in EmployeeCreateVM.cs & Employees/Edit.cshtml
[working on] - profile user name
[working on] - members criteria

==========================================================
Lesson 部分

[V]將sa5的密碼修改和Web.config相同
[V]初步修改Lessons、LessonCategories CRUD版面調整

[V]index進階搜尋 OK

===========0704==============
[V]Lessons Create基本完成
===========0705==============
[V]Lessons Edit 基本完成
[V]增加時間判定在Create、Edit中開課時間不能是過去
[V]Index 進階搜尋可以清空資料
[working on]Lessons EditImage
===========0706=====================
[V]Lessons 編輯照片
[working on]將刪除變成上架或已開課
==================================================================
[working on]完善Course CRUD、Courses的多張照片上傳和修改
[working on]CRUD頁面需要補上<main> ⇒ 版型調整、table部分更改成datatable(有需要使用的)


















==========================================================
Teacher 部分
[working on]建立三層式
-add TeacherDto class
[v]改完版型
[v]建立VM classes
[v]寫擴充方法轉vm
[v]index頁轉vm
[v]修改TeacherIndexVM，增加display
[v]TeacherIndexVM描述截斷，留前幾個字
[v]修改TeacherCreateVM
[v]上傳檔案
[v]edit
[v]變更圖片
[v]Delete VM
[v]檢查CRUD
[v]試推orders04
[v]index查詢
[v]index變更圖片icon

[v]index頁上下架更改圖示
[v] 所有頁面新增<div class="row">
        <div class="card">
            <div class="card-body">
[v]師資預覽圖片
[v]改成離職在職
[v]criteria新增是否在職 controller有改
[v]沒有刪除師資
[v]join teacher&lesson
[v]datatable完成
[working on]下架有的有錯誤訊息，有的可以直接離職，資料庫老師就被刪掉，但畫面還顯示著老師資料


















==========================================================
Order 部分
[v]改完版型
[v]EDIT/DELETE抓到當下的創建時間
[v]form-select

[v]訂單(甜點 )畫面需要那些欄位
[v]IndexVM 更改
[v]建立dessert order VM classes
[v]dessert order index VM
[v]寫擴充方法轉vm
[working on]檢查CRUD
[working on]下拉清單寫成一個副程式
[working on]debug課程訂單明細資料庫錯誤，
ViewBag.LessonOrderId = new SelectList(db.LessonOrders, "LessonOrderId", "Note", lessonOrderDetail.LessonOrderId);
[v]criteria進階搜尋 做清除搜尋
[v]Partial View
已甜點訂單的總額(DessertOrderTotal)做前十熱銷的甜點訂單
已甜點訂單明細(DessertOrderDetail)的數量(Quantity)做最熱銷前五名甜點(DessertName)
[V]手風琴單筆ID做開合
[v]button大小間距
[v]更新DessertOrderTotal加上DessertOrderStatusId == 3
[v]課程criteria進階搜尋 清除搜尋















==========================================================
Promotion部分

[V] - 優惠群組增刪查  //todo 改成ajax
        
[V] - 編輯優惠群組頁面
    - 一個畫面使用多個vm
    
[V] - 編輯優惠群組名稱

[V] - 實作優惠群組內加入商品(ajax新增一筆後畫面直接多出一個button不刷新)  //todo強化搜尋功能、搜尋畫面一鍵加入

[V] - 優惠群組去除商品(同上)  

[V] - 優惠券清單，VM因為有一個關聯的東西是nullable所以要特別處理  //todo 可能要新增狀態欄位，複製功能(不編輯了)

[V] - 新增優惠券

[V] - 優惠券詳情

[V] - 促銷活動清單、新增 //jqueryUI datepicker沒法正常顯示 
    - 新增時預覽圖片
    - dropdownlist只抓取特定類別的優惠券
    - 選擇關聯優惠券自動填入此優惠券的開始、結束時間

[V] - 促銷活動修改 //todo 修改圖片還沒

[working on] - 會員優惠券


(1)
discountGroup 增
              刪
              查
              改

coupon        增
              查

promotion     增
              查
              改
------------------------
(2)
promotion     改照片

memberCoupon  查
              增
------------------------
(3)
discount      增
              查
              改
                
(1)大概完成，還須修畫面，補一些小功能，檢查bug
(2)未完成
(3)可能寫不到嗚嗚












