主頁面

[V] _Layout & Index 頁面完成 (新增Logo)
	--更改 content的css位址
	--更改 Script 的 JS位址 bootstrap位址
    - 首頁預覽頁面，icon小圖示

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
    - 資料庫使用Sierras 0629 (0630 更新資料庫)
    - 版本 4.7.2 

[V] - 新上架的5筆甜點

[working on] - 針對404錯誤訊息畫面 修改web.config畫面，圖片尚未抓到
=========================================================
前台頁面 部分
[V] - 新增前台商品上架的頁面 LayoutFront , Sierras.cshtml
    - 新增師資下拉清單
    - 新增Logo
    - 字體顏色調整

=========================================================
Desserts 部分

[V] - 全選 checkbox 的變更事件監聽器 (Desserts & Categories 的 Index 頁面 抓到分頁數量的checkbox )
    - AutoComplete 甜點清單頁面

[V] - 單筆上,下架按鈕
    - Checkbox Dessert Index 一鍵上,下架
    - 上,下架通知新增SweetAlert方式

[V] - Add DessertIndexPartVM & DessertExts
    - HomeController Add RecentDesserts method
    - Partial View RecentUpDesserts

[working on] - 新增多張圖顯示， 編輯照片(照片的新增 / 刪除)

[working on] - AJAX 甜點清單頁面

[working on] - 編輯照片新增欄位(簡易編輯)
             - 照片更改成圖示 <img>
             - 清單的編輯新增 icon


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
    - modify Index view page
[V] - modify Members/Index（修改MemberIndexVM）
[V] - complete Employee CRUD




==========================================================
Lesson 部分

[V]將sa5的密碼修改和Web.config相同
[V]初步修改Lessons、LessonCategories CRUD版面調整

[V]index進階搜尋 OK

[working on]完善Course CRUD、Courses的多張照片上傳和修改
[working on]CRUD頁面需要補上<main> ⇒ 版型調整、table部分更改成datatable(有需要使用的)


















==========================================================
Teacher 部分
[working on]建立三層式
-add TeacherDto class
[v]改完版型
[v]建立VM classes
[workink on]寫擴充方法轉vm
[v]index頁轉vm
[v]修改TeacherIndexVM，增加display
[v]TeacherIndexVM描述截斷，留前幾個字
[v]修改TeacherCreateVM
[v]上傳檔案
[v]edit
[v]變更圖片
[working on]index查詢
[v]檢查CRUD
[v]試推orders04


















==========================================================
Order 部分
[v]改完版型
[v]EDIT/DELETE抓到當下的創建時間
[v]form-select
[working on]建立VM classes
[workink on]寫擴充方法轉vm
[working on]檢查CRUD
[working on]下拉清單寫成一個副程式
[working on]debug課程訂單明細資料庫錯誤，
ViewBag.LessonOrderId = new SelectList(db.LessonOrders, "LessonOrderId", "Note", lessonOrderDetail.LessonOrderId);
[working on]查詢














==========================================================
Promotion部分

[V] - 優惠群組增刪查
        
[V] - 編輯優惠群組頁面
    - 一個畫面使用多個vm
    
[V] - 編輯優惠群組名稱

[V] - 實作優惠群組內加入商品(ajax新增一筆後畫面直接多出一個button不刷新)  //todo強化搜尋功能、搜尋畫面一鍵加入

[V] - 優惠群組去除商品(同上)  

[V] - 優惠券清單，VM因為有一個關聯的東西是nullable所以要特別處理  //todo 可能要新增狀態欄位，複製功能(不編輯了)

[V] - 新增優惠券

[V] - 優惠券詳情

[V] - 促銷活動清單、新增 //todo dropdownlist只顯示分類為活動的優惠券(不行的話就選完之後ajax檢查)
                      //jqueryUI datepicker沒法正常顯示  

[working on] - 促銷活動修改、刪除













