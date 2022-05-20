class FormHandler {
    form: HTMLFormElement;
    apiRoute: string;
    data: DataHandler;
    method: string

    constructor(formId: string, apiRoute: string, method: string) {
        this.form = document.querySelector(formId)!;
        this.apiRoute = apiRoute;
        this.method = method;
    }

    submit = async<T>(onSuccessCallback: Function | null = null, onErrorCallback: Function| null = null) => {
        this.form.onsubmit = async (event) => {
            event.preventDefault();

            const formdata = new FormData(this.form);
            var object = {};
            formdata.forEach(function(value, key) {
                object[key] = value;
            });

            if (!handleValidationError<T>(object)) {
                if (this.method === 'post')
                    this.data = new DataHandlerPOST(this.apiRoute, JSON.stringify(object))
                else if (this.method === 'put')
                    this.data = new DataHandlerPUT(this.apiRoute, JSON.stringify(object))
                else
                    throw new RangeError();

                let result = await this.data.fetchdata<T>();
                alertify.alert(JSON.stringify(result.post)).set({ title: "Tax Calculator" });

                if (onErrorCallback !== null) {
                    onErrorCallback(result);
                }
            }

            if (onSuccessCallback !== null)
                await onSuccessCallback();
        };
    };

}
