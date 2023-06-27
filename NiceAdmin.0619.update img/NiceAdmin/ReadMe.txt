[V] _Layout & Index 頁面完成
	--更改 content的css位址
	--更改 Script 的 JS位址 bootstrap位址

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

[V] - 修改DataTable 的模板樣式，更改成中文版本

[V] - 新增前台商品上架的頁面 LayoutFront , Sierra.cshtml
    - 新增多張圖顯示， 編輯照片


[working on ] - 全選 checkbox 的變更事件監聽器 
              - AutoComplete

[V] - 加裝sweet alert delete的部分。
    - 資料庫使用Sierras
    - 版本 4.7.2 