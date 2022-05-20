const signin = (result) => {
    let user = result.post;
    if (!result.error && user !== null) {
        adduser(user);
        displaySignedIn();
        location.reload();
    }
    else {
        signout();
    }
};
const signout = () => {
    localStorage.removeItem('currentUser');
    logoutLink.style.display = 'none';
    loginLink.style.display = 'block';
};
const adduser = (user) => {
    localStorage.setItem('currentUser', JSON.stringify(user));
};
const getuser = () => {
    let userJson = localStorage.getItem('currentUser');
    let apiUser = JSON.parse(userJson);
    return apiUser;
};
const isAuthenticated = getuser() !== null;
const logoutLink = document.getElementById('logout-user');
const loginLink = document.getElementById('login-user');
logoutLink.addEventListener("click", () => {
    signout();
}, false);
const loginFormHandler = new FormHandler('#form-userlogin', 'users/authenticate', 'post')
    .submit(null, signin);
function displaySignedIn() {
    loginLink.style.display = 'none';
    logoutLink.style.display = 'block';
}
//# sourceMappingURL=UserService.js.map