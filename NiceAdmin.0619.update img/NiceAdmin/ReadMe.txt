主頁面

[V] _Layout & Index 頁面完成
	--更改 content的css位址
	--更改 Script 的 JS位址 bootstrap位址
    - 首頁預覽頁面，icon小圖示
[working on] -  search bar 也使用 autocomplete 而且可以連結到網頁

[V] 創建DefaultController
	-- 新增Form 頁面  /Default/Index

[working on] 創建EFModels
			 --新增 各自的Controller (View Models)
			 --cshtml可以從/Default/Index 裡面的Index.cshtml 更改版型內容。
			 --在自己的Index.cshtml @{BiewBag.Title = "Index";}下面加上
					<main id="main" class="main">
                        <div class="pagetitle">
                            <h1>Form Elements</h1>
                            <nav>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="@Url.Action("Index","Default")">首頁</a></li>
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
    - 資料庫使用Sierras 0627
    - 版本 4.7.2 


=========================================================
Desserts 部分

[V] - 新增前台商品上架的頁面 LayoutFront , Sierras.cshtml
    - 新增多張圖顯示， 編輯照片

[V] - 全選 checkbox 的變更事件監聽器 
    - AutoComplete 甜點清單頁面

[working on] - AJAX 甜點清單頁面

[working on] - 照片更改成圖示 <img>
             


==========================================================
Member 部分



















==========================================================
Class 部分



















==========================================================
Teacher 部分
















==========================================================
Order 部分















==========================================================
Promotion部分















