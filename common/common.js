async function loadLayout(layoutId, layoutPath) {
    try {
        //使用fetch取得資料
        var response = await fetch(layoutPath);
        var data = await response.text();
        document.getElementById(layoutId).innerHTML = data;
    } catch(error) {
        console.error("錯誤", error);
    }
}

document.addEventListener('DOMContentLoaded', function() {
    loadLayout('header', '../common/header.html');
    loadLayout('nav', '../common/nav.html');
    loadLayout('footer', '../common/footer.html');
});