var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
$(document).ready(function () {
    loadSubmissions();
});
const submitTable = document.querySelector('#submission-table');
const loadSubmissions = () => __awaiter(this, void 0, void 0, function* () {
    loadPostalCodes();
    submitTable.innerHTML = '';
    let data = new DataHandlerGET('tax/submission');
    let result = yield data.fetchdata();
    let post = result.post;
    for (let sub of post) {
        let tr = document.createElement('TR');
        let td = document.createElement('TD');
        td.innerHTML = `<span class="postalcode"><strong>${sub.postalCode}</strong></span>`;
        tr.appendChild(td);
        td = document.createElement('TD');
        td.innerHTML = `<span class ="income">R ${sub.annualIncome.toFixed(2)}</span>`;
        tr.appendChild(td);
        td = document.createElement('TD');
        let date = new Date(sub.submissionDate);
        td.innerHTML = `<span class ="date">${date.toISOString().split('T')[0]}</span>`;
        tr.appendChild(td);
        td = document.createElement('TD');
        td.innerHTML = `<span class ="tax">R ${sub.calculatedTax.toFixed(2)}</span>`;
        tr.appendChild(td);
        td = document.createElement('TD');
        td.innerHTML = `<button type="button" class="edit btn btn-light btn-sm" data-id="${sub.id}" data-bs-toggle="modal" data-bs-target="#taxcalc-edit-modal">Edit</button>`;
        tr.appendChild(td);
        submitTable.appendChild(tr);
    }
    const editLinks = document.getElementsByClassName('edit');
    for (const editlink of Array.from(editLinks)) {
        editlink.addEventListener("click", () => __awaiter(this, void 0, void 0, function* () {
            let dataAttribute = editlink.getAttribute('data-id');
            let data = new DataHandlerGET(`tax/submission/${dataAttribute}`);
            let result = yield data.fetchdata();
            let idField = document.querySelector('.edit-id');
            let postalField = document.querySelector('.edit-postal');
            let incomeField = document.querySelector('.edit-income');
            idField.value = dataAttribute;
            postalField.value = result.post.postalCode;
            incomeField.value = result.post.annualIncome.toString();
        }), false);
    }
});
const taxFormHandler = new FormHandler('#form-taxCalc', 'tax/submission', 'post')
    .submit(loadSubmissions);
const loadPostalCodes = () => {
    const selects = document.querySelectorAll('#PostalCodeSelect');
    for (let s of selects) {
        if (s.childElementCount > 1)
            continue;
        var options = ["7441", "A100", "7000", "1000"];
        for (let opt of options) {
            let element = document.createElement("option");
            element.text = opt;
            element.value = opt;
            s.appendChild(element);
        }
    }
};
const updateInput = () => {
    const selects = document.querySelectorAll('#PostalCodeSelect');
    for (let s of selects) {
        const inputs = document.querySelectorAll('#PostalCode');
        for (let i of inputs) {
            let sel = s;
            let inp = i;
            inp.value = sel.value;
        }
    }
};
const taxEditFormHandler = new FormHandler('#form-editTax', 'tax/submission', 'put')
    .submit(loadSubmissions);
//# sourceMappingURL=TaxCalculatorService.js.map