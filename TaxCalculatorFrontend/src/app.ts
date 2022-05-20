declare var alertify: any;

const config = {
    apiUrl: 'https://localhost:4000',
    frontendUrl: $(location).attr("origin"),
}

$(document).ready(function () {
    if (isAuthenticated) {
        displaySignedIn();
    }
})

