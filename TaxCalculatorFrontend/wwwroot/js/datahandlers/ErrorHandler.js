const handleError = () => {
    let div = document.getElementById('main-content');
    let c_event = new CustomEvent("throwError");
    div.addEventListener("throwError", function (e) {
        window.location.href = "/Error";
    }.bind(this));
    div.dispatchEvent(c_event);
};
const handleValidationError = (object) => {
    let json = JSON.stringify(object);
    let obj = JSON.parse(json);
    let hasError = false;
    let message = '';
    for (let key in object) {
        let input = document.getElementById(key);
        if (input === null)
            continue;
        let tpe = input.type;
        let val = Reflect.get(object, key);
        if (tpe === 'number') {
            if (isNaN(val)) {
                message += `${key} is not a valid number \n\n`;
                hasError = true;
            }
        }
        if (tpe === 'text') {
            if (val === '' || val === null) {
                message += `${key} is not a valid string \n\n`;
                hasError = true;
            }
        }
    }
    if (hasError)
        alertify.alert(message);
    return hasError;
};
//# sourceMappingURL=ErrorHandler.js.map