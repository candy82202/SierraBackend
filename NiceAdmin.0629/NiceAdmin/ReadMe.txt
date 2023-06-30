主頁面

[V] _Layout & Index 頁面完成
	--更改 content的css位址
	--更改 Script 的 JS位址 bootstrap位址
    - 首頁預覽頁面，icon小圖示

[working on] - search bar 也使用 autocomplete 而且可以連結到網頁( 樣式修改 )

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
    - 資料庫使用Sierras 0629
    - 版本 4.7.2 

[working on] - 針對404錯誤訊息畫面 修改web.config畫面，圖片尚未抓到
=========================================================
Desserts 部分

[V] - 新增前台商品上架的頁面 LayoutFront , Sierras.cshtml
    - 新增多張圖顯示， 編輯照片(照片的新增 / 刪除)

[V] - 全選 checkbox 的變更事件監聽器 (Desserts & Categories 的 Index 頁面 抓到分頁數量的checkbox )
    - AutoComplete 甜點清單頁面

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















==========================================================
Lesson 部分

[V]將sa5的密碼修改和Web.config相同
[V]初步修改Lessons、LessonCategories CRUD版面調整



[working on]完善Course CRUD、Courses的多張照片上傳和修改
[working on]CRUD頁面需要補上<main> ⇒ 版型調整、table部分更改成datatable(有需要使用的)


















==========================================================
Teacher 部分
[working on]建立三層式
-add TeacherDto class
[v]改完版型
[working on]VM
[working on]新增資料:抓到當下的創建時間
[working on]檢查CRUD

















==========================================================
Order 部分















==========================================================
Promotion部分

[V] - 優惠群組增刪查
        
[V] - 編輯優惠群組頁面
    - 一個畫面使用多個vm
    
[V] - 編輯優惠群組名稱

[working on] - 實作優惠群組內加入、去除商品功能

改改看












