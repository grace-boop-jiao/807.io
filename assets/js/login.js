window.onload = function () {
    var user = JSON.parse(localStorage.getItem('tt_currentUser'));
    if (user) document.getElementById('avatarLink').href = "member.html";
};

function handleRegister(e) {
    e.preventDefault();

    var name = document.getElementById('regName').value.trim();
    var phone = document.getElementById('regPhone').value.trim();
    var email = document.getElementById('regEmail').value.trim();
    var pwd = document.getElementById('regPwd').value;

    if (!name || !email || !pwd) {
        alert("請完整填寫必填欄位");
        return;
    }

    try {
        var users = JSON.parse(localStorage.getItem('tt_users') || '[]');
    } catch (e) { users = []; }

    // uniqueness checks
    if (users.find(function(u){ return u.email === email; })) { alert('此 Email 已被使用'); return; }
    if (phone && users.find(function(u){ return u.phone === phone; })) { alert('此電話已被使用'); return; }

    var nextId = users.length ? Math.max.apply(null, users.map(function(u){ return u.id || 0; })) + 1 : 1;
    var newUser = { id: nextId, name: name, email: email, password: pwd, phone: phone, address: '' };
    users.push(newUser);
    localStorage.setItem('tt_users', JSON.stringify(users));
    localStorage.setItem('tt_currentUser', JSON.stringify(newUser));
    alert('註冊成功！');
    location.href = 'member.html';
}

function handleLogin(e) {
    e.preventDefault();

    var email = document.getElementById('loginEmail').value.trim();
    var pwd = document.getElementById('loginPwd').value;

    if (!email || !pwd) {
        alert("請輸入帳號與密碼");
        return;
    }

    try {
        var users = JSON.parse(localStorage.getItem('tt_users') || '[]');
    } catch (e) { users = []; }

    var found = users.find(function(u){ return u.email === email && u.password === pwd; });
    if (!found) { alert('登入失敗，請確認帳號密碼'); return; }
    localStorage.setItem('tt_currentUser', JSON.stringify(found));
    alert('登入成功！');
    location.href = 'member.html';
}

function showForgetModal() { document.getElementById('forgetModal').style.display = 'flex'; }
function closeForgetModal() { document.getElementById('forgetModal').style.display = 'none'; }

function confirmForget() {
    var email = document.getElementById('forgetEmail').value;
    var phone = document.getElementById('forgetPhone').value;
    if (!email || !phone) { alert("請填寫所有欄位！"); return; }
    alert("這是 demo 網站，請聯絡客服重設密碼 : )");
    closeForgetModal();
}
