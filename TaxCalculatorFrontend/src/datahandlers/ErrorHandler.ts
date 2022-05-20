
const handleError = () => {
    let div: any = document.getElementById('main-content');

    let c_event = new CustomEvent("throwError");
    div.addEventListener("throwError", function (e: Event) {
        window.location.href = "/Error";
    }.bind(this));

    div.dispatchEvent(c_event);
}

const handleValidationError = <T>(object): boolean => {
    let json = JSON.stringify(object);
    let obj = JSON.parse(json) as T;

    let hasError = false;
    let message: string = ''

    for (let key in object) {
        let input = document.getElementById(key) as HTMLInputElement
        if (input === null)
            continue;

        let tpe = input.type
        

        let val = Reflect.get(object, key)
        
        if (tpe === 'number') {
            if (isNaN(val)) {
                message += `${key} is not a valid number \n\n`;
                hasError = true
            }
        }
        if (tpe === 'text') {
            if (val === '' || val === null) {
                message += `${key} is not a valid string \n\n`;
                hasError = true
            }
        }
    }

    if (hasError)
        alertify.alert(message);

    return hasError
}