var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class FormHandler {
    constructor(formId, apiRoute, method) {
        this.submit = (onSuccessCallback = null, onErrorCallback = null) => __awaiter(this, void 0, void 0, function* () {
            this.form.onsubmit = (event) => __awaiter(this, void 0, void 0, function* () {
                event.preventDefault();
                const formdata = new FormData(this.form);
                var object = {};
                formdata.forEach(function (value, key) {
                    object[key] = value;
                });
                if (!handleValidationError(object)) {
                    if (this.method === 'post')
                        this.data = new DataHandlerPOST(this.apiRoute, JSON.stringify(object));
                    else if (this.method === 'put')
                        this.data = new DataHandlerPUT(this.apiRoute, JSON.stringify(object));
                    else
                        throw new RangeError();
                    let result = yield this.data.fetchdata();
                    alertify.alert(JSON.stringify(result.post)).set({ title: "Tax Calculator" });
                    if (onErrorCallback !== null) {
                        onErrorCallback(result);
                    }
                }
                if (onSuccessCallback !== null)
                    yield onSuccessCallback();
            });
        });
        this.form = document.querySelector(formId);
        this.apiRoute = apiRoute;
        this.method = method;
    }
}
//# sourceMappingURL=FormHandler.js.map