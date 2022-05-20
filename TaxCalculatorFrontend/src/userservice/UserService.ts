const signin = (result: ApiResult<ApiUser>) => {
    let user = result.post;
    if (!result.error && user !== null) {
        adduser(user);
        displaySignedIn();
        location.reload();
    }
    else {
        signout();
    }
}

const signout = () => {
    localStorage.removeItem('currentUser')
    logoutLink.style.display = 'none'
    loginLink.style.display = 'block'
}

const adduser = (user: ApiUser): void => {
    localStorage.setItem('currentUser', JSON.stringify(user));
}

const getuser = (): ApiUser => {
    let userJson: string = localStorage.getItem('currentUser')
    let apiUser: ApiUser = JSON.parse(userJson) as ApiUser;
    return apiUser;
}

const isAuthenticated: boolean = getuser() !== null;

const logoutLink = document.getElementById('logout-user')
const loginLink = document.getElementById('login-user')

logoutLink.addEventListener("click",
    () => {
        signout();
    },
    false);

const loginFormHandler = new FormHandler('#form-userlogin', 'users/authenticate', 'post')
    .submit<ApiUser>(null, signin);

function displaySignedIn() {
    loginLink.style.display = 'none';
    logoutLink.style.display = 'block';
}
