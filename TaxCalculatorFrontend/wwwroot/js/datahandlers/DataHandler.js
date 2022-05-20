var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class DataHandler {
    constructor(apiEndpoint, requestBody = null) {
        this.fetchdata = () => __awaiter(this, void 0, void 0, function* () {
            let result = { post: null, error: null, loading: true };
            yield fetch(config.apiUrl + '/' + this.apiEndpoint, this.requestOptions)
                .then(res => res.json())
                .then(data => {
                if (data.message) {
                    alertify.alert(data.message).set({ title: "Tax Calculator" });
                }
                result.post = data;
                result.loading = false;
            }).catch(err => {
                result.error = err.message;
                if (result.error.toLocaleLowerCase().includes('unexpected token')) {
                    result.error = 'Not Found';
                }
                handleError();
            });
            return result;
        });
        this.apiEndpoint = apiEndpoint;
        let auth = getuser();
        var bearer = (auth !== null) ? 'Bearer ' + auth.token : '';
        this.requestOptions = {
            method: 'get',
            headers: { 'Content-Type': 'application/json', 'Authorization': bearer },
            body: requestBody
        };
    }
}
class DataHandlerGET extends DataHandler {
    constructor(apiEndpoint, requestBody = null) {
        super(apiEndpoint, requestBody);
        this.requestOptions.method = 'get';
    }
}
class DataHandlerPOST extends DataHandler {
    constructor(apiEndpoint, requestBody = null) {
        super(apiEndpoint, requestBody);
        this.requestOptions.method = 'post';
    }
}
class DataHandlerPUT extends DataHandler {
    constructor(apiEndpoint, requestBody = null) {
        super(apiEndpoint, requestBody);
        this.requestOptions.method = 'put';
    }
}
//# sourceMappingURL=DataHandler.js.map