
$(document).ready(function () {
    loadSubmissions()
})

const submitTable = document.querySelector('#submission-table');

const loadSubmissions = async () => {
    loadPostalCodes()
    submitTable.innerHTML = '';
    let data = new DataHandlerGET('tax/submission');
    let result = await data.fetchdata<TaxSubmissionResponse[]>();
    let post = result.post;

    for (let sub of post) {
        let tr = document.createElement('TR');
        let td = document.createElement('TD');
        td.innerHTML = `<span class="postalcode"><strong>${sub.postalCode}</strong></span>`;
        tr.appendChild(td);
        td = document.createElement('TD');
        td.innerHTML = `<span class ="income">R ${sub.annualIncome.toFixed(2)}</span>`
        tr.appendChild(td);
        td = document.createElement('TD');
        let date = new Date(sub.submissionDate)
        td.innerHTML = `<span class ="date">${date.toISOString().split('T')[0]}</span>`
        tr.appendChild(td);
        td = document.createElement('TD');
        td.innerHTML = `<span class ="tax">R ${sub.calculatedTax.toFixed(2)}</span>`
        tr.appendChild(td);
        td = document.createElement('TD');
        td.innerHTML = `<button type="button" class="edit btn btn-light btn-sm" data-id="${sub.id}" data-bs-toggle="modal" data-bs-target="#taxcalc-edit-modal">Edit</button>`
        tr.appendChild(td);
        
        submitTable.appendChild(tr);
    }

    const editLinks = document.getElementsByClassName('edit') as HTMLCollectionOf<HTMLElement>
    for (const editlink of Array.from(editLinks)) {
        editlink.addEventListener("click",
            async () => {
                let dataAttribute = editlink.getAttribute('data-id');
                let data = new DataHandlerGET(`tax/submission/${dataAttribute}`)
                let result = await data.fetchdata<TaxSubmissionEdit>();
                let idField = document.querySelector('.edit-id') as HTMLInputElement;
                let postalField = document.querySelector('.edit-postal') as HTMLInputElement;
                let incomeField = document.querySelector('.edit-income') as HTMLInputElement;
                idField.value = dataAttribute;
                postalField.value = result.post.postalCode;
                incomeField.value = result.post.annualIncome.toString();
            },
            false);
    }

}

const taxFormHandler = new FormHandler('#form-taxCalc', 'tax/submission','post')
    .submit<TaxSubmissionSubmit>(loadSubmissions);

const loadPostalCodes = () => {
    const selects = document.querySelectorAll('#PostalCodeSelect');
    for (let s of selects) {
        if (s.childElementCount > 1)
            continue;

        var options = ["7441", "A100", "7000", "1000"]
        for (let opt of options) {
            let element = document.createElement("option");
            element.text = opt
            element.value = opt
            s.appendChild(element);
        }
    }
}

const updateInput = () => {
    const selects = document.querySelectorAll('#PostalCodeSelect');
    for (let s of selects) {
        const inputs = document.querySelectorAll('#PostalCode');
        for (let i of inputs) {
            let sel = s as HTMLInputElement;
            let inp = i as HTMLInputElement;
            inp.value = sel.value
        }
    }
}

const taxEditFormHandler = new FormHandler('#form-editTax', 'tax/submission', 'put')
    .submit<TaxSubmissionEdit>(loadSubmissions);
