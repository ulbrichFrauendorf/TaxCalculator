interface RequestOptions {
    headers: any,
    method: string,
    body: string
}

class DataHandler {

    apiEndpoint: string;
    requestOptions: RequestOptions;

    constructor(apiEndpoint: string, requestBody: string | null = null) {
        this.apiEndpoint = apiEndpoint;
        let auth = getuser();
        var bearer = (auth !== null) ? 'Bearer ' + auth.token : '';
        this.requestOptions = {
            method: 'get',
            headers: { 'Content-Type': 'application/json', 'Authorization': bearer },
            body: requestBody
        };
    }

    fetchdata = async <T>() => {
        let result: ApiResult<T> = { post: null, error: null, loading: true };
        await fetch(config.apiUrl + '/' + this.apiEndpoint, this.requestOptions)
            .then(res => res.json())
            .then(data => {

                if (data.message) {
                    alertify.alert(data.message).set({ title: "Tax Calculator" });
                }

                result.post = <T>data;
                result.loading = false;
            }).catch(err => {
                result.error = err.message;
                if (result.error.toLocaleLowerCase().includes('unexpected token')) {
                    result.error = 'Not Found';
                }

                handleError();
            });

        return result;
    };
}

class DataHandlerGET extends DataHandler {

    constructor(apiEndpoint: string, requestBody: string | null = null) {
        super(apiEndpoint, requestBody);
        this.requestOptions.method = 'get'
    }
}

class DataHandlerPOST extends DataHandler {
    constructor(apiEndpoint: string, requestBody: string | null = null) {
        super(apiEndpoint, requestBody);
        this.requestOptions.method = 'post'
    }
}

class DataHandlerPUT extends DataHandler {

    constructor(apiEndpoint: string, requestBody: string | null = null) {
        super(apiEndpoint, requestBody);
        this.requestOptions.method = 'put'
    }
}